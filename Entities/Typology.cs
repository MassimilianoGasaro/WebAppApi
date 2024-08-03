using Microsoft.Extensions.Hosting;

namespace WebAppApi.Entities
{
    public class Typology
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Expense> Expenses { get; } = [];
    }
}
