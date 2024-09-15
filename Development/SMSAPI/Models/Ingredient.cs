namespace SMSAPI.Models
{
    public class Ingredient
    {
        public Guid IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string IngredientType { get; set; }

        public string IngredientMeasureType { get; set; }

        public int IngredientQty { get; set; }
    }
}
