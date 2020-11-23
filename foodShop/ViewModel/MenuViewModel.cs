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
        private bool kassir;
        private CheckModel selectedCheck;
        List<CheckModel> checkModels;
        List<CheckModel> checkModel_otchet;
        private decimal? sum, bon;

        private DateTime date1, date2, date; //1 и 2 даты + промежуточная date для отправки даты с календаря
        public ObservableCollection<CheckModel> Checks { get; set; } //коллекция чеков для отображения в меню
        public ObservableCollection<CheckModel> Check_otchet { get; set; } //коллекция чеков для отчета

        public decimal? Sum //подводим итоговую сумму чеков
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }

        public decimal? Bon //подводим кол-во бонусов
        {
            get { return bon; }
            set
            {
                bon = value;
                OnPropertyChanged("Bon");
            }
        }

        public DateTime Date //получим дату из календаря
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private RelayCommand okDate2; //нажали ок (для 2 даты)
        public RelayCommand OkDate2
        {
            get
            {
                return okDate2 ??
                  (okDate2 = new RelayCommand(obj =>
                  {
                      date2 = date;
                      Date2 = date2;
                  }));
            }
        }

        private RelayCommand okDate1; //нажали ок (для 1 даты)
        public RelayCommand OkDate1
        {
            get
            {
                return okDate1 ??
                  (okDate1 = new RelayCommand(obj =>
                  {
                      date1 = date;
                      Date1 = date1;
                  }));
            }
        }

        private RelayCommand getOtchet; //нажали ПОЛУЧИТЬ (отчет)
        public RelayCommand GetOtchet
        {
            get
            {
                return getOtchet ??
                  (getOtchet = new RelayCommand(obj =>
                  {
                      //сначала почистим коллекцию
                      if (checkModel_otchet != null)
                          Check_otchet.Clear();

                      DateTime dateTime = date2.AddDays(1);
                      checkModel_otchet = db.GetAllCheck().Where(i => i.date_and_time >= date1 && i.date_and_time<=dateTime).ToList();
                      //Check_otchet= new ObservableCollection<CheckModel>(db.GetAllCheck().Where(i=>i.date_and_time>=date1&&
                      //i.date_and_time<=dateTime));
                      sum = bon = 0;
                      foreach (var temp in checkModel_otchet)
                      {
                          var test = temp;
                          sum += temp.total_cost;
                          if (temp.bonus == null)
                              test.bonus = 0;
                          else bon += temp.bonus;
                          Check_otchet.Add(test);
                      }
                      Sum = sum;
                      Bon = bon;
                  },
                 //условие, при котором будет доступна команда:
                 //дата 1 <= дата 2
                 (obj) => (date1.Year!=1 && date2.Year != 1 && date1<=date2)));
            }
        }

        public DateTime Date1 //получим 1 дату
        {
            get { return date1; }
            set
            {
                    date1 = value;
                    OnPropertyChanged("Date1");
            }
        }

        public DateTime Date2 //получим 2 дату
        {
            get { return date2; }
            set
            {
                
                    date2 = value;
                    OnPropertyChanged("Date2");
            }
        }

        public CheckModel SelectedCheck //получим выбранный чек
        {
            get { return selectedCheck; }
            set
            {
                selectedCheck = value;
                OnPropertyChanged("SelectedCheck");
            }
        }

        private RelayCommand change_Check; //нажали РЕДАКТИРОВАТЬ ЧЕК
        public RelayCommand Change_Check
        {
            get
            {
                return change_Check ??
                  (change_Check = new RelayCommand(obj =>
                  {
                      int index = Checks.IndexOf(selectedCheck);
                      ChangeCheck change = new ChangeCheck(selectedCheck, db);
                      change.ShowDialog(); //открыли окно для редактирования строк чека
                      selectedCheck = db.GetCheck(selectedCheck.number_of_check); //выниает снова прошлое значение
                      Checks[index] = selectedCheck;
                      //Checks.RemoveAt(index);
                      //Checks.Insert(index, check);
                  },
                 //условие, при котором будет доступна команда:
                 //разница даты покупки и текущей даты не более 1 дня
                 (obj) => (selectedCheck!=null && DateTime.Now.Subtract(selectedCheck.date_and_time).Days==0
                 && DateTime.Now.Subtract(selectedCheck.date_and_time).Hours<=5 && selectedCheck.total_cost!=0)));
            }
        }

        public bool TabControlVis //скрывает "списать" и "отчет" для кассира
        {
            get { return kassir; }
            set
            {
                kassir = value;
                OnPropertyChanged("TabControlVis");
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
                      AddCheck add = new AddCheck(db);
                      add.ShowDialog(); //будем открывать последовательно окно с добавлением строк
                      //чека и потом окно с добавлением скидочной карты
                      CheckModel checkModel = db.GetLastCheck();
                      Checks.Insert(0,checkModel);
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
        public MenuViewModel(Menu menu, bool kassir, DBOperations db)
        {
            this.menu = menu;
            this.kassir = kassir;
            this.db = db;
            //db = new DBOperations();
            //буду показывать чеки только за последний день
            checkModels = db.GetAllCheck().Where(i=> DateTime.Now.Subtract(i.date_and_time).Days == 0).ToList();
            checkModels.Reverse(); //чтобы сначала видели новые чеки
           Checks = new ObservableCollection<CheckModel>(checkModels);
            Check_otchet = new ObservableCollection<CheckModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
