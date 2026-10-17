#!/bin/bash
set -e

APP_DIR="/var/www/lojour"
SUDO=""
if [ "$(id -u)" -ne 0 ]; then
  SUDO="sudo"
fi

CURRENT_USER=$(whoami)

echo "=================================================="
echo ">>> Deploying Lojour Properties for user: $CURRENT_USER"
echo "=================================================="

# 1. Ensure directory exists and has correct ownership
$SUDO mkdir -p "$APP_DIR"
$SUDO chown -R "$CURRENT_USER":"$CURRENT_USER" "$APP_DIR"

cd "$APP_DIR"

# 2. Extract new release
if [ -f "/tmp/app-package.tar.gz" ]; then
  echo ">>> Extracting application package to $APP_DIR..."
  tar -xzf /tmp/app-package.tar.gz -C "$APP_DIR"
  rm -f /tmp/app-package.tar.gz
fi

chmod +x "$APP_DIR/LojourProperties.Web"

# 3. Setup .env file if it doesn't already exist
if [ ! -f "$APP_DIR/.env" ]; then
  echo ">>> Initializing default .env file..."
  cat << 'EOF' > "$APP_DIR/.env"
ConnectionStrings__DefaultConnection="Server=147.93.128.160,1433;Database=DB_9AFABF_Lojour;User Id=user_db;Password=Exwhyzee@123;TrustServerCertificate=True;MultipleActiveResultSets=true"
AwsConfiguration__AWSAccessKey="AKIA4Y267QYK25XMO6WE"
AwsConfiguration__AWSSecretKey="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"
ASPNETCORE_ENVIRONMENT=Production
EOF
  chmod 600 "$APP_DIR/.env"
fi

# 4. Stop existing instance temporarily to verify port availability
echo ">>> Checking existing services..."
if command -v systemctl >/dev/null 2>&1 && systemctl is-active --quiet lojour.service 2>/dev/null; then
  echo ">>> Stopping current lojour.service..."
  $SUDO systemctl stop lojour.service || true
fi
if [ -f "$APP_DIR/app.pid" ]; then
  OLD_PID=$(cat "$APP_DIR/app.pid")
  kill "$OLD_PID" 2>/dev/null || true
  rm -f "$APP_DIR/app.pid"
fi
pkill -f "LojourProperties.Web" 2>/dev/null || true
sleep 2

# 5. Detect / Confirm free port
PORT=""
if [ -f "$APP_DIR/.port" ] && [ -s "$APP_DIR/.port" ]; then
  SAVED_PORT=$(cat "$APP_DIR/.port" | tr -d '[:space:]')
  # Verify if saved port is free now
  if ! ss -tuln 2>/dev/null | grep -q ":$SAVED_PORT " && ! netstat -tuln 2>/dev/null | grep -q ":$SAVED_PORT "; then
    PORT=$SAVED_PORT
    echo ">>> Reusing previously assigned port: $PORT"
  fi
fi

if [ -z "$PORT" ]; then
  echo ">>> Searching for free port starting from 5000..."
  TEST_PORT=5000
  while ss -tuln 2>/dev/null | grep -q ":$TEST_PORT " || netstat -tuln 2>/dev/null | grep -q ":$TEST_PORT " || lsof -i :$TEST_PORT >/dev/null 2>&1; do
    echo "Port $TEST_PORT is in use, trying next..."
    TEST_PORT=$((TEST_PORT + 1))
  done
  PORT=$TEST_PORT
  echo ">>> Confirmed free port: $PORT"
  echo "$PORT" > "$APP_DIR/.port"
fi

# Update ASPNETCORE_URLS in .env
if grep -q "ASPNETCORE_URLS=" "$APP_DIR/.env"; then
  sed -i "s|ASPNETCORE_URLS=.*|ASPNETCORE_URLS=http://0.0.0.0:$PORT|g" "$APP_DIR/.env"
else
  echo "ASPNETCORE_URLS=http://0.0.0.0:$PORT" >> "$APP_DIR/.env"
fi

# 6. Allow port through firewall if ufw / iptables are active
if command -v ufw >/dev/null 2>&1; then
  echo ">>> Allowing port $PORT through UFW firewall..."
  $SUDO ufw allow "$PORT"/tcp 2>/dev/null || true
fi
if command -v iptables >/dev/null 2>&1; then
  $SUDO iptables -I INPUT -p tcp --dport "$PORT" -j ACCEPT 2>/dev/null || true
fi

# 7. Start application (systemd or fallback)
USE_SYSTEMD=false
if command -v systemctl >/dev/null 2>&1 && [ -d /run/systemd/system ]; then
  USE_SYSTEMD=true
fi

if [ "$USE_SYSTEMD" = true ]; then
  echo ">>> Configuring systemd service /etc/systemd/system/lojour.service..."
  cat << EOF | $SUDO tee /etc/systemd/system/lojour.service > /dev/null
[Unit]
Description=Lojour Properties Web Application
After=network.target

[Service]
WorkingDirectory=$APP_DIR
ExecStart=$APP_DIR/LojourProperties.Web --urls http://0.0.0.0:$PORT
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=lojour-web
User=$CURRENT_USER
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=PORT=$PORT
EnvironmentFile=$APP_DIR/.env

[Install]
WantedBy=multi-user.target
EOF

  $SUDO systemctl daemon-reload
  $SUDO systemctl enable lojour.service
  $SUDO systemctl restart lojour.service
  echo ">>> Systemd service restarted."
else
  echo ">>> Starting in background with nohup..."
  nohup "$APP_DIR/LojourProperties.Web" --urls "http://0.0.0.0:$PORT" > "$APP_DIR/app.log" 2>&1 &
  echo $! > "$APP_DIR/app.pid"
  echo ">>> Started process PID $(cat "$APP_DIR/app.pid")"
fi

# 8. Health check
echo ">>> Checking application status on port $PORT..."
sleep 4

HEALTHY=false
for i in {1..6}; do
  HTTP_STATUS=$(curl -s -o /dev/null -w "%{http_code}" "http://127.0.0.1:$PORT" || true)
  echo "Health check attempt $i: HTTP status $HTTP_STATUS"
  if [ "$HTTP_STATUS" = "200" ] || [ "$HTTP_STATUS" = "302" ] || [ "$HTTP_STATUS" = "301" ] || [ "$HTTP_STATUS" = "404" ]; then
    HEALTHY=true
    break
  fi
  sleep 2
done

echo ""
echo "=================================================="
echo "DEPLOYMENT COMPLETE"
echo "PORT: $PORT"
echo "HEALTHY: $HEALTHY"
echo "=================================================="
