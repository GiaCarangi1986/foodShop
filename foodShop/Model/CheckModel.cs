using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class CheckModel 
    {
        public int number_of_check { get; set; }
        public DateTime date_and_time { get; set; }
        public decimal? total_cost { get; set; }
        public int? number_of_card_FK { get; set; }
        public decimal? bonus { get; set; }
        public bool card { get; set; }

        public CheckModel() { }
        public CheckModel(Check check)
        {
            number_of_check = check.number_of_check;
            date_and_time = check.date_and_time;
            total_cost = check.total_cost;
            number_of_card_FK = check.number_of_card_FK;
            bonus = check.bonus;
            card = check.card;
        }
    }
}
