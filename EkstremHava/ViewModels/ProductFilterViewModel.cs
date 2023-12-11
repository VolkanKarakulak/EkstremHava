using EkstremHava.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
#nullable disable

namespace EkstremHava.ViewModels
{
    public class ProductFilterViewModel
    {
        private SelectList _types;

        public List<Product> Products { get; set; }
        public SelectList Types
        {
            get
            {
                return _types ??= new SelectList(new List<ProductType>(), nameof(ProductType.ProductTypeId), nameof(ProductType.ProductTypeName));
            }
            set { _types = value; }
        }

        public int SelectedTypeId { get; set; }

        public void LoadTypes(IEnumerable<ProductType> allProductTypes)
        {
            Types = new SelectList(allProductTypes, nameof(ProductType.ProductTypeId), nameof(ProductType.ProductTypeName));
        }
    }
}
