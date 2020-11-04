using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    class PostavkaModel
    {
        public int number_of_postavka { get; set; }
        public decimal? itogo_cost { get; set; }
        public DateTime date_of_postavka { get; set; }

        public PostavkaModel() { }
        public PostavkaModel(Postavka postavka)
        {
            number_of_postavka = postavka.number_of_postavka;
            itogo_cost = postavka.itogo_cost;
            date_of_postavka = postavka.date_of_postavka;
        }
    }
}
