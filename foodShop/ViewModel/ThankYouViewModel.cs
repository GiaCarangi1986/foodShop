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
    class ThankYouViewModel : INotifyPropertyChanged
    {
        CheckModel checkModel; //полученный чек

        private RelayCommand ok; //ознакомились с подведением итогов о покупке
        public RelayCommand Ok
        {
            get
            {
                return ok ??
                  (ok = new RelayCommand(obj =>
                  {
                      thankYou.Close(); //закрыли текущее окно ThankYou
                  }));
            }
        }

        private ThankYou thankYou;
        public ThankYouViewModel(ThankYou thankYou)
        {
            this.thankYou = thankYou;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
