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
    class MenuViewModel : INotifyPropertyChanged
    {
        private DBOperations db;

        public ObservableCollection<CheckModel> Checks { get; set; } //коллекция чеков

        private RelayCommand addCheck; //нажали ДОБАВИТЬ ЧЕК
        public RelayCommand Add_Check
        {
            get
            {
                return addCheck ??
                  (addCheck = new RelayCommand(obj =>
                  {
                      AddCheck add = new AddCheck();
                      add.ShowDialog(); //будем открывать последовательно окно с добавлением строк
                      //чека и потом окно с добавлением скидочной карты
                      
                  }));
            }
        }

        private RelayCommand changeCheck; //нажали РЕДАКТИРОВАТЬ ЧЕК
        public RelayCommand Change_Check
        {
            get
            {
                return changeCheck ??
                  (changeCheck = new RelayCommand(obj =>
                  {
                      ChangeCheck change = new ChangeCheck();
                      change.ShowDialog(); //открыли окно для редактирования строк чека
                  }));
            }
        }

        private RelayCommand goodbay; //закрыть меню
        public RelayCommand Goodbay
        {
            get
            {
                return goodbay ??
                  (goodbay = new RelayCommand(obj =>
                  {
                      menu.Close(); //закрыли текущее окно Menu
                  }));
            }
        }

        private Menu menu;
        public MenuViewModel(Menu menu)
        {
            this.menu = menu;

            db = new DBOperations();
           Checks = new ObservableCollection<CheckModel>(db.GetAllCheck());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
