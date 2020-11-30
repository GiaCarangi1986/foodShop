using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foodShop
{
    public class ProsrochkaModel : IComparable
    {
        public int? ostalos_product { get; set; }
        public bool spisano { get; set; }
        public int? scor_godnosti_O { get; set; }
        public DateTime? date_of_preparing { get; set; }
        public int line_of_postavka { get; set; }
        public int number_of_postavka_FK { get; set; }
        public int code_of_product_FK { get; set; }
        public string title { get; set; }
        public bool isSelected { get; set; } //выбран / не выбран продукт для списания

            public int CompareTo(object o)
        {
            ProsrochkaModel b = o as ProsrochkaModel;
            if (b != null)
                return title.CompareTo(b.title);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}
