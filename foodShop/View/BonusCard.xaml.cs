using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace foodShop
{
    /// <summary>
    /// Логика взаимодействия для BonusCard.xaml
    /// </summary>
    public partial class BonusCard : Window
    {
        DBOperations DB;
        public BonusCard(CheckModel check, DBOperations db)
        {
            InitializeComponent();
            DataContext = new BonusCardViewModel(this, check, db);
            DB = db;
            //this.Closing += new System.ComponentModel.CancelEventHandler(MyWindow_Closing);
        }

        private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!DB.close)
                e.Cancel = true;
            else e.Cancel = false;
        }
    }
}
