using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DAL;

namespace foodShop
{
    class ThankYouViewModel : INotifyPropertyChanged
    {
        private decimal? sale; //скидка составила
        private decimal? sum; //сумма покупок чека без учета скидки
        private decimal? itog; //сумма покупок чека со скидкой
        private decimal? nowBonusov; //осталось на карте кол-во бонусов
        private double cost; //цена приобретения покупателем бонусной карты

        DBOperations db;

        public decimal? Sum //сумма покупок чека без учета скидки
        {
            get { return sum; }
            set
            {
                    sum = value;
                    OnPropertyChanged("Sum");
            }
        }

        public decimal? Sale //скидка составила
        {
            get { return sale; }
            set
            {
                sale = value;
                OnPropertyChanged("Sale");
            }
        }

        public decimal? Itog //сумма покупок чека со скидкой
        {
            get { return itog; }
            set
            {
                itog = value;
                OnPropertyChanged("Itog");
            }
        }

        public string NowBonusov //осталось на карте кол-во бонусов
        {
            get {
                if (selectedBonusCard == null)
                    return "-";
                return nowBonusov.ToString();
            }
            set
            {
                nowBonusov = Convert.ToDecimal(value);
                OnPropertyChanged("NowBonusov");
            }
        }

        private RelayCommand ok; //ознакомились с подведением итогов о покупке
        public RelayCommand Ok
        {
            get
            {
                return ok ??
                  (ok = new RelayCommand(obj =>
                  {
                      
                      thankYou.Close(); //закрыли текущее окно ThankYou
                      bonusCard.Close();
                  }));
            }
        }

        private Bonus_cardModel selectedBonusCard;
        private CheckModel check;
        private BonusCard bonusCard;
        private ThankYou thankYou;
        public ThankYouViewModel(ThankYou thankYou, BonusCard bonusCard, CheckModel check, Bonus_cardModel selectedBonusCard, DBOperations db, double cost)
        {
            this.thankYou = thankYou;
            this.bonusCard = bonusCard;
            this.check = check;
            this.selectedBonusCard = selectedBonusCard;
            this.db = db;
            this.cost = cost;

            sum = check.total_cost + (decimal?)cost;
            if (selectedBonusCard != null)
            {
                sum += selectedBonusCard.snayli_bonusov;
                sale = selectedBonusCard.snayli_bonusov;
                nowBonusov = selectedBonusCard.kolvo_bonusov;
            }
            else
            {
                sale = 0;
                nowBonusov = null;
            }
            itog = check.total_cost;
            //bonusCard.Hide();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
