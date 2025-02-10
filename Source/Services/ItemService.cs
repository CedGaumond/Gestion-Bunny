using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;


namespace Gestion_Bunny.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;

        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        // This method should return a Task<List<ItemCategory>>
        public async Task<List<ItemCategory>> GetCategoriesAsync()
        {
            return await _context.ItemCategories.ToListAsync();  // Return the list of categories from the database
        }

        public async Task<List<Item>> GetRecipesAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task DeleteRecipeAsync(int recipeId)
        {
            var recipe = await _context.Items.FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe != null)
            {
                recipe.IsDeleted = true;
                recipe.DeletedDate = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
            }
        }

        Task<List<Item>> IItemService.GetRecipesAsync()
        {
            throw new NotImplementedException();
        }
    }

}
