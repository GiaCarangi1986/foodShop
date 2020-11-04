using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    class Line_of_checkModel
    {
        public int line_number_of_check { get; set; }
        public int much_of_products { get; set; }
        public decimal cost_for_buyer { get; set; }
        public int? number_of_check_FK { get; set; }
        public int? code_of_product_FK { get; set; }

        public string name_of_product { get; set; }
        public decimal itogo { get; set; }

        public Line_of_checkModel() { }
        public Line_of_checkModel(Line_of_check lcheck)
        {
            line_number_of_check = lcheck.line_number_of_check;
            much_of_products = lcheck.much_of_products;
            cost_for_buyer = lcheck.cost_for_buyer;
            number_of_check_FK = lcheck.number_of_check_FK;
            code_of_product_FK = lcheck.code_of_product_FK;

            name_of_product = lcheck.Product.title;
            itogo = lcheck.Product.now_cost * much_of_products;
        }
    }
}
