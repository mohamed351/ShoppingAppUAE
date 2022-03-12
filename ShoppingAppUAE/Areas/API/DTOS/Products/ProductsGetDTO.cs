using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.API.DTOS.Products
{
    public class ProductsGetDTO
    {
        public ProductsGetDTO()
        {
            this.Data = new List<ProductDataDTO>();
            this.MetaData = new MetaDataDTO();

        }

        public List<ProductDataDTO> Data { get; set; }

        public MetaDataDTO MetaData { get; set; }


    }

    public class ProductDataDTO
    {
        public int ProductID { get; set; }

        public string ProductNameEnglish { get; set; }


        public string ProductNameArabic { get; set; }



        public string DescriptionArabic { get; set; }

        public string DescriptionEnglish { get; set; }

        public int CateogryID { get; set; }

        public string ImageURL { get; set; }
    }

    public class MetaDataDTO
    {
        public int recordsFiltered { get; set; }
    
        public int recordsTotal { get; set; }
    }
}
