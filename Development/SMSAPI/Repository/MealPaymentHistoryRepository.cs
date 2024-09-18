using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class MealPaymentHistoryRepository : IMealPaymentHistoryRepository
    {
        private readonly DataContext _context;

        public MealPaymentHistoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMealPaymentHistory(MealPaymentHistory mealPaymentHistory)
        {
            _context.Add(mealPaymentHistory);

            return Save();
        }

        public bool DeleteMealPaymentHistory(MealPaymentHistory mealPaymentHistory)
        {
            _context.Remove(mealPaymentHistory);

            return Save();
        }

        public ICollection<MealPaymentHistory> GetMealPaymentHistories()
        {
            return [.. _context.MealPaymentHistories.OrderBy(mph => mph.MealPaymentId)];
        }

        public ICollection<MealPaymentHistory> GetMealPaymentHistoriesByPupilId(Guid pupilId)
        {
            return [.. _context.MealPaymentHistories.Where(mph => mph.MealPaymentPupilId == pupilId)];
        }

        public MealPaymentHistory GetMealPaymentHistory(Guid mealPaymentHistoryId)
        {
            return _context.MealPaymentHistories.Where(mph => mph.MealPaymentId == mealPaymentHistoryId).FirstOrDefault();
        }

        public bool MealPaymentHistoryExists(Guid mealPaymentHistoryId)
        {
            return _context.MealPaymentHistories.Any(mph => mph.MealPaymentId == mealPaymentHistoryId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateMealPaymentHistory(MealPaymentHistory mealPaymentHistory)
        {
            _context.Update(mealPaymentHistory);

            return Save();
        }
    }
}
