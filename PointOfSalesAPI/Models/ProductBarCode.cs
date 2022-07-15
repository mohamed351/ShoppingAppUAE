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
        public SerialStatus SerialStatus { get; set; }

        public ICollection<SupportItemBarCode> SupportItemBarCodes { get; set; }

        public bool IsDeleted { get; set; }

    }


    public enum SerialStatus
    {
        New = 0,
        Buy = 1,
        Purchased = 2,
        PurchaseReund = 3,
        BackToSupplier = 4
    }
}
