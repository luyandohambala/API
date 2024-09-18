using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IMealPaymentHistoryRepository
    {
        //read methods
        ICollection<MealPaymentHistory> GetMealPaymentHistories();
        ICollection<MealPaymentHistory> GetMealPaymentHistoriesByPupilId(Guid pupilId); 

        MealPaymentHistory GetMealPaymentHistory(Guid mealPaymentHistoryId);


        //create methods
        bool CreateMealPaymentHistory(MealPaymentHistory mealPaymentHistory);

        bool UpdateMealPaymentHistory(MealPaymentHistory mealPaymentHistory);

        bool Save();


        //delete methods
        bool DeleteMealPaymentHistory(MealPaymentHistory mealPaymentHistory);


        //validation methods    
        bool MealPaymentHistoryExists(Guid mealPaymentHistoryId);
    }
}
