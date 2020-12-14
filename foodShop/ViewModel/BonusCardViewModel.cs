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
                MaxBonus = max;
                spisat = 0;
                OnPropertyChanged("SelectedProduct");
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

                      ThankYou thank = new ThankYou(bonusCard, check, selectedBonusCard, db);
                      thank.Show(); //октрыть окно с подведением итогов о покупке
                     // bonusCard.Close(); //закрываем окно BonusCard
                  },
                 //условие, при котором будет доступна команда
                 (obj) => (selectedBonusCard!=null && spisat<= selectedBonusCard.kolvo_bonusov)));
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
                      ThankYou thank = new ThankYou(bonusCard, check, selectedBonusCard, db);
                      thank.Show(); //октрыть окно с подведением итогов о покупке
                      db.close = true;
                      bonusCard.Close(); //закрываем окно BonusCard
                  }));
            }
        }

        private CheckModel check;
        private BonusCard bonusCard;
        public BonusCardViewModel(BonusCard bonusCard, CheckModel check, DBOperations db)
        {
            this.bonusCard = bonusCard; //используем для последующего закрытия текущего окна
            this.check = check;
            db.close = false;
            //db = new DBOperations();
            this.db = db;
            BonusCards = new ObservableCollection<Bonus_cardModel>(db.GetAllBonus_card());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
