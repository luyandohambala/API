using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IMealPreparedRepository
    {
        //read methods
        ICollection<MealPrepared> GetMealsPrepared();

        MealPrepared GetMealPrepared(Guid mealPreapredId);


        //write methods
        bool CreateMealPrepared(MealPrepared mealPrepared);

        bool UpdateMealPrepared(MealPrepared mealPrepared);

        bool Save();


        //delete methods
        bool DeleteMealPrepared(MealPrepared mealPrepared);


        //validation methods
        bool MealPreparedExists(Guid mealPreapredId);
    }
}
