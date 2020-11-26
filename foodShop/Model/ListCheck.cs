using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foodShop
{
    public class ListCheck
    {
        public DateTime dateTime { get; set; }
        public ObservableCollection<CheckModel> checkModels { get; set; }

        public ListCheck(DateTime date)
        {
            dateTime = DateTime.Parse(date.ToShortDateString());
            checkModels = new ObservableCollection<CheckModel>();
        }

        public void Add(CheckModel check)
        {
            checkModels.Add(check);
        }

        public decimal Sum()
        {
            decimal sum = 0;
            foreach (var temp in checkModels)
                sum += (decimal)temp.total_cost;
            return sum;
        }

        public decimal Bonus()
        {
            decimal bonus = 0;
            foreach (var temp in checkModels)
                if (temp.bonus!=null)
                bonus += (decimal)temp.bonus;
            return bonus;
        }
    }
}
