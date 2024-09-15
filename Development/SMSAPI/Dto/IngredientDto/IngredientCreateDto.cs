namespace SMSAPI.Dto.IngredientDto
{
    public class IngredientCreateDto
    {
        public Guid IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string IngredientType { get; set; }

        public string IngredientMeasureType { get; set; }

        public int IngredientQty { get; set; }
    }
}
