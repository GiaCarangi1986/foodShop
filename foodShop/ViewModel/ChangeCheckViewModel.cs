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
    class ChangeCheckViewModel : INotifyPropertyChanged
    {
        private RelayCommand gotovo; //нажали готово (посмотрели итоги покупки)
        public RelayCommand Gotovo
        {
            get
            {
                return gotovo ??
                  (gotovo = new RelayCommand(obj =>
                  {
                      change.Close(); //закрываем окно ChangeCheck
                  }));
            }
        }

        private ChangeCheck change;
        public ChangeCheckViewModel(ChangeCheck change)
        {
            this.change = change;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
