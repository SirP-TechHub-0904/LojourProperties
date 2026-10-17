using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojourProperties.Web.Areas.Admin.Pages.V1.FileReconciler
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public IndexModel(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public int PropertyImagesCount { get; set; }
        public int SlidersCount { get; set; }
        public int CategoryLocationsCount { get; set; }
        public int PropertyDocumentsCount { get; set; }
        public int PropertyAgenciesCount { get; set; }
        public int PropertyVideosCount { get; set; }
        public int ProfilesCount { get; set; }

        public List<ReconcileLogItem> LogItems { get; set; } = new List<ReconcileLogItem>();

        [TempData]
        public string? SuccessMessage { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            await LoadCountsAsync();
        }

        private async Task LoadCountsAsync()
        {
            PropertyImagesCount = await _context.PropertyImages.CountAsync();
            SlidersCount = await _context.Sliders.CountAsync();
            CategoryLocationsCount = await _context.CategoryLocations.CountAsync();
            PropertyDocumentsCount = await _context.PropertyDocuments.CountAsync();
            PropertyAgenciesCount = await _context.PropertyAgencies.CountAsync();
            PropertyVideosCount = await _context.PropertyVideos.CountAsync();
            ProfilesCount = await _context.Users.CountAsync(p => !string.IsNullOrEmpty(p.PhotoKey) || !string.IsNullOrEmpty(p.PhotoUrl));
        }

        public async Task<IActionResult> OnPostReconcileAllAsync()
        {
            int totalUpdated = 0;
            try
            {
                totalUpdated += await ReconcilePropertyImagesInternalAsync();
                totalUpdated += await ReconcileSlidersInternalAsync();
                totalUpdated += await ReconcileCategoryLocationsInternalAsync();
                totalUpdated += await ReconcilePropertyDocumentsInternalAsync();
                totalUpdated += await ReconcilePropertyAgenciesInternalAsync();
                totalUpdated += await ReconcilePropertyVideosInternalAsync();
                totalUpdated += await ReconcileProfilesInternalAsync();

                SuccessMessage = $"File reconciliation completed successfully! Total {totalUpdated} file/image URLs re-signed and updated.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred during reconciliation: {ex.Message}";
            }

            await LoadCountsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostReconcilePropertyImagesAsync()
        {
            try
            {
                int count = await ReconcilePropertyImagesInternalAsync();
                SuccessMessage = $"Successfully re-signed {count} Property Images!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error updating Property Images: {ex.Message}";
            }
            await LoadCountsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostReconcileSlidersAsync()
        {
            try
            {
                int count = await ReconcileSlidersInternalAsync();
                SuccessMessage = $"Successfully re-signed {count} Sliders!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error updating Sliders: {ex.Message}";
            }
            await LoadCountsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostReconcileCategoryLocationsAsync()
        {
            try
            {
                int count = await ReconcileCategoryLocationsInternalAsync();
                SuccessMessage = $"Successfully re-signed {count} Category Locations!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error updating Category Locations: {ex.Message}";
            }
            await LoadCountsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostReconcileDocumentsAsync()
        {
            try
            {
                int count = await ReconcilePropertyDocumentsInternalAsync();
                count += await ReconcilePropertyAgenciesInternalAsync();
                count += await ReconcilePropertyVideosInternalAsync();
                SuccessMessage = $"Successfully re-signed {count} Documents, Agencies, and Videos!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error updating Documents: {ex.Message}";
            }
            await LoadCountsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostReconcileProfilesAsync()
        {
            try
            {
                int count = await ReconcileProfilesInternalAsync();
                SuccessMessage = $"Successfully re-signed {count} Profile Photos!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error updating Profiles: {ex.Message}";
            }
            await LoadCountsAsync();
            return Page();
        }

        private async Task<int> ReconcilePropertyImagesInternalAsync()
        {
            var items = await _context.PropertyImages.ToListAsync();
            int updated = 0;

            foreach (var img in items)
            {
                bool changed = false;

                // 1. Main Image
                var mainKey = img.Key;
                if (string.IsNullOrWhiteSpace(mainKey) && !string.IsNullOrWhiteSpace(img.ImageUrl))
                {
                    mainKey = ExtractKeyFromUrl(img.ImageUrl);
                    if (!string.IsNullOrWhiteSpace(mainKey))
                    {
                        img.Key = mainKey;
                    }
                }
                if (!string.IsNullOrWhiteSpace(mainKey))
                {
                    var newUrl = GeneratePresignedUrl(mainKey);
                    if (!string.IsNullOrWhiteSpace(newUrl))
                    {
                        img.ImageUrl = newUrl;
                        changed = true;
                    }
                }

                // 2. Full Image
                var fullKey = img.FullImageKey;
                if (string.IsNullOrWhiteSpace(fullKey) && !string.IsNullOrWhiteSpace(img.FullImageUrl))
                {
                    fullKey = ExtractKeyFromUrl(img.FullImageUrl);
                    if (!string.IsNullOrWhiteSpace(fullKey))
                    {
                        img.FullImageKey = fullKey;
                    }
                }
                if (!string.IsNullOrWhiteSpace(fullKey))
                {
                    var newFull = GeneratePresignedUrl(fullKey);
                    if (!string.IsNullOrWhiteSpace(newFull))
                    {
                        img.FullImageUrl = newFull;
                        changed = true;
                    }
                }

                // 3. Small Size Image
                var smallKey = img.SmallSizeKey;
                if (string.IsNullOrWhiteSpace(smallKey) && !string.IsNullOrWhiteSpace(img.SmallSizeImageUrl))
                {
                    smallKey = ExtractKeyFromUrl(img.SmallSizeImageUrl);
                    if (!string.IsNullOrWhiteSpace(smallKey))
                    {
                        img.SmallSizeKey = smallKey;
                    }
                }
                if (!string.IsNullOrWhiteSpace(smallKey))
                {
                    var newSmall = GeneratePresignedUrl(smallKey);
                    if (!string.IsNullOrWhiteSpace(newSmall))
                    {
                        img.SmallSizeImageUrl = newSmall;
                        changed = true;
                    }
                }

                if (changed)
                {
                    updated++;
                }
            }

            if (updated > 0)
            {
                await _context.SaveChangesAsync();
            }
            return updated;
        }

        private async Task<int> ReconcileSlidersInternalAsync()
        {
            var sliders = await _context.Sliders.ToListAsync();
            int updated = 0;

            foreach (var item in sliders)
            {
                var key = item.Key;
                if (string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(item.Url))
                {
                    key = ExtractKeyFromUrl(item.Url);
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        item.Key = key;
                    }
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    var newUrl = GeneratePresignedUrl(key);
                    if (!string.IsNullOrWhiteSpace(newUrl))
                    {
                        item.Url = newUrl;
                        updated++;
                    }
                }
            }

            if (updated > 0)
            {
                await _context.SaveChangesAsync();
            }
            return updated;
        }

        private async Task<int> ReconcileCategoryLocationsInternalAsync()
        {
            var items = await _context.CategoryLocations.ToListAsync();
            int updated = 0;

            foreach (var item in items)
            {
                var key = item.ImageKey;
                if (string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(item.ImageUrl))
                {
                    key = ExtractKeyFromUrl(item.ImageUrl);
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        item.ImageKey = key;
                    }
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    var newUrl = GeneratePresignedUrl(key);
                    if (!string.IsNullOrWhiteSpace(newUrl))
                    {
                        item.ImageUrl = newUrl;
                        updated++;
                    }
                }
            }

            if (updated > 0)
            {
                await _context.SaveChangesAsync();
            }
            return updated;
        }

        private async Task<int> ReconcilePropertyDocumentsInternalAsync()
        {
            var items = await _context.PropertyDocuments.ToListAsync();
            int updated = 0;

            foreach (var item in items)
            {
                var key = item.Key;
                if (string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(item.Url))
                {
                    key = ExtractKeyFromUrl(item.Url);
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        item.Key = key;
                    }
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    var newUrl = GeneratePresignedUrl(key);
                    if (!string.IsNullOrWhiteSpace(newUrl))
                    {
                        item.Url = newUrl;
                        updated++;
                    }
                }
            }

            if (updated > 0)
            {
                await _context.SaveChangesAsync();
            }
            return updated;
        }

        private async Task<int> ReconcilePropertyAgenciesInternalAsync()
        {
            var items = await _context.PropertyAgencies.ToListAsync();
            int updated = 0;

            foreach (var item in items)
            {
                var key = item.Key;
                if (string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(item.Url))
                {
                    key = ExtractKeyFromUrl(item.Url);
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        item.Key = key;
                    }
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    var newUrl = GeneratePresignedUrl(key);
                    if (!string.IsNullOrWhiteSpace(newUrl))
                    {
                        item.Url = newUrl;
                        updated++;
                    }
                }
            }

            if (updated > 0)
            {
                await _context.SaveChangesAsync();
            }
            return updated;
        }

        private async Task<int> ReconcilePropertyVideosInternalAsync()
        {
            var items = await _context.PropertyVideos.ToListAsync();
            int updated = 0;

            foreach (var item in items)
            {
                var key = item.Key;
                if (string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(item.VideoUrl))
                {
                    key = ExtractKeyFromUrl(item.VideoUrl);
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        item.Key = key;
                    }
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    var newUrl = GeneratePresignedUrl(key);
                    if (!string.IsNullOrWhiteSpace(newUrl))
                    {
                        item.VideoUrl = newUrl;
                        updated++;
                    }
                }
            }

            if (updated > 0)
            {
                await _context.SaveChangesAsync();
            }
            return updated;
        }

        private async Task<int> ReconcileProfilesInternalAsync()
        {
            var profiles = await _context.Users.ToListAsync();
            int updated = 0;

            foreach (var user in profiles)
            {
                var key = user.PhotoKey;
                if (string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(user.PhotoUrl))
                {
                    key = ExtractKeyFromUrl(user.PhotoUrl);
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        user.PhotoKey = key;
                    }
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    var newUrl = GeneratePresignedUrl(key);
                    if (!string.IsNullOrWhiteSpace(newUrl))
                    {
                        user.PhotoUrl = newUrl;
                        updated++;
                    }
                }
            }

            if (updated > 0)
            {
                await _context.SaveChangesAsync();
            }
            return updated;
        }

        public string? GeneratePresignedUrl(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return null;

            try
            {
                var accessKey = _config["AwsConfiguration:AWSAccessKey"];
                var secretKey = _config["AwsConfiguration:AWSSecretKey"];
                var bucket = _config["AwsConfiguration:BucketName"] ?? "lojourxyz";

                var credentials = new BasicAWSCredentials(accessKey, secretKey);
                var config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.USEast1
                };

                using var client = new AmazonS3Client(credentials, config);

                var preurl = new GetPreSignedUrlRequest
                {
                    BucketName = bucket,
                    Key = key,
                    Expires = DateTime.UtcNow.AddYears(3)
                };

                return client.GetPreSignedURL(preurl);
            }
            catch
            {
                return null;
            }
        }

        public static string? ExtractKeyFromUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;

            try
            {
                var qIdx = url.IndexOf('?');
                var pathOnly = qIdx > 0 ? url.Substring(0, qIdx) : url;

                if (Uri.TryCreate(pathOnly, UriKind.Absolute, out var uri))
                {
                    var path = uri.AbsolutePath.TrimStart('/');
                    if (path.StartsWith("lojourxyz/", StringComparison.OrdinalIgnoreCase))
                    {
                        path = path.Substring("lojourxyz/".Length);
                    }
                    return Uri.UnescapeDataString(path);
                }
                else
                {
                    return pathOnly.TrimStart('/');
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class ReconcileLogItem
    {
        public string EntityType { get; set; } = "";
        public string Key { get; set; } = "";
        public string OldUrl { get; set; } = "";
        public string NewUrl { get; set; } = "";
        public bool Success { get; set; }
    }
}
