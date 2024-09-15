using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DataContext _context;

        public IngredientRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateIngredient(Ingredient ingredient)
        {
            _context.Add(ingredient);

            return Save();
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            _context.Remove(ingredient);

            return Save();
        }

        public Ingredient GetIngredient(Guid ingredientId)
        {
            return _context.Ingredients.Where(i => i.IngredientId == ingredientId).FirstOrDefault();
        }

        public ICollection<Ingredient> GetIngredients()
        {
            return [.. _context.Ingredients.OrderBy(i => i.IngredientId)];
        }

        public bool IngredientExists(Guid ingredientId)
        {
            return _context.Ingredients.Any(i => i.IngredientId ==  ingredientId);  
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateIngredient(Ingredient ingredient)
        {
            _context.Update(ingredient);

            return Save();
        }
    }
}
