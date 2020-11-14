using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class DBOperations
    {
        private foodShopContext db;

            public DBOperations()
            {
                db = new foodShopContext();
            }

            public List<CheckModel> GetAllCheck()
            {
            return db.Checks.ToList().Select(i => new CheckModel(i)).ToList();
        }

            public List<Line_of_checkModel> GetAllLine_of_check()
            {
                return db.Line_of_check.ToList().Select(i => new Line_of_checkModel(i)).ToList();
            }

        public List<Line_of_postavkaModel> GetAllLine_of_postavka()
        {
            return db.Line_of_postavka.ToList().Select(i => new Line_of_postavkaModel(i)).ToList();
        }

        public List<Line_of_checkModel> GetAllLine_of_check(int id)
        {
            return db.Line_of_check.ToList().Select(a => new Line_of_checkModel(a)).Where(i=> i.number_of_check_FK==id).ToList();
        }

        public List<Bonus_cardModel> GetAllBonus_card()
        {
            return db.Bonus_card.ToList().Select(i => new Bonus_cardModel(i)).ToList().OrderBy(i => i).ToList();
        }

        public decimal? UpdateBonus_card(Bonus_cardModel bonus, CheckModel check, int spisat)
        {
            Bonus_card bonus_Card = db.Bonus_card.Find(bonus.number_of_card);
            //можно списать только старые бонусы, а новые будут зачислены на след покупку
            bonus_Card.kolvo_bonusov = bonus_Card.kolvo_bonusov + check.total_cost * (decimal?)0.01 - (decimal?)spisat;
            Save();
            return bonus_Card.kolvo_bonusov;
        }

        public List<ProductModel> GetAllProduct()
            {
                return db.Products.ToList().Select(i => new ProductModel(i)).ToList().OrderBy(i => i).ToList();
            }

            public ProductModel GetProduct(int Id)
            {
                return new ProductModel(db.Products.Find(Id));
            }

        public CheckModel GetLastCheck()
        {
            CheckModel checkModel = db.Checks.ToList().Select(i => new CheckModel(i)).ToList().LastOrDefault();
            return new CheckModel(db.Checks.Find(checkModel.number_of_check));
        }

        public Line_of_checkModel GetLine_of_check(int Id)
        {
            return new Line_of_checkModel(db.Line_of_check.Find(Id));
        }

        public Line_of_postavkaModel GetLine_of_postavka(int Id)
        {
            return new Line_of_postavkaModel(db.Line_of_postavka.Find(Id));
        }

        public void UpdateLine_of_postavka(Line_of_postavkaModel line_of_postavkaModel)
        {
            Line_of_postavka pline = db.Line_of_postavka.Find(line_of_postavkaModel.line_of_postavka);
            pline.ostalos_product = line_of_postavkaModel.ostalos_product;
            pline.spisano = line_of_postavkaModel.spisano;
            Save();
        }

        public int CreateLine_of_check(Line_of_checkModel line_Of_Check)
            {
            Line_of_check line = new Line_of_check();
            line.much_of_products = line_Of_Check.much_of_products;
            line.cost_for_buyer = line_Of_Check.cost_for_buyer;
            line.number_of_check_FK = line_Of_Check.number_of_check_FK;
            line.code_of_product_FK = line_Of_Check.code_of_product_FK;
            db.Line_of_check.Add(line);
            /*db.Line_of_check.Add(new Line_of_check()
            {
                much_of_products = line_Of_Check.much_of_products,
                cost_for_buyer = line_Of_Check.cost_for_buyer,
                number_of_check_FK = line_Of_Check.number_of_check_FK,
                code_of_product_FK = line_Of_Check.code_of_product_FK
            });*/
            Save();
            return line.line_number_of_check;
            }

            public void UpdateLine_of_check(Line_of_checkModel line_Of_Check)
            {
                Line_of_check line = db.Line_of_check.Find(line_Of_Check.line_number_of_check);
                line.much_of_products = line_Of_Check.much_of_products;
                line.cost_for_buyer = line_Of_Check.cost_for_buyer;
                line.number_of_check_FK = line_Of_Check.number_of_check_FK;
                line.code_of_product_FK = line_Of_Check.code_of_product_FK;
                Save();
            }

        public int CreateCheck(CheckModel checkModel)
        {
            Check check = new Check();
            check.date_and_time = (DateTime)checkModel.date_and_time;
            check.number_of_card_FK = checkModel.number_of_card_FK;
            check.total_cost = checkModel.total_cost;
            db.Checks.Add(check);
            /*db.Checks.Add(new Check()
            {
              date_and_time = (DateTime)checkModel.date_and_time,
            number_of_card_FK = checkModel.number_of_card_FK,
            total_cost = checkModel.total_cost
        });*/
            Save();
            return check.number_of_check;
        }

        public void UpdateCheck(CheckModel checkModel)
        {
            Check check = db.Checks.Find(checkModel.number_of_check);
            check.date_and_time = (DateTime)checkModel.date_and_time;
            check.number_of_card_FK = checkModel.number_of_card_FK;
            check.total_cost = checkModel.total_cost;
            Save();
        }

        public bool Save()
            {
                if (db.SaveChanges() > 0) return true;
                return false;
            }
        }

}
