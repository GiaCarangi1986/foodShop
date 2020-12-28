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
                          bool kassir = false;
                          bool starKassir = false;
                          if (user.login == "kassir")
                              kassir = true;
                          else if (user.login == "starKassir")
                              starKassir = true;
                          Menu menu = new Menu(kassir,starKassir);
                          //Menu menu = new Menu();
                          //mainWindow.Close(); //закрываем текущее окно MainWindow
                          Login = null;
                          passwordBox.Password = null;
                          menu.ShowDialog(); //открываем меню (окно Menu)
                      }
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
