using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DAL;

namespace foodShop
{
    class AddCheckViewModel : INotifyPropertyChanged
    {
        private Line_of_checkModel lcheck; //хранит строку чека
        private DBOperations db;
        private ProductModel selectedProduct; //хранит выбранный в combox продукт
        private CheckModel check; //создает новый чек, куда впишем строки чека
        private int max; //хранит максимально доступное кол-во товара
        private int vvodMax; //пользовательский ввод кол-ва продуктов
        private int sumInCheck; //итоговая сумма чека
        private decimal price;

        public ObservableCollection<ProductModel> Products { get; set; } //коллекция продуктов
        public ObservableCollection<Line_of_checkModel> Line_of_checks { get; set; } //коллекция строк чека

        public decimal Price //указывается цена товара
        {
            get { return price; }
            set
            {
                    price = value;
                    OnPropertyChanged("Price");
            }
        }

        public int VvodMax //ввод желаемого кол-ва товаров
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
                Price = price;
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
                      lcheck.much_of_products = vvodMax;
                      lcheck.name_of_product = selectedProduct.title;
                      lcheck.itogo = selectedProduct.now_cost * vvodMax;

                      sumInCheck += (int)lcheck.itogo;

                      //тут должен быть код выбора рандома продуктов из партий

                      db.CreateLine_of_check(lcheck); //в бд сохраним новую строку чека

                      Line_of_checks.Insert(Line_of_checks.Count, lcheck); //добавить строку чека в поле слева
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
