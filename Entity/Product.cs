using System.Xml.Linq;

namespace SirketYonetim.Entity
{
    public class Product:BaseEntity
    {   
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public int producStock { get; set; }


    }
}
