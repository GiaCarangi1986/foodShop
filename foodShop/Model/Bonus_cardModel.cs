using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    class Bonus_cardModel
    {
        public int number_of_card { get; set; }
        public int? kolvo_bonusov { get; set; }

        public Bonus_cardModel() { }
        public Bonus_cardModel(Bonus_card bonus)
        {
            number_of_card = bonus.number_of_card;
            kolvo_bonusov = bonus.kolvo_bonusov;
        }
    }
}
