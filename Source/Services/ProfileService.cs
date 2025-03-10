using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public RestaurantProfile GetProfile()
        {
            return _context.RestaurantProfiles.FirstOrDefault();
        }

        public void AddProfile(RestaurantProfile profileProvider)
        {
            _context.RestaurantProfiles.Add(profileProvider);
            _context.SaveChanges();
        }

        public void UpdateProfile(RestaurantProfile profileProvider)
        {
            _context.Entry(profileProvider).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}