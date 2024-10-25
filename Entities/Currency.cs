namespace WebAppApi.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Expense> Expenses { get; } = [];
    }
}
