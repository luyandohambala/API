using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class MealPaymentRepository : IMealPaymentRepository
    {
        private readonly DataContext _context;

        public MealPaymentRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateMealPayment(MealPayment mealPayment)
        {
            _context.Add(mealPayment);

            return Save();
        }

        public bool DeleteMealPayment(MealPayment mealPayment)
        {
            _context.Remove(mealPayment);

            return Save();
        }

        public MealPayment GetMealPayment(Guid mealPaymentId)
        {
            return _context.MealPayments.Where(mp => mp.MealPaymentId == mealPaymentId).FirstOrDefault();
        }

        public MealPayment GetMealPaymentByPupilId(Guid pupilId)
        {
            return _context.MealPayments.Where(mp => mp.MealPaymentPupilId == pupilId).FirstOrDefault();
        }

        public ICollection<MealPayment> GetMealPayments()
        {
            return [.. _context.MealPayments.OrderBy(mp => mp.MealPaymentId)];
        }

        public bool MealPaymentExists(Guid mealPaymentId)
        {
            return _context.MealPayments.Any(mp => mp.MealPaymentId == mealPaymentId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateMealPayment(MealPayment mealPayment)
        {
            _context.Update(mealPayment);

            return Save();
        }
    }
}
