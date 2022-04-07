using System.ComponentModel.DataAnnotations;

namespace ShoppingAppUAE.Data
{
    public class Counties
    {
        [Key]
        public string Code { get; set; }

        public string EnglishName { get; set; }

        public string ArabicName { get; set; }

        public string Prefix { get; set; }
    }

}
