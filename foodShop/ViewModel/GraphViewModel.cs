using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;

namespace foodShop
{
    public class GraphViewModel : INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Func<string, string> yFornatter { get; set; }
        public string[] Labels { get; set; }
        public ObservableCollection<CheckModel> Graph { get; set; } //дата, сумма и бонусы
        private decimal? sum, bon;

        public decimal? Sum //подводим итоговую сумму чеков
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }

        public decimal? Bon //подводим кол-во бонусов
        {
            get { return bon; }
            set
            {
                bon = value;
                OnPropertyChanged("Bon");
            }
        }

        /*private RelayCommand goodbay; //закрыть меню
        public RelayCommand Goodbay
        {
            get
            {
                return goodbay ??
                  (goodbay = new RelayCommand(obj =>
                  {
                      graph.Close(); //закрыли текущее окно Graph
                  }));
            }
        }*/

        private Graph graph;
        public GraphViewModel(Graph graph, ObservableCollection<CheckModel> Graph)
        {
            SeriesCollection = new SeriesCollection();
            this.graph = graph;
            this.Graph = Graph;
            string[] dates = new string[Graph.Count];
            int i = 0;
            sum = bon = 0;
            foreach (var temp in Graph)
            {
                dates[i] = temp.date_and_time.ToShortDateString();
                sum += temp.total_cost;
                bon += temp.bonus;
                i++;
            }
            Labels = dates;
            yFornatter = value => value.ToString();
            RefreshCharts();
        }

        void RefreshCharts()
        {
            SeriesCollection.Clear();
            //foreach (var service in Graph)
            //{
            ChartValues<decimal> sum = new ChartValues<decimal>();
            ChartValues<decimal> bonus = new ChartValues<decimal>();
            foreach (var temp in Graph)
            {
                sum.Add((decimal)temp.total_cost);
            }
            foreach (var temp in Graph)
            {
                bonus.Add((decimal)temp.bonus);
            }
            SeriesCollection.Add(new LineSeries
            {
                Title = "Сумма",
                    Values = new ChartValues<decimal>(sum)
                });
            SeriesCollection.Add(new LineSeries
            {
                Title = "Бонусы",
                Values = new ChartValues<decimal>(bonus)
            });
            /*SeriesCollection.Add(new Axis
            {

            });*/

            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
