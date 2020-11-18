using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class Stroka_check_and_postavkaModel
    {
        public int id { get; set; }
        public int id_stroka_check { get; set; }
        public int id_stroka_postavka { get; set; }
        public int kolvo_product_in_stroka_postavka { get; set; }

        public Stroka_check_and_postavkaModel() { }
        public Stroka_check_and_postavkaModel(Stroka_check_and_postavka check_and_postavka)
        {
            id_stroka_check = check_and_postavka.id_stroka_check;
            id_stroka_postavka = check_and_postavka.id_stroka_postavka;
            id = check_and_postavka.id;
            kolvo_product_in_stroka_postavka = check_and_postavka.kolvo_product_in_stroka_postavka;
        }
    }
}
