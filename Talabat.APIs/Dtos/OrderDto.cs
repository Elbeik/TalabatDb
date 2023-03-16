namespace Talabat.APIs.Dtos
{
    public class OrderDto
    {
        public string BaskedId { get; set; }
        public int DelivertMethode { get; set; }
        public AddressDto ShappingAddress { get; set; }
    }
}
