namespace ProductManagementApi.Models.Response
{
    public class PlaceOrderResponce
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        public DateTime OrderDate { get; set; }

        public string? Message { get; set; }
    }
}
