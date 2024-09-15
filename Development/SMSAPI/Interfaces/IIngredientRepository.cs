using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IIngredientRepository
    {
        //read methods
        ICollection<Ingredient> GetIngredients();

        Ingredient GetIngredient(Guid ingredientId);


        //write methods
        bool CreateIngredient(Ingredient ingredient);

        bool UpdateIngredient(Ingredient ingredient);

        bool Save();


        //delete methods
        bool DeleteIngredient(Ingredient ingredient);


        //validation methods
        bool IngredientExists(Guid ingredientId);
    }
}
