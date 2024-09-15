namespace SMSAPI.Models
{
    public class MealPaymentHistory
    {
        public Guid MealPaymentId { get; set; }

        public string MealPaymentPupilName { get; set; }

        public string MealPaymentPeriod { get; set; }

        public DateOnly MealPaymentDate { get; set; }

        public string MealPaymentAmount { get; set; }

        public Guid MealPaymentPupilId { get; set; }


        //Navigation properties
        public Pupil Pupil { get; set; }
    }
}
