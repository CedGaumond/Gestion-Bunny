using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
   public interface IProfileService
   {
        RestaurantProfile GetProfile();
        void AddProfile(RestaurantProfile profileProvider);
        void UpdateProfile(RestaurantProfile profileProvider);
   }
}
