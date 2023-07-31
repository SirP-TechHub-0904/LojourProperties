
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurayUniversal.Application.Repository.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfiguration _configuration;
        ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;
        public SettingsService(IConfiguration configuration, ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> GetMaxUserCount()
        {
            // Retrieve the value from the database or any other data source
            // Example: Assuming the value is stored in a column named 'MaxUserCount' of a table named 'Settings'
            //var xmaxUserCount = await _context.SuperSettings.FirstOrDefaultAsync();
            //var maxUserCount = await _context.SuperSettings.FirstOrDefaultAsync();
            //var UserCount = maxUserCount.MaximumUsers;
            //var excludedRoles = new[] { "mSuperAdmin", "SubAdmin", "finance" };

            //var excludedUserCount = 0;
            //foreach (var role in excludedRoles)
            //{
            //    var usersInRole = await _userManager.GetUsersInRoleAsync(role);
            //    excludedUserCount += usersInRole.Count;
            //}

            //var totalUserCount = _userManager.Users.Count() - excludedUserCount;


            //if (totalUserCount <= UserCount)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            return false;
        }
    }
}
