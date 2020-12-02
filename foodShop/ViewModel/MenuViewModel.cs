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
        private bool kassir; //открыть доступ админу или кассиру
        private CheckModel selectedCheck; //выбранный чек (для редактировани)
        private List<CheckModel> checkModels; //вспомогательная переменная, которая содержит все чеки в обратном порядке
        private List<CheckModel> checkModel_otchet; //для отчета (содержит чеки для заданного промежутка времени)
        //private decimal? sum, bon; //реализовано в графике
        private List<ListCheck> listChecks; //лист Листа-списка чеков (для вывода по дням)
        private ListCheck checkOfList; //лист чеков
        private DateTime date1, date2, date; //1 и 2 даты + промежуточная date для отправки даты с календаря
        public ObservableCollection<CheckModel> Checks { get; set; } //коллекция чеков для отображения в меню
        public ObservableCollection<CheckModel> Check_otchet { get; set; } //коллекция чеков для отчета (по дням все посчитано)
        public ObservableCollection<Line_of_postavkaModel> Procrochka { get; set; } //коллекция строк поставки
        public ObservableCollection<ProductModel> Products { get; set; } //все продукты
        public ObservableCollection<ProsrochkaModel> Procrochka_spisok { get; set; } //коллекция испорченных продуктов

        /*public decimal? Sum //подводим итоговую сумму чеков
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
        }*/

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
                      //date2 = date;
                      //Date2 = date2;
                      Date2 = date;
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
                      //date1 = date;
                      //Date1 = date1;
                      Date1 = date;
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
                      if (listChecks != null)
                          listChecks.Clear();

                      DateTime dateTime = date2.AddDays(1);
                      checkModel_otchet = db.GetAllCheck().Where(i => i.date_and_time >= date1 && i.date_and_time<=dateTime).ToList();
                      int days = date2.Subtract(date1).Days + 1; //кол-во дней между датами
                      for (int i = 0; i < days; i++)
                      {
                          checkOfList = new ListCheck(date1.AddDays(i));
                          foreach(var test in checkModel_otchet)
                          {
                              if (test.date_and_time.Day == checkOfList.dateTime.Day)
                              {
                                  checkOfList.Add(test);
                              }
                          }
                          listChecks.Add(checkOfList);
                      }

                      foreach(var temp in listChecks)
                      {
                          decimal sum = 0;
                          decimal bonus = 0;
                          sum = temp.Sum();
                          bonus = temp.Bonus();
                          CheckModel check = new CheckModel();
                          check.total_cost = sum;
                          check.bonus = bonus;
                          check.date_and_time = temp.dateTime;
                          Check_otchet.Add(check);
                      }
                      Graph graph = new Graph(Check_otchet);
                      graph.ShowDialog();
                      
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

                      //надо теперь просрочку обновить
                      Procrochka_spisok.Clear();
                      ChangeProsrochka();
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

                      //надо теперь просрочку обновить
                      Procrochka_spisok.Clear();
                      ChangeProsrochka();
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

        private RelayCommand spicat; //нажали СПИСАТЬ
        public RelayCommand Spicat
        {
            get
            {
                return spicat ??
                  (spicat = new RelayCommand(obj =>
                  {

                          foreach (var temp in Procrochka_spisok.Where(i=>i.isSelected))
                          db.SpisatProsrochka(temp.line_of_postavka);

                      //надо теперь просрочку обновить
                      Procrochka_spisok.Clear();
                      ChangeProsrochka();
                  },
                 //условие, при котором будет доступна команда:
                 //список просрочки не пуст и есть хотя бы 1 выбранный элемент
                 (obj) => (Procrochka_spisok.Count()>0 && Procrochka_spisok.Where(i => i.isSelected).ToList().Count > 0)));
            }
        }

        private RelayCommand all_Empty; //нажали ВЫБРАТЬ ВСЕ / СНЯТЬ ВЫБОР
        public RelayCommand All_Empty
        {
            get
            {
                return all_Empty ??
                  (all_Empty = new RelayCommand(obj =>
                  {
                      foreach (var temp in Procrochka_spisok)
                          temp.isSelected = !temp.isSelected;
                      /*if (all_or_nothing == false) //не выбраны элементы
                      {
                          foreach (var temp in Procrochka_spisok)
                              temp.isSelected = true;
                          all_or_nothing = true; //все элементы выбраны
                      }
                      else
                      {
                          foreach (var temp in Procrochka_spisok)
                              temp.isSelected = false;
                          all_or_nothing = false; //ничто не выбрано
                      }*/
                  },
                 //условие, при котором будет доступна команда
                 (obj) => (Procrochka_spisok.Count() > 0)));
            }
        }

        private void ChangeProsrochka()
        {
            //получаем строки чека, где продукты не списаны
            Procrochka = new ObservableCollection<Line_of_postavkaModel>(db.GetAllLine_of_postavka().Where(i => i.spisano == false).ToList());

            //получаем все продукты
            Products = new ObservableCollection<ProductModel>(db.GetAllProduct());

            //объединть надо строку поставки и продукт, чтобы выбрать те строки поставки, где продукт просрочился
            var result = Procrochka.Join(Products, // второй набор
                 post => post.code_of_product_FK, // свойство-селектор объекта из первого набор
                 prod => prod.code_of_product, // свойство-селектор объекта из второго набора
                 (post, prod) => new {
                     code_of_product_FK = post.code_of_product_FK,
                     ostalos_product = post.ostalos_product,
                     date_of_preparing = post.date_of_preparing,
                     spisano = post.spisano,
                     scor_godnosti_O = prod.scor_godnosti_O,
                     line_of_postavka = post.line_of_postavka,
                     number_of_postavka_FK = post.number_of_postavka_FK,
                     title = prod.title
                 }).OrderBy(i=>i.title); // результат

            foreach (var temp in result)
            {
                if (temp.spisano == false && temp.ostalos_product != 0)
                {
                    /*if (temp.scor_godnosti_O == null)
                    {
                        //Line_of_postavkaModel line_Of_Postavka = db.GetLine_of_postavka(temp.line_of_postavka);
                        ProsrochkaModel prosrochka = new ProsrochkaModel();
                        prosrochka.code_of_product_FK = temp.code_of_product_FK;
                        prosrochka.date_of_preparing = temp.date_of_preparing;
                        prosrochka.line_of_postavka = temp.line_of_postavka;
                        prosrochka.ostalos_product = temp.ostalos_product;
                        prosrochka.scor_godnosti_O = temp.scor_godnosti_O;
                        prosrochka.spisano = temp.spisano;
                        prosrochka.title = temp.title;
                        prosrochka.number_of_postavka_FK = temp.number_of_postavka_FK;
                        Procrochka_spisok.Add(prosrochka);
                    }
                    else*/
                        if (DateTime.Now.Subtract((DateTime)temp.date_of_preparing).Days
                            > (temp.scor_godnosti_O / 24))
                    {
                        //Line_of_postavkaModel line_Of_Postavka = db.GetLine_of_postavka(temp.line_of_postavka);
                        ProsrochkaModel prosrochka = new ProsrochkaModel();
                        prosrochka.code_of_product_FK = temp.code_of_product_FK;
                        prosrochka.date_of_preparing = temp.date_of_preparing;
                        prosrochka.line_of_postavka = temp.line_of_postavka;
                        prosrochka.ostalos_product = temp.ostalos_product;
                        prosrochka.scor_godnosti_O = temp.scor_godnosti_O;
                        prosrochka.spisano = temp.spisano;
                        prosrochka.title = temp.title;
                        prosrochka.number_of_postavka_FK = temp.number_of_postavka_FK;
                        Procrochka_spisok.Add(prosrochka);
                    }
                }
            }
        }

        private Menu menu;
        public MenuViewModel(Menu menu, bool kassir, DBOperations db)
        {
            this.menu = menu;
            this.kassir = kassir;
            this.db = db;
            //db = new DBOperations();
            checkModels = db.GetAllCheck(); //сначала берем все чеки
            checkModels.Reverse(); //чтобы сначала видели новые чеки (обратный порядок чеков)
           Checks = new ObservableCollection<CheckModel>(checkModels); //в коллекции чеки в обратном порядке
            Check_otchet = new ObservableCollection<CheckModel>();
            Date = DateTime.Parse(DateTime.Now.ToShortDateString()); //в календаре отображается текущая дата
            listChecks = new List<ListCheck>();

            Procrochka_spisok = new ObservableCollection<ProsrochkaModel>();

            //получаем строки чека, где продукты не списаны
            Procrochka = new ObservableCollection<Line_of_postavkaModel>(db.GetAllLine_of_postavka().Where(i => i.spisano == false).ToList());

            //получаем все продукты
            Products = new ObservableCollection<ProductModel>(db.GetAllProduct());

            ChangeProsrochka();
            ////объединть надо строку поставки и продукт, чтобы выбрать те строки поставки, где продукт просрочился
            //var result = Procrochka.Join(Products, // второй набор
            //     post => post.code_of_product_FK, // свойство-селектор объекта из первого набор
            //     prod => prod.code_of_product, // свойство-селектор объекта из второго набора
            //     (post, prod) => new {
            //         code_of_product_FK = post.code_of_product_FK,
            //         ostalos_product = post.ostalos_product,
            //         date_of_preparing = post.date_of_preparing,
            //         spisano = post.spisano,
            //         scor_godnosti_O = prod.scor_godnosti_O,
            //         line_of_postavka = post.line_of_postavka,
            //         number_of_postavka_FK = post.number_of_postavka_FK,
            //         title = prod.title
            //     }); // результат

            //foreach (var temp in result)
            //{
            //    if (temp.spisano == false && temp.ostalos_product != 0)
            //    {
            //        if (temp.scor_godnosti_O == null)
            //        {
            //            //Line_of_postavkaModel line_Of_Postavka = db.GetLine_of_postavka(temp.line_of_postavka);
            //            ProsrochkaModel prosrochka = new ProsrochkaModel();
            //            prosrochka.code_of_product_FK = temp.code_of_product_FK;
            //            prosrochka.date_of_preparing = temp.date_of_preparing;
            //            prosrochka.line_of_postavka = temp.line_of_postavka;
            //            prosrochka.ostalos_product = temp.ostalos_product;
            //            prosrochka.scor_godnosti_O = temp.scor_godnosti_O;
            //            prosrochka.spisano = temp.spisano;
            //            prosrochka.title = temp.title;
            //            prosrochka.number_of_postavka_FK = temp.number_of_postavka_FK;
            //            Procrochka_spisok.Add(prosrochka);
            //        }
            //        else
            //            if (DateTime.Now.Subtract((DateTime)temp.date_of_preparing).Days
            //                < (temp.scor_godnosti_O / 24))
            //        {
            //            //Line_of_postavkaModel line_Of_Postavka = db.GetLine_of_postavka(temp.line_of_postavka);
            //            ProsrochkaModel prosrochka = new ProsrochkaModel();
            //            prosrochka.code_of_product_FK = temp.code_of_product_FK;
            //            prosrochka.date_of_preparing = temp.date_of_preparing;
            //            prosrochka.line_of_postavka = temp.line_of_postavka;
            //            prosrochka.ostalos_product = temp.ostalos_product;
            //            prosrochka.scor_godnosti_O = temp.scor_godnosti_O;
            //            prosrochka.spisano = temp.spisano;
            //            prosrochka.title = temp.title;
            //            prosrochka.number_of_postavka_FK = temp.number_of_postavka_FK;
            //            Procrochka_spisok.Add(prosrochka);
            //        }
            //    }
            //}

            //Procrochka = new ObservableCollection<Line_of_postavkaModel>(db.GetAllLine_of_postavka().Where(i=>i.spisano==false
            //&&DateTime.Now.Subtract((DateTime)i.date_of_preparing)<=i.));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
