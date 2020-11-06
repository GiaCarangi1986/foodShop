using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class Line_of_postavkaModel
    {
        public int line_of_postavka { get; set; }
        public int? kolvo_of_product { get; set; }
        public int? ostalos_product { get; set; }
        public DateTime? date_of_preparing { get; set; }
        public bool spisano { get; set; }
        public decimal own_cost { get; set; }
        public int code_of_product_FK { get; set; }
        public int number_of_postavka_FK { get; set; }

        public Line_of_postavkaModel() { }
        public Line_of_postavkaModel(Line_of_postavka lpostavka)
        {
            line_of_postavka = lpostavka.line_of_postavka1;
            own_cost = lpostavka.own_cost;
            kolvo_of_product = lpostavka.kolvo_of_product;
            ostalos_product = lpostavka.ostalos_product;
            date_of_preparing = lpostavka.date_of_preparing;
            spisano = lpostavka.spisano;
            code_of_product_FK = lpostavka.code_of_product_FK;
            number_of_postavka_FK = lpostavka.number_of_postavka_FK;
        }
    }
}
