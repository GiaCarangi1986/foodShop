using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class Bonus_cardModel : IComparable
    {
        public int number_of_card { get; set; }
        public decimal? kolvo_bonusov { get; set; }
        public decimal? snayli_bonusov { get; set; } //был int?

        public Bonus_cardModel() { }
        public Bonus_cardModel(Bonus_card bonus)
        {
            number_of_card = bonus.number_of_card;
            kolvo_bonusov = bonus.kolvo_bonusov;
        }

        public int CompareTo(object o)
        {
            Bonus_cardModel b = o as Bonus_cardModel;
            if (b != null)
                return this.number_of_card.CompareTo(b.number_of_card);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}
