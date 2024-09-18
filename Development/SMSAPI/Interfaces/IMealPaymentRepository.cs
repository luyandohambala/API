using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IMealPaymentRepository
    {
        //read methods
        ICollection<MealPayment> GetMealPayments();

        MealPayment GetMealPayment(Guid mealPaymentId);

        MealPayment GetMealPaymentByPupilId(Guid pupilId);


        //write methods
        bool CreateMealPayment(MealPayment mealPayment);

        bool UpdateMealPayment(MealPayment mealPayment);

        bool Save();

           
        //delete methods
        bool DeleteMealPayment(MealPayment mealPayment);


        //Validation methods
        bool MealPaymentExists(Guid mealPaymentId);
    }
}
