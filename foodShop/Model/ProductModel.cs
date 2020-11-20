using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class ProductModel : IComparable
    {
        public int code_of_product { get; set; }
        public decimal now_cost { get; set; }
        public string title { get; set; }
        public int? scor_godnosti_O { get; set; }
        public int id_categoria_FK { get; set; }

        public int? all_kolvo { get; set; } //нужно из строки поставки брать и вычитать оттуда ostalos

        public ProductModel() { }
        public ProductModel(Product product)
        {
            code_of_product = product.code_of_product;
            now_cost = product.now_cost;
            title = product.title;
            scor_godnosti_O = product.scor_godnosti_O;
            id_categoria_FK = product.id_categoria_FK;

            all_kolvo = 0;
            //правильно считаю кол-во всех продуктов?
            foreach (var lpost in product.Line_of_postavka)
                if (lpost.spisano!=true)
                all_kolvo += lpost.ostalos_product;
        }

        public int CompareTo(object o)
        {
            ProductModel p = o as ProductModel;
            if (p != null)
                return this.title.CompareTo(p.title);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}
