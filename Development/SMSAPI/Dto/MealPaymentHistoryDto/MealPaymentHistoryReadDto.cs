namespace SMSAPI.Dto.MealPaymentHistoryDto
{
    public class MealPaymentHistoryReadDto
    {
        public Guid MealPaymentId { get; set; }

        public string MealPaymentPupilName { get; set; }

        public string MealPaymentPeriod { get; set; }

        public DateOnly MealPaymentDate { get; set; }

        public string MealPaymentAmount { get; set; }

        public Guid MealPaymentPupilId { get; set; }

    }
}
