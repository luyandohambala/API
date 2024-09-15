namespace SMSAPI.Models
{
    public class MealPayment
    {
        public Guid MealPaymentId { get; set; }

        public string MealPaymentPupilName { get; set; }

        public string MealPaymentPeriod { get; set; }

        public DateOnly MealPaymentDate { get; set; }

        public string MealPaymentAmount { get; set; }

        public string MealPaymentStatus { get; set; }

        public Guid MealPaymentPupilId { get; set; }    


        //navigation properties
        public Pupil Pupil { get; set; }
    }
}
