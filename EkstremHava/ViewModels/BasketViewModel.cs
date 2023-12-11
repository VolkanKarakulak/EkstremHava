using EkstremHava.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EkstremHava.ViewModels
{
    public class BasketViewModel
    {
        public List<BasketInfo> BasketItems { get; set; }

        public decimal BasketTotal { get; set; }

        public int ItemsInBasketCount { get; set; }

    }
}
