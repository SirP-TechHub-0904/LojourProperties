using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LojourProperties.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<Profile, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<OperatingRegion> OperatingRegions { get; set; }
        public DbSet<FeaturesCategory> FeaturesCategories { get; set; }
        public DbSet<PrivacyCategory> PrivacyCategories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyCategory> PropertyCategories { get; set; }
        public DbSet<PropertyFeature> PropertyFeatures { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyVideo> PropertyVideos { get; set; }
        public DbSet<LocalGoverment> LocalGoverments { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<PropertyDocument> PropertyDocuments { get; set; }
        public DbSet<PropertyAgency> PropertyAgencies { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<WebPage> WebPages { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SafetyTip> SafetyTips { get; set; }
        public DbSet<MailingSystem> MailingSystems { get; set; }
        public DbSet<UserRegion> UserRegions { get; set; }
        public DbSet<PropertyCategoryList> PropertyCategoryLists { get; set; }
        public DbSet<Testimony> Testimonies { get; set; }

        public DbSet<CityLocation> CityLocations { get; set; }
        public DbSet<CategoryLocation> CategoryLocations { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

    }
    //public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {




    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        optionsBuilder.UseSqlServer("Server=sql5094.site4now.net;Database=DB_9AFABF_GlobalWebs;User Id=DB_9AFABF_GlobalWebs_admin;Password=Exwhyzee@2023;MultipleActiveResultSets=true");

    //        return new ApplicationDbContext(optionsBuilder.Options);

    //    }
    //}
}