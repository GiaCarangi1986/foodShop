using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace foodShop
{
    class BonusCardViewModel : INotifyPropertyChanged
    {
        private RelayCommand withCard; //ПРИМЕНИТЬ скидку
        public RelayCommand WithCard
        {
            get
            {
                return withCard ??
                  (withCard = new RelayCommand(obj =>
                  {
                      bonusCard.Close(); //закрываем окно BonusCard
                  }));
            }
        }

        private RelayCommand withoutCard; //НЕ ПРИМЕНЯТЬ скидку
        public RelayCommand WithoutCard
        {
            get
            {
                return withoutCard ??
                  (withoutCard = new RelayCommand(obj =>
                  {
                      bonusCard.Close(); //закрываем окно BonusCard
                  }));
            }
        }

        private BonusCard bonusCard;
        public BonusCardViewModel(BonusCard bonusCard)
        {
            this.bonusCard = bonusCard; //используем для последующего закрытия текущего окна
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
