namespace CoffeeMaker.Presentation.Response
{
    public class ChangeResponse
    {
        public int TotalChange { get; set; }
        public Dictionary<int, int> ChangeBreakdown { get; set; }
        public string Message { get; set; }
    }
}
