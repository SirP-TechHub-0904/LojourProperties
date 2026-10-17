using Amazon.S3;
using LojourProperties.Domain;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Load environment variables from .env file if present
LoadEnvFile();

var builder = WebApplication.CreateBuilder(args);

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
    }
});

builder.Services.AddScoped<DbContext, ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Profile, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddLojourDataServices();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    // options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddAWSService<IAmazonS3>();

// Forwarded headers for Linux hosting
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

if (args.Contains("--reconcile-all"))
{
    Console.WriteLine("==================================================");
    Console.WriteLine("STARTING DATABASE FILE & IMAGE S3 RECONCILER");
    Console.WriteLine("==================================================");
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var reconciler = new LojourProperties.Web.Areas.Admin.Pages.V1.FileReconciler.IndexModel(db, config);

        Console.WriteLine("[1/5] Reconciling Property Images...");
        await reconciler.OnPostReconcilePropertyImagesAsync();
        Console.WriteLine(">>> " + (reconciler.SuccessMessage ?? reconciler.ErrorMessage));

        Console.WriteLine("[2/5] Reconciling Web Sliders...");
        await reconciler.OnPostReconcileSlidersAsync();
        Console.WriteLine(">>> " + (reconciler.SuccessMessage ?? reconciler.ErrorMessage));

        Console.WriteLine("[3/5] Reconciling Category Locations...");
        await reconciler.OnPostReconcileCategoryLocationsAsync();
        Console.WriteLine(">>> " + (reconciler.SuccessMessage ?? reconciler.ErrorMessage));

        Console.WriteLine("[4/5] Reconciling Documents, Agencies, Videos...");
        await reconciler.OnPostReconcileDocumentsAsync();
        Console.WriteLine(">>> " + (reconciler.SuccessMessage ?? reconciler.ErrorMessage));

        Console.WriteLine("[5/5] Reconciling User Profiles...");
        await reconciler.OnPostReconcileProfilesAsync();
        Console.WriteLine(">>> " + (reconciler.SuccessMessage ?? reconciler.ErrorMessage));
    }
    Console.WriteLine("==================================================");
    Console.WriteLine("RECONCILIATION COMPLETED SUCCESSFULLY!");
    Console.WriteLine("==================================================");
    return;
}

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}

if (string.Equals(Environment.GetEnvironmentVariable("ENABLE_HTTPS_REDIRECTION"), "true", StringComparison.OrdinalIgnoreCase))
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

void LoadEnvFile()
{
    var candidates = new[]
    {
        Path.Combine(Directory.GetCurrentDirectory(), ".env"),
        Path.Combine(AppContext.BaseDirectory, ".env"),
        Path.Combine(AppContext.BaseDirectory, "..", ".env"),
        Path.Combine(Directory.GetCurrentDirectory(), "..", ".env")
    };

    foreach (var path in candidates)
    {
        if (File.Exists(path))
        {
            try
            {
                foreach (var rawLine in File.ReadAllLines(path))
                {
                    var line = rawLine.Trim();
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        continue;

                    var idx = line.IndexOf('=');
                    if (idx > 0)
                    {
                        var key = line.Substring(0, idx).Trim();
                        var val = line.Substring(idx + 1).Trim();
                        if ((val.StartsWith("\"") && val.EndsWith("\"")) || (val.StartsWith("'") && val.EndsWith("'")))
                        {
                            val = val.Substring(1, val.Length - 2);
                        }
                        Environment.SetEnvironmentVariable(key, val);
                        if (key.Contains("__"))
                        {
                            Environment.SetEnvironmentVariable(key.Replace("__", ":"), val);
                        }
                        else if (key.Contains(":"))
                        {
                            Environment.SetEnvironmentVariable(key.Replace(":", "__"), val);
                        }
                    }
                }
            }
            catch
            {
                // Silently ignore if reading fails
            }
            break;
        }
    }
}
