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
    class BonusCardViewModel : INotifyPropertyChanged
    {
        private DBOperations db;
        private Bonus_cardModel selectedBonusCard; //хранит выбранную в combox бонусную карту
        private int? spisat;
        private decimal? max;
        private bool newCard; //приобрел ли покупатель новую карту?
        private double cost;//цена приоьретения бонусной карты
        Line_of_postavkaModel postavkaModel; //строка поставки с бонусной картой

        public ObservableCollection<Bonus_cardModel> BonusCards { get; set; } //коллекция бонусных карт

        public int? VvodBonus //ввод желаемого кол-ва бонусов для списания
        {
            get { return spisat; }
            set
            {
                if (selectedBonusCard != null)
                {
                    spisat = value;
                    OnPropertyChanged("VvodMax");
                }
            }
        }

        public decimal? MaxBonus //отправляем в текстблок максимально возможное кол-во бонусов для списания
        {
            get { return max; }
            set
            {
                max = value;
                OnPropertyChanged("MaxBonus");
            }
        }

        public Bonus_cardModel SelectedBonusCard //запомнить бонусную карту, выбранную в combobox
        {
            get { return selectedBonusCard; }
            set
            {
                selectedBonusCard = value;

                max = selectedBonusCard.kolvo_bonusov;
                if (max != null)
                {
                    MaxBonus = max;
                    //spisat = 0;
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        private RelayCommand withCard; //ПРИМЕНИТЬ бонусную карту (зачислить новые бонусы и/или списать старые бонусы)
        public RelayCommand WithCard
        {
            get
            {
                return withCard ??
                  (withCard = new RelayCommand(obj =>
                  {
                      //логика (на карту идет 1% от всех покупок, потом можно будет снимать 1:1)
                      selectedBonusCard.kolvo_bonusov = db.UpdateBonus_card(selectedBonusCard, check, spisat.Value);
                      selectedBonusCard.snayli_bonusov = spisat.Value;
                      check.number_of_card_FK = selectedBonusCard.number_of_card;

                      if (spisat != 0)
                      {
                          check.total_cost -= spisat;
                          check.bonus = spisat.Value;
                      }

                      db.UpdateCheck(check); //обновили чек в бд  (итоговая стоимость ниже, если сняла бонусы)
                                             //плюс запишем в чек инфу о бонусной карте, если ее применили


                      ThankYou thank = new ThankYou(bonusCard, check, selectedBonusCard, db, cost);
                      thank.Show(); //октрыть окно с подведением итогов о покупке
                  },
                 //условие, при котором будет доступна команда
                 (obj) => (selectedBonusCard != null && spisat <= selectedBonusCard.kolvo_bonusov && spisat<=check.total_cost)));
            }
        }

        private RelayCommand withoutCard; //НЕ ПРИМЕНЯТЬ бонусную карту
        public RelayCommand WithoutCard
        {
            get
            {
                return withoutCard ??
                  (withoutCard = new RelayCommand(obj =>
                  {
                      if (selectedBonusCard != null)
                          selectedBonusCard = null;
                      ThankYou thank = new ThankYou(bonusCard, check, selectedBonusCard, db, cost);
                      thank.Show(); //октрыть окно с подведением итогов о покупке
                      bonusCard.Close(); //закрываем окно BonusCard
                  }));
            }
        }

        private RelayCommand addCard; //ПРИОБРЕСТИ бонусную карту
        public RelayCommand AddCard
        {
            get
            {
                return addCard ??
                  (addCard = new RelayCommand(obj =>
                  {
                      Bonus_cardModel model = new Bonus_cardModel();
                      model.number_of_card = db.CreateBobusCard(model);
                      BonusCards.Add(model);
                      model.kolvo_bonusov = 0;
                      newCard = true;
                      cost = db.CardCost();

                      CheckModel check1 = new CheckModel();
                      check1.date_and_time = DateTime.Now;
                      check1.card = true;
                      check1.total_cost = (decimal?)cost;
                      check1.number_of_check = db.CreateCheck(check1);

                      Line_of_checkModel line = new Line_of_checkModel();
                      line.code_of_product_FK = 26;
                      line.cost_for_buyer = (decimal)cost;
                      line.itogo = (decimal)cost;
                      line.much_of_products = 1;
                      line.name_of_product = "бонусная карта";
                      line.number_of_check_FK = check1.number_of_check;
                      db.CreateLine_of_check(line);

                      postavkaModel.ostalos_product--;
                      db.UpdateLine_of_postavka(postavkaModel);
                  },
                 //условие, при котором будет доступна команда
                 (obj) => (newCard == false && (postavkaModel = db.GetCardPostavka()) != null)));
            }
        }

        private CheckModel check;
        private BonusCard bonusCard;
        public BonusCardViewModel(BonusCard bonusCard, CheckModel check, DBOperations db)
        {
            this.bonusCard = bonusCard; //используем для последующего закрытия текущего окна
            this.check = check;
            this.db = db;
            BonusCards = new ObservableCollection<Bonus_cardModel>(db.GetAllBonus_card());
            Line_of_postavkaModel postavkaModel = new Line_of_postavkaModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
