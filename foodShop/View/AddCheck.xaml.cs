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
        public AddCheck(DBOperations db)
        {
            InitializeComponent();
            DataContext = new AddCheckViewModel(this, db);
        }
    }
}
