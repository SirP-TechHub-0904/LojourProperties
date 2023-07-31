using JurayUniversal.Application.Repository.Base;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.Application.Repository.GeneralRepository.PropertyServices
{
    public class PropertyRepositoryAsync : GenericRepositoryAsync<Property>, IProjectRepositoryAsync
    {
     
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Property> _property;


        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStorageService _storageService;
        public PropertyRepositoryAsync(ApplicationDbContext dbContext, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IStorageService storageService) : base(dbContext)
        {
            _property = dbContext.Set<Property>();
            _context = context;           
            _httpContextAccessor = httpContextAccessor;
            _storageService = storageService;
        }

    }
}