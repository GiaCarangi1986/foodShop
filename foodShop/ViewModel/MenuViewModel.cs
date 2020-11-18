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
        private double statistic;
        bool kassir;

        public ObservableCollection<CheckModel> Checks { get; set; } //коллекция чеков

        public bool TabControlVis //скрывает "списать" и "отчет" для кассира
        {
            get { return kassir; }
            set
            {
                kassir = value;
                OnPropertyChanged("TabControlVis");
            }
        }

        public double ShowStatictic //выводит в Label процентное соотношение покупателей, пользующихся
            //картой, ко всем покупателям
        {
            get { return statistic; }
            set
            {
                statistic = value;
                OnPropertyChanged("ShowStatictic");
            }
        }

        private RelayCommand getStatictic; //нажали ПОЛУЧИТЬ СТАТИСТИКУ
        public RelayCommand GetStatictic
        {
            get
            {
                return getStatictic ??
                  (getStatictic = new RelayCommand(obj =>
                  {
                      double yes = 0;
                      double all = 0;
                      foreach (var item in Checks)
                      {
                          if (item.number_of_card_FK != null)
                              yes += 1;
                          all += 1;
                      }
                      if (all != 0) {
                          statistic = yes / all;
                          ShowStatictic = statistic;
                      }
                  }));
            }
        }

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
                      CheckModel checkModel = db.GetLastCheck();
                      Checks.Insert(0,checkModel);
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
        public MenuViewModel(Menu menu, bool kassir)
        {
            this.menu = menu;
            this.kassir = kassir;

            db = new DBOperations();
            List<CheckModel> checkModels = db.GetAllCheck();
            checkModels.Reverse(); //чтобы сначала видели новые чеки
           Checks = new ObservableCollection<CheckModel>(checkModels);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
