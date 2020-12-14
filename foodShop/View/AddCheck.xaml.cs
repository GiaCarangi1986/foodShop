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
    /// Логика взаимодействия для AddCheck.xaml
    /// </summary>
    public partial class AddCheck : Window
    {
        DBOperations DB;
        public AddCheck(DBOperations db)
        //public AddCheck()
        {
            InitializeComponent();
            DB = db;
            DataContext = new AddCheckViewModel(this, db);
            //DataContext = new AddCheckViewModel();
            //this.Closing += new System.ComponentModel.CancelEventHandler(MyWindow_Closing);
        }

        private void NO_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!DB.close)
                e.Cancel = true;
            else e.Cancel = false;
        }

        /*private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }*/
    }
}
