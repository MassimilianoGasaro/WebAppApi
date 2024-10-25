namespace WebAppApi.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Where { get; set; }
        public float Cost { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public DateTime Date {  get; set; }

        public int TypeId { get; set; }
        public Typology Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

}
