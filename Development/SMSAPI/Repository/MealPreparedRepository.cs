using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class MealPreparedRepository : IMealPreparedRepository
    {
        private readonly DataContext _context;

        public MealPreparedRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateMealPrepared(MealPrepared mealPrepared)
        {
            _context.Add(mealPrepared);

            return Save();
        }

        public bool DeleteMealPrepared(MealPrepared mealPrepared)
        {
            _context.Remove(mealPrepared);

            return Save();
        }

        public MealPrepared GetMealPrepared(Guid mealPreapredId)
        {
            return _context.MealsPrepared.Where(mp => mp.MealPreparedId == mealPreapredId).FirstOrDefault();
        }

        public ICollection<MealPrepared> GetMealsPrepared()
        {
            return [.. _context.MealsPrepared.OrderBy(mp => mp.MealPreparedId)];
        }

        public bool MealPreparedExists(Guid mealPreapredId)
        {
            return _context.MealsPrepared.Any(mp => mp.MealPreparedId == mealPreapredId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateMealPrepared(MealPrepared mealPrepared)
        {
            _context.Update(mealPrepared);

            return Save();
        }
    }
}
