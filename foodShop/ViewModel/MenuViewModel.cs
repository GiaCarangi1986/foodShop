using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;

namespace foodShop
{
    class MenuViewModel : INotifyPropertyChanged
    {
        private DBOperations db;
        private bool kassir, starKassir; //открыть доступ админу или кассиру или старшему кассиру
        private CheckModel selectedCheck; //выбранный чек (для редактировани)
        private List<CheckModel> checkModels; //вспомогательная переменная, которая содержит все чеки в обратном порядке
        private List<CheckModel> checkModel_otchet; //для отчета (содержит чеки для заданного промежутка времени)
        private List<ListCheck> listChecks; //лист Листа-списка чеков (для вывода по дням)
        private ListCheck checkOfList; //лист чеков
        private DateTime date1, date2, date; //1 и 2 даты + промежуточная date для отправки даты с календаря
        public ObservableCollection<CheckModel> Checks { get; set; } //коллекция чеков для отображения в меню
        public ObservableCollection<CheckModel> Check_otchet { get; set; } //коллекция чеков для отчета (по дням все посчитано)
        public ObservableCollection<Line_of_postavkaModel> Procrochka { get; set; } //коллекция строк поставки
        public ObservableCollection<ProductModel> Products { get; set; } //все продукты
        public ObservableCollection<ProsrochkaModel> Procrochka_spisok { get; set; } //коллекция испорченных продуктов

        //переменные для удаления чека
        public ObservableCollection<Line_of_checkModel> Line_of_checks { get; set; } //коллекция строк чека
        public ObservableCollection<Stroka_check_and_postavkaModel> Сheck_and_postavka { get; set; } //коллекция строк поставки,
        //совмещенных со строками чека
        public ObservableCollection<Line_of_postavkaModel> Line_of_postavkas { get; set; } //коллекция строк поставки

        private Visibility vis;
        public Visibility Vis
        {
            get { return vis; }
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
                      checkModel_otchet = db.GetAllCheck().Where(i => i.date_and_time >= date1 && i.date_and_time <= dateTime).ToList();
                      int days = date2.Subtract(date1).Days + 1; //кол-во дней между датами
                      for (int i = 0; i < days; i++)
                      {
                          checkOfList = new ListCheck(date1.AddDays(i));
                          foreach (var test in checkModel_otchet)
                          {
                              if (test.date_and_time.Day == checkOfList.dateTime.Day)
                              {
                                  checkOfList.Add(test);
                              }
                          }
                          listChecks.Add(checkOfList);
                      }

                      foreach (var temp in listChecks)
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
                 (obj) => (date1.Year != 1 && date2.Year != 1 && date1 <= date2)));
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

                      //надо теперь просрочку обновить
                      Procrochka_spisok.Clear();
                      ChangeProsrochka();
                  },
                 //условие, при котором будет доступна команда:
                 //разница даты покупки и текущей даты не более 5 часов
                 (obj) => (selectedCheck != null && DateTime.Now.Subtract(selectedCheck.date_and_time).Days == 0
                 && DateTime.Now.Subtract(selectedCheck.date_and_time).Hours <= 4 && selectedCheck.total_cost != 0
                 && selectedCheck.card == false && selectedCheck.total_cost!=null)));
            }
        }

        public bool TabControlVis //скрывает "списать" и "отчет" для кассира
        {
            get {
                bool noAdmin=true;
                if (!kassir && !starKassir)
                    noAdmin = false;
                return noAdmin; }
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
                      //AddCheck add = new AddCheck();
                      add.ShowDialog(); //будем открывать последовательно окно с добавлением строк
                      //чека и потом окно с добавлением скидочной карты

                      CheckModel checkModel = db.GetLastCheck();
                      if (checkModel.card)
                      {
                          CheckModel checkModel1 = db.GetPredLastCheck();
                          Checks.Insert(0, checkModel1);
                      }
                      Checks.Insert(0, checkModel);

                      //надо теперь просрочку обновить
                      Procrochka_spisok.Clear();
                      ChangeProsrochka();
                  }));
            }
        }

        private RelayCommand delete_Check; //нажали УДАЛИТЬ ЧЕК
        public RelayCommand Delete_Check
        {
            get
            {
                return delete_Check ??
                  (delete_Check = new RelayCommand(obj =>
                  {
                      Line_of_checks = new ObservableCollection<Line_of_checkModel>(db.GetAllLine_of_check(selectedCheck.number_of_check));
                      Сheck_and_postavka = new ObservableCollection<Stroka_check_and_postavkaModel>(db.GetAllStrokaCheckAndPostavka());

                      var result = Line_of_checks.Join(Сheck_and_postavka, // второй набор
                 lc => lc.line_number_of_check, // свойство-селектор объекта из первого набор
                 pc => pc.id_stroka_check, // свойство-селектор объекта из второго набора
                 (lc, pc) => new {
                     line_number_of_postavka = pc.id_stroka_postavka,
                     line_number_of_check = lc.line_number_of_check,
                     kolvo = pc.kolvo_product_in_stroka_postavka,
                     id = pc.id,
                     number_of_check = lc.number_of_check_FK,
                     much_of_products = lc.much_of_products
                 }); // результат

                      foreach (var item in result.Where(i => i.number_of_check == selectedCheck.number_of_check))
                      {
                          int nowKolvo = item.much_of_products;
                          if (item.number_of_check == selectedCheck.number_of_check)
                          {
                              if (item.kolvo >= nowKolvo)
                              {
                                  Line_of_postavkaModel postavkaModel = db.GetLine_of_postavka(item.line_number_of_postavka);
                                  postavkaModel.ostalos_product += nowKolvo;
                                  db.UpdateLine_of_postavka(postavkaModel);
                                  Stroka_check_and_postavkaModel stroka = db.GetStrokaCheckAndPostavka(item.id);
                                  db.DeleteLine_of_check_and_postavka(stroka.id);
                                  //break; исключение в delete можно сделать и глянуть что там с редактированием
                              }
                              else
                              {
                                  Line_of_postavkaModel postavkaModel = db.GetLine_of_postavka(item.line_number_of_postavka);
                                  postavkaModel.ostalos_product += item.kolvo;
                                  nowKolvo -= item.kolvo;
                                  db.UpdateLine_of_postavka(postavkaModel);
                                  Stroka_check_and_postavkaModel stroka = db.GetStrokaCheckAndPostavka(item.id);
                                  db.DeleteLine_of_check_and_postavka(stroka.id);
                              }
                          }
                      }

                      foreach (var temp in Line_of_checks.Where(i => i.number_of_check_FK == selectedCheck.number_of_check))
                      {
                          Line_of_checkModel line_Of_CheckModel = db.GetLine_of_check(temp.line_number_of_check);
                          db.DeleteLine_of_check(line_Of_CheckModel.line_number_of_check);
                      }
                      db.DeleteCheck(selectedCheck.number_of_check);
                      Checks.Remove(selectedCheck);
                  },
                 //условие, при котором будет доступна команда:
                 //разница даты покупки и текущей даты не более 5 часов
                 (obj) => (selectedCheck != null && DateTime.Now.Subtract(selectedCheck.date_and_time).Days == 0
                 && DateTime.Now.Subtract(selectedCheck.date_and_time).Hours <= 4 && selectedCheck.card == false)));
            }
        }

        private RelayCommand print_Check; //нажали ВЕРСИЯ ДЛЯ ПЕЧАТИ
        public RelayCommand Print_Check
        {
            get
            {
                return print_Check ??
                  (print_Check = new RelayCommand(obj =>
                  {
                      Print print = new Print(db, selectedCheck);
                      //AddCheck add = new AddCheck();
                      print.ShowDialog(); //будем открывать последовательно окно с версией печати
                  },
                 //условие, при котором будет доступна команда
                 (obj) => (selectedCheck != null && selectedCheck.total_cost != null)));
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

                      foreach (var temp in Procrochka_spisok.Where(i => i.isSelected))
                          db.SpisatProsrochka(temp.line_of_postavka);

                      //надо теперь просрочку обновить
                      Procrochka_spisok.Clear();
                      ChangeProsrochka();
                  },
                 //условие, при котором будет доступна команда:
                 //список просрочки не пуст и есть хотя бы 1 выбранный элемент
                 (obj) => (Procrochka_spisok.Count() > 0 && Procrochka_spisok.Where(i => i.isSelected).ToList().Count > 0)));
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
                 }).OrderBy(i => i.title); // результат

            foreach (var temp in result)
            {
                if (temp.spisano == false && temp.ostalos_product != 0)
                {
                    if (temp.scor_godnosti_O != null)
                        if (DateTime.Now.Subtract((DateTime)temp.date_of_preparing).Days+1
                            > (temp.scor_godnosti_O / 24))
                        {
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
        public MenuViewModel(Menu menu, bool kassir, DBOperations db, bool starKassir)
        {
            this.menu = menu;
            this.kassir = kassir;
            this.starKassir = starKassir;
            if (!this.kassir)
                vis = Visibility.Visible;
            else vis = Visibility.Collapsed;
            this.db = db;
            //db = new DBOperations();
            checkModels = db.GetAllCheck(); //сначала берем все чеки
            checkModels.Reverse(); //чтобы сначала видели новые чеки (обратный порядок чеков)
            Checks = new ObservableCollection<CheckModel>(checkModels); //в коллекции чеки в обратном порядке
            Check_otchet = new ObservableCollection<CheckModel>();
            Date = DateTime.Parse(DateTime.Now.ToShortDateString()); //в календаре отображается текущая дата
            Date1 = DateTime.Parse(DateTime.Now.ToShortDateString());
            Date2 = DateTime.Parse(DateTime.Now.ToShortDateString());
            listChecks = new List<ListCheck>();

            Procrochka_spisok = new ObservableCollection<ProsrochkaModel>();

            //получаем строки чека, где продукты не списаны
            Procrochka = new ObservableCollection<Line_of_postavkaModel>(db.GetAllLine_of_postavka().Where(i => i.spisano == false).ToList());

            //получаем все продукты
            Products = new ObservableCollection<ProductModel>(db.GetAllProduct()); //все, кроме бонусной карты

            ChangeProsrochka();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
