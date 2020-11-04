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
    class VxodViewModel : INotifyPropertyChanged
    {
        private RelayCommand voiti; //вход
        public RelayCommand Voiti
        {
            get
            {
                return voiti ??
                  (voiti = new RelayCommand(obj =>
                  {
                      //LoginAndPassword log = new LoginAndPassword();
                      //if (log.Login == "Lizok") //поч не попадает в set из xaml? я там прибиндила все вроде
                      {
                          Menu menu = new Menu(); 
                          mainWindow.Close(); //закрываем текущее окно MainWindow
                          menu.Show(); //открываем меню (окно Menu)
                      }
                  }));
            }
        }

        private RelayCommand goodbay; //закрываем окно входа
        public RelayCommand Goodbay
        {
            get
            {
                return goodbay ??
                  (goodbay = new RelayCommand(obj =>
                  {
                      mainWindow.Close(); //закрыли текущее окно MainWindow (вышли из программы)
                  }));
            }
        }

        private MainWindow mainWindow;
        public VxodViewModel (MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    
}
