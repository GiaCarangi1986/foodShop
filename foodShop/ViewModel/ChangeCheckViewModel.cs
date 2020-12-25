using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace foodShop
{
    class ChangeCheckViewModel : INotifyPropertyChanged
    {
        private CheckModel check;
        private Line_of_checkModel selectedLine_of_check;
        private DBOperations db;
        private string title;
        private int? vvodMax; //желаемое кол-во продуктов
        private int nowKolvo; //столько хотим вычесть
        private int max;
        public ObservableCollection<Line_of_checkModel> Line_of_checks { get; set; } //коллекция строк чека
        public ObservableCollection<Stroka_check_and_postavkaModel> Сheck_and_postavka { get; set; } //коллекция строк поставки,
        //совмещенных со строками чека
        public ObservableCollection<Line_of_postavkaModel> Line_of_postavkas { get; set; } //коллекция строк поставки

        public int? VvodMax //ввод желаемого кол-ва товара (д.б. < текущего кол-ва)
        {
            get { return vvodMax; }
            set
            {
                if (selectedLine_of_check != null)
                {
                    vvodMax = value;
                    nowKolvo = selectedLine_of_check.much_of_products - (int)vvodMax;
                    OnPropertyChanged("VvodMax");
                }
            }
        }

        public int Max //отправляем в текстблок максимально возможное кол-во товара
        {
            get { return max; }
            set
            {
                if (selectedLine_of_check != null)
                {
                    max = value;
                    OnPropertyChanged("Max");
                }
            }
        }

        public string Title //название продукта отобразим в label из списка
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public Line_of_checkModel SelectedLine_of_check //получим выбранный продукт
        {
            get { return selectedLine_of_check; }
            set
            {
                if (value != null)
                {
                    selectedLine_of_check = value;
                    title = SelectedLine_of_check.name_of_product;
                    Title = title;
                    max = (int)selectedLine_of_check.much_of_products;
                    Max = max;
                    OnPropertyChanged("SelectedLine_of_check");
                }
            }
        }

        // команда изменения строки чека
        private RelayCommand changeLine_of_check;
        public RelayCommand ChangeLine_of_check
        {
            get
            {
                return changeLine_of_check ??
                  (changeLine_of_check = new RelayCommand(obj =>
                  {
                      CheckModel check = db.GetCheck(selectedLine_of_check.number_of_check_FK);
                      nowKolvo = selectedLine_of_check.much_of_products - (int)vvodMax;
                      decimal oldItogo = selectedLine_of_check.itogo; //старая сумма строки чека

                      var result = Line_of_checks.Join(Сheck_and_postavka, // второй набор
                 lc => lc.line_number_of_check, // свойство-селектор объекта из первого набор
                 pc => pc.id_stroka_check, // свойство-селектор объекта из второго набора
                 (lc, pc) => new {
                     line_number_of_postavka = pc.id_stroka_postavka,
                     line_number_of_check = lc.line_number_of_check,
                     kolvo = pc.kolvo_product_in_stroka_postavka,
                     id = pc.id
                 }); // результат

                      foreach (var item in result.Where(i => i.line_number_of_check == selectedLine_of_check.line_number_of_check))
                      {
                          
                            if (item.kolvo >= nowKolvo)
                            {
                              Line_of_postavkaModel postavkaModel = db.GetLine_of_postavka(item.line_number_of_postavka);
                              postavkaModel.ostalos_product += nowKolvo;
                              db.UpdateLine_of_postavka(postavkaModel);
                              Stroka_check_and_postavkaModel stroka = db.GetStrokaCheckAndPostavka(item.id);
                              stroka.kolvo_product_in_stroka_postavka -= nowKolvo;
                              db.UpdateStrokaCheckAndPostavka(stroka);
                              selectedLine_of_check.much_of_products -= nowKolvo;
                              break;
                            }
                            else
                            {
                              Line_of_postavkaModel postavkaModel = db.GetLine_of_postavka(item.line_number_of_postavka);
                              postavkaModel.ostalos_product += item.kolvo;
                              nowKolvo -= item.kolvo;
                              db.UpdateLine_of_postavka(postavkaModel);
                              Stroka_check_and_postavkaModel stroka = db.GetStrokaCheckAndPostavka(item.id);
                              stroka.kolvo_product_in_stroka_postavka = 0;
                              db.UpdateStrokaCheckAndPostavka(stroka);
                              selectedLine_of_check.much_of_products -= item.kolvo;
                            }

                      }

                      int index = Line_of_checks.IndexOf(selectedLine_of_check);
                      max = selectedLine_of_check.much_of_products;
                      vvodMax = max;
                      Max = max;
                      selectedLine_of_check.itogo = selectedLine_of_check.much_of_products * selectedLine_of_check.cost_for_buyer;
                      Line_of_checks.RemoveAt(index); //удалим старую строку чека
                      Line_of_checks.Insert(index, selectedLine_of_check); //вставим на ее место измненную
                      db.UpdateLine_of_check(selectedLine_of_check);

                      check.total_cost -= oldItogo;
                      check.total_cost += selectedLine_of_check.itogo;
                      if (check.total_cost < 0) //бонусы пропадают (молоко стоит 50, бонусами оплатил пусть полностью, т е 50, чек по сути
                          check.total_cost = 0; //сейчас = 0 и в минус не пойдет после вычета молока
                      db.UpdateCheck(check);
                  },
                 //условие, при котором будет доступна команда
                 (obj) => (vvodMax < max && vvodMax >= 0 && selectedLine_of_check!=null)));
            }
        }

        private RelayCommand gotovo; //нажали готово (завершили редактироание чека)
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
        public ChangeCheckViewModel(ChangeCheck change, CheckModel check, DBOperations db)
        {
            this.change = change;
            this.check = check;
            this.db = db;
            Line_of_checks = new ObservableCollection<Line_of_checkModel>(db.GetAllLine_of_check(check.number_of_check));
            Line_of_postavkas = new ObservableCollection<Line_of_postavkaModel>(db.GetAllLine_of_postavka());
            Сheck_and_postavka = new ObservableCollection<Stroka_check_and_postavkaModel>(db.GetAllStrokaCheckAndPostavka());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
