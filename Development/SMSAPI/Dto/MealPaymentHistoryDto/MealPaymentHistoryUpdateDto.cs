namespace SMSAPI.Dto.MealPaymentHistoryDto
{
    public class MealPaymentHistoryUpdateDto
    {
        public Guid MealPaymentId { get; set; }

        public string MealPaymentPeriod { get; set; }

        public DateOnly MealPaymentDate { get; set; }

        public string MealPaymentAmount { get; set; }

    }
}
