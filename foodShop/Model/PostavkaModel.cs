using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class PostavkaModel
    {
        public int number_of_postavka { get; set; }
        public DateTime date_of_postavka { get; set; }

        public PostavkaModel() { }
        public PostavkaModel(Postavka postavka)
        {
            number_of_postavka = postavka.number_of_postavka;
            date_of_postavka = postavka.date_of_postavka;
        }
    }
}
