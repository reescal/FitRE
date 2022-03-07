namespace FitRE.Shared
{
    public class Budget
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal[] HistoricalExpenses { get; set; }
        public string[] ExpenseCategories { get; set; }
    }
}