using WebAppApi.Entities;

namespace WebAppApi.DTOs
{
    public class ExpenseDTO
    {
        public string Name { get; set; }
        public string Where { get; set; }
        public float Cost { get; set; }
        public int CurrencyId { get; set; }
        public DateTime Date { get; set; }
        public int TypeId { get; set; }

    }
}
