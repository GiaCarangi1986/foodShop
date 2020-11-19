using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DAL;
using System.Globalization;

namespace foodShop
{
    class AddCheckViewModel : INotifyPropertyChanged
    {
        private Line_of_checkModel lcheck; //хранит строку чека
        private Line_of_postavkaModel lpostavka; //хранит строку поставки
        private DBOperations db;
        private ProductModel selectedProduct; //хранит выбранный в combox продукт
        private CheckModel check; //создает новый чек, куда впишем строки чека
        private Stroka_check_and_postavkaModel check_and_postavka; //связывает строку чека и строку поставки
        private int max; //хранит максимально доступное кол-во товара
        private int? vvodMax; //пользовательский ввод кол-ва продуктов
        private int sumInCheck; //итоговая сумма чека
        private decimal price;

        public ObservableCollection<ProductModel> Products { get; set; } //коллекция продуктов
        public ObservableCollection<Line_of_checkModel> Line_of_checks { get; set; } //коллекция строк чека
        public ObservableCollection<Line_of_postavkaModel> Line_of_postavkas { get; set; } //коллекция строк поставки

        public string Price //указывается цена товара
        {
            get {
                //var test = price.ToString("N", CultureInfo.InvariantCulture);
                return price.ToString("0.00"); }
            set
            {
                    price = Convert.ToDecimal(value);
                    OnPropertyChanged("Price");
            }
        }

        public int? VvodMax //ввод желаемого кол-ва товаров
        {
            get {  return vvodMax; }
            set
            {
                if (selectedProduct != null) {
                    vvodMax = value;
                    OnPropertyChanged("VvodMax");
                }
            }
        }

        public int Max //отправляем в текстблок максимально возможное кол-во товара
        {
            get { return max; }
            set
            {
                max = value;
               OnPropertyChanged("Max");
            }
        }
         
        public ProductModel SelectedProduct //запомнить продукт, выбранный в combobox
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                max = (int)selectedProduct.all_kolvo;
                Max = max;
                price = selectedProduct.now_cost;
                Price = price.ToString();
                OnPropertyChanged("SelectedProduct");
            }
        }

       // команда добавления новой строки чека
        private RelayCommand addLine_of_check;
        public RelayCommand AddLine_of_check
        {
            get
            {
                return addLine_of_check ??
                  (addLine_of_check = new RelayCommand(obj =>
                  {
                      lcheck = new Line_of_checkModel();
                      lcheck.code_of_product_FK = selectedProduct.code_of_product;
                      lcheck.number_of_check_FK = check.number_of_check;
                      lcheck.cost_for_buyer = selectedProduct.now_cost;
                      lcheck.much_of_products = vvodMax.Value;
                      lcheck.name_of_product = selectedProduct.title;
                      lcheck.itogo = selectedProduct.now_cost * vvodMax.Value;

                      sumInCheck += (int)lcheck.itogo;

                      //совместить тут надо 2 строки чека, если уже есть товар с такими данными
                      bool update = false;
                      foreach (var item in Line_of_checks)
                      {
                          if (item.code_of_product_FK == lcheck.code_of_product_FK)
                          {
                              Line_of_checkModel model = db.GetLine_of_check((int)item.line_number_of_check);
                              model.much_of_products += lcheck.much_of_products;
                              model.itogo += lcheck.itogo;

                              Line_of_checks.Remove(item); //удалим первое вхождение старой строки чека
                              Line_of_checks.Insert(Line_of_checks.Count, model); //добавим в конец обновленную строку чека

                              db.UpdateLine_of_check(model); //обновим кол-во продуктов и итог стоимость у строки чека в бд
                              update = true;
                              break;
                          }
                      }
                      if (!update)
                      {
                          
                          lcheck.line_number_of_check = db.CreateLine_of_check(lcheck); //в бд сохраним новую строку чека
                          Line_of_checks.Insert(Line_of_checks.Count, lcheck); //добавить строку чека в поле слева
                      }

                      //уменьшить кол-во в строке поставки

                          var result = Line_of_checks.Join(Line_of_postavkas, // второй набор
                 lc => lc.code_of_product_FK, // свойство-селектор объекта из первого набор
                 pc => pc.code_of_product_FK, // свойство-селектор объекта из второго набора
                 (lc, pc) => new { ostalos_product = pc.ostalos_product, number_of_check = lc.number_of_check_FK,
                 line_of_postavka = pc.line_of_postavka, line_number_of_check = lc.line_number_of_check}); // результат

                      check_and_postavka = new Stroka_check_and_postavkaModel();
                      //подумать над вторым выражением после "и"
                  foreach (var item in result.Where(i=>i.ostalos_product>0 && i.number_of_check==lcheck.number_of_check_FK)) 
                      {
                          if (item.ostalos_product>= vvodMax)
                          {
                              Line_of_postavkaModel pline = db.GetLine_of_postavka(item.line_of_postavka);
                              pline.ostalos_product -= vvodMax;
                              db.UpdateLine_of_postavka(pline);
                              selectedProduct.all_kolvo -= vvodMax;
                              max = (int)selectedProduct.all_kolvo;
                              Max = max;
                              check_and_postavka.id_stroka_check = item.line_number_of_check;
                              check_and_postavka.id_stroka_postavka = pline.line_of_postavka;
                              check_and_postavka.kolvo_product_in_stroka_postavka = (int)vvodMax;
                              db.CreateStroka_check_and_postavka(check_and_postavka);
                              break;
                          }
                          else
                          {
                              Line_of_postavkaModel pline = db.GetLine_of_postavka(item.line_of_postavka);
                              vvodMax -= pline.ostalos_product;
                              selectedProduct.all_kolvo -= vvodMax;
                              pline.ostalos_product = 0;
                              db.UpdateLine_of_postavka(pline);
                              check_and_postavka.id_stroka_check = item.line_number_of_check;
                              check_and_postavka.id_stroka_postavka = pline.line_of_postavka;
                              check_and_postavka.kolvo_product_in_stroka_postavka = (int)vvodMax;
                              db.CreateStroka_check_and_postavka(check_and_postavka);
                          }
                      }
                  },
                 //условие, при котором будет доступна команда
                 (obj) => (vvodMax <= max && vvodMax>0)));
            }
        }

        private RelayCommand getCheck; //нажали составить чек
        public RelayCommand GetCheck
        {
            get
            {
                return getCheck ??
                  (getCheck = new RelayCommand(obj =>
                  {
                      check.total_cost = sumInCheck;
                      db.UpdateCheck(check);
                      BonusCard bonusCard = new BonusCard(check);
                      bonusCard.ShowDialog(); //открываем окно с бонусной картой
                      add.Close(); //как ток там все сделается, закрываем это окно
                  },
                  //условие, при котором будет доступна команда
                 (obj) => (Line_of_checks.Count>=1)));
            }
        }

        private AddCheck add;
        public AddCheckViewModel(AddCheck add)
        {
            this.add = add;
            db = new DBOperations();
            Products = new ObservableCollection<ProductModel>(db.GetAllProduct());
            Line_of_postavkas = new ObservableCollection<Line_of_postavkaModel>(db.GetAllLine_of_postavka());

            check = new CheckModel(); //создаем чек
            check.date_and_time = DateTime.Now;
            check.number_of_check = db.CreateCheck(check);

            Line_of_checks = new ObservableCollection<Line_of_checkModel>(db.GetAllLine_of_check(check.number_of_check));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
