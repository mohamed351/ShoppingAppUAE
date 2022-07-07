using System.Collections.Generic;

namespace PointOfSalesAPI.Models
{
    public class ProductBarCode
    {
        public ProductBarCode()
        {
            this.SupportItemBarCodes = new HashSet<SupportItemBarCode>();
        }
        public int ID { get; set; }

        public string MainBarCode { get; set; }

        public int ProductID { get; set; }

        public ICollection<SupportItemBarCode> SupportItemBarCodes { get; set; }

    }
}
