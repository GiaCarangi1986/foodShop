using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using DAL;

namespace foodShop
{
    class VxodViewModel : INotifyPropertyChanged
    {
        private foodShopContext foodShop;
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }
        private RelayCommand voiti; //вход
        public RelayCommand Voiti
        {
            get
            {
                return voiti ??
                  (voiti = new RelayCommand(obj =>
                  {
                      var passwordBox = obj as PasswordBox;
                      if (passwordBox == null || passwordBox.Password == "")
                          return;
                      var _password = passwordBox.Password;
                      User user = foodShop.Users.Where(i => i.login == _login).SingleOrDefault();
                      if (user!=null && user.password == _password)
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
            foodShop = new foodShopContext();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    
}
