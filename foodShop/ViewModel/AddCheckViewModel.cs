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
        private foodShopContext dbContext;
        private Line_of_check selectedLine_of_check;
        private ProductModel selectedProductModel;

        public ObservableCollection<ProductModel> Products { get; set; }
        public ObservableCollection<Line_of_check> Line_of_checks { get; set; }
        
        public Line_of_check SelectedLine_of_check
        {
            get { return selectedLine_of_check; }
            set
            {
                if (selectedLine_of_check.Check.number_of_check == 0)
                {
                    selectedLine_of_check = value;
                    OnPropertyChanged("SelectedLine_of_check");
                }
            }
        }

        public int? SelectedProductModel
        {
            get { return selectedProductModel.all_kolvo; }
        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                //здесь должно быть добавление нового элемента в дал (работа тут с моделями или как)?
                //если да, то как фиксировать добавления именно в дал и изменение строки чека
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Line_of_check lcheck = new Line_of_check();
                      //lcheck.Check.number_of_check = 0;
                      Line_of_checks.Insert(Line_of_checks.Count, lcheck);
                      selectedLine_of_check = lcheck;
                  }));
            }
        }

        private RelayCommand getCheck;
        public RelayCommand GetCheck
        {
            get
            {
                return getCheck ??
                  (getCheck = new RelayCommand(obj =>
                  {
                      BonusCard bonusCard = new BonusCard();
                      bonusCard.ShowDialog();
                      add.Close();
                  }));
            }
        }

        /*private RelayCommand bay;
        public RelayCommand Bay
        {
            get
            {
                return bay ??
                  (bay = new RelayCommand(obj =>
                  {
                      add.Close();
                  }));
            }
        }*/

        private AddCheck add;
        public AddCheckViewModel(AddCheck add)
        {
            this.add = add;
            dbContext = new foodShopContext();
            Line_of_checks = new ObservableCollection<Line_of_check>(dbContext.Line_of_check.ToList());
            Products = new ObservableCollection<ProductModel>(dbContext.Products.ToList().Select(i => new ProductModel(i)).ToList());
            //  selectedProductModel = dbContext.Products.Select(i => new ProductModel(i)).ToList();
            //надо коллекцию ProductModel?
            //нужен класс dbOperations где пропишем все методы?
            //искать в коллекции?
            //как тут ввести инфу о выбранном в комбобоксе продукте??
           // selectedProductModel = new ProductModel(dbContext.Products.Find(Products[0].code_of_product));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
