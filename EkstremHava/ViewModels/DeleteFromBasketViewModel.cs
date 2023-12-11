namespace EkstremHava.ViewModels
{
    public class DeleteFromBasketViewModel
    {
        public string Message { get; set; }

        public decimal BasketTotal { get; set; }

        public decimal QuantityOfProductBasket { get; set; }

        public int QuantityOfRelatedProduct { get; set; }

        public int DeletedOfProductId { get; set; }
    }
}
