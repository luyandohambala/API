namespace SMSAPI.Dto.MealPaymentDto
{
    public class MealPaymentCreateDto
    {
        public Guid MealPaymentId { get; set; }

        public string MealPaymentPupilName { get; set; }

        public string MealPaymentPeriod { get; set; }

        public DateOnly MealPaymentDate { get; set; }

        public string MealPaymentAmount { get; set; }

        public string MealPaymentStatus { get; set; }
    }
}
