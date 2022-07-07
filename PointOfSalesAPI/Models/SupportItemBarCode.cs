using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesAPI.Models
{
    public class SupportItemBarCode
    {

        public int ID { get; set; }

        public int ProductBarCode { get; set; }
        [ForeignKey(nameof(ProductBarCode))]
        public ProductBarCode SupportItem { get; set; }

        public string BarCode { get; set; }

    }
}
