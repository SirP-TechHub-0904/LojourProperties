using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Services.ImageResize
{
    public class FileImageResize : IFileImageResize
    {
        public async Task<MemoryStream> UploadFileAsync(string url, int xheight, int xwidth)
        {
            string imageUrl = "https://example.com/image.jpg";
            int desiredWidth = 800;
            int desiredHeight = 600;

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(imageUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream imageStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (Image originalImage = Image.FromStream(imageStream))
                            {
                                using (Image resizedImage = ResizeImage(originalImage, desiredWidth, desiredHeight))
                                {
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        resizedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        //memoryStream.Seek(0, SeekOrigin.Begin);

                                        //// Use memoryStream as needed, e.g., copy it to another stream.
                                        //using (FileStream fileStream = new FileStream("resized_image.jpg", FileMode.Create))
                                        //{
                                        //    await memoryStream.CopyToAsync(fileStream);
                                        //    //return fileStream;
                                        //}
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }


        static Image ResizeImage(Image image, int desiredWidth, int desiredHeight)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            float widthRatio = (float)desiredWidth / originalWidth;
            float heightRatio = (float)desiredHeight / originalHeight;

            float ratio = Math.Min(widthRatio, heightRatio);

            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, originalWidth, originalHeight, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public async Task<MemoryStream> ReduceFileSteamAsync(MemoryStream imageStream, int xheight, int xwidth)
        {
            MemoryStream rxstream = new MemoryStream();
            using (Image originalImage = Image.FromStream(imageStream))
            {
                using (Image resizedImage = ResizeImage(originalImage, xwidth, xheight))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        resizedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        rxstream = memoryStream;
                        //memoryStream.Seek(0, SeekOrigin.Begin);

                        //// Use memoryStream as needed, e.g., copy it to another stream.
                        //using (FileStream fileStream = new FileStream("resized_image.jpg", FileMode.Create))
                        //{
                        //    await memoryStream.CopyToAsync(fileStream);
                        //    //return fileStream;
                        //   // rxstream = fileStream;
                        //}

                    }
                }
            }
            return rxstream;
        }
    }
}
