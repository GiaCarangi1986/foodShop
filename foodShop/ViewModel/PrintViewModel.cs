using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DAL;
using System.Globalization;
using System.Windows;

namespace foodShop
{
    class PrintViewModel : INotifyPropertyChanged
    {
        private DBOperations db;
        private CheckModel check;
        public ObservableCollection<Line_of_checkModel> Line_of_checks { get; set; } //коллекция строк чека

        private decimal? sale; //скидка составила
        private decimal? sum; //сумма покупок чека без учета скидки
        private decimal? itog; //сумма покупок чека со скидкой
        private decimal? nowBonusov; //осталось на карте кол-во бонусов
        private Bonus_cardModel selectedBonusCard;

        public decimal? Sum //сумма покупок чека без учета скидки
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }

        private Visibility vis;
        public Visibility Vis
        {
            get { return vis; }
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
            get
            {
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

                      print.Close();
                  }));
            }
        }

        private Print print;
        public PrintViewModel(Print print, DBOperations db, CheckModel check)
        {
            this.print = print;
            this.db = db;
            this.check = check;
            Line_of_checks = new ObservableCollection<Line_of_checkModel>(db.GetAllLine_of_check(check.number_of_check));

            sum = check.total_cost;
            if (check.number_of_card_FK != null) {
                if (check.bonus != null)
                {
                    sum += check.bonus;
                    sale = check.bonus;
                }
                else sale = 0;
                selectedBonusCard = db.GetBonus_card((int)check.number_of_card_FK);
                if (selectedBonusCard.kolvo_bonusov != null)
                    nowBonusov = selectedBonusCard.kolvo_bonusov;
                vis = Visibility.Visible;
            }
            else
            {
                sale = 0;
                nowBonusov = null;
                if (check.card == true)
                    vis = Visibility.Collapsed;
                else vis = Visibility.Visible;
            }
            itog = check.total_cost;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
