using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void DeleteCheck(int Id)
        {
            Check check = db.Checks.Find(Id);
            db.Checks.Remove(check);
            Save();
        }

        public void DeleteLine_of_check(int Id)
        {
            Line_of_check lcheck = db.Line_of_check.Find(Id);
            db.Line_of_check.Remove(lcheck);
            Save();
        }

        public void DeleteLine_of_check_and_postavka(int Id)
        {
            Stroka_check_and_postavka lstrpost = db.Stroka_check_and_postavka.Find(Id);
            db.Stroka_check_and_postavka.Remove(lstrpost);
            Save();
        }

        public Line_of_postavkaModel GetCardPostavka()
        {
            List<Line_of_postavkaModel> line_s = new List<Line_of_postavkaModel>();
            line_s = GetAllLine_of_postavka();
            foreach (var temp in line_s.Where(i=>i.code_of_product_FK==26 && i.ostalos_product>0))
            {
                return temp;
            }
            return null;
        }

            public double CardCost()
        {
            int Id = 26; //id бонусной карты
            ProductModel product= new ProductModel(db.Products.Find(Id));
            return (double)product.now_cost;
        }

        public int CreateBobusCard(Bonus_cardModel bonusCard)
        {
            Bonus_card bonus = new Bonus_card();
            bonus.kolvo_bonusov = 0;
            db.Bonus_card.Add(bonus);
            Save();
            return bonus.number_of_card;
        }

        public void SpisatProsrochka(int Id)
        {
            Line_of_postavka line = db.Line_of_postavka.Find(Id);
            line.spisano = true;
            Save();
        }

        public CheckModel GetCheck(int Id)
        {
            db.Checks.Load();
            var t = db.Checks.ToList();
            var test =new CheckModel(db.Checks.Where(i=>i.number_of_check==Id).FirstOrDefault());
            return test; 
        }

        public List<Stroka_check_and_postavkaModel> GetAllStrokaCheckAndPostavka()
        {
            return db.Stroka_check_and_postavka.ToList().Select(i => new Stroka_check_and_postavkaModel(i)).ToList();
        }

        public Stroka_check_and_postavkaModel GetStrokaCheckAndPostavka(int Id)
        {
            return new Stroka_check_and_postavkaModel(db.Stroka_check_and_postavka.Find(Id));
        }

        public void UpdateStrokaCheckAndPostavka(Stroka_check_and_postavkaModel model)
        {
            Stroka_check_and_postavka stroka = db.Stroka_check_and_postavka.Find(model.id);
            stroka.kolvo_product_in_stroka_postavka = model.kolvo_product_in_stroka_postavka;
            Save();
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
            bonus_Card.kolvo_bonusov += check.total_cost * (decimal?)0.01 - (decimal?)spisat;
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

        public Bonus_cardModel GetBonus_card(int Id)
        {
            return new Bonus_cardModel(db.Bonus_card.Find(Id));
        }

        public CheckModel GetLastCheck()
        {
            CheckModel checkModel = db.Checks.ToList().Select(i => new CheckModel(i)).ToList().LastOrDefault();
            return new CheckModel(db.Checks.Find(checkModel.number_of_check));
        }

        public CheckModel GetPredLastCheck()
        {
            int index = db.Checks.ToList().Select(i => new CheckModel(i)).ToList().Count-2;
            CheckModel checkModel = db.Checks.ToList().Select(i => new CheckModel(i)).ToList().ElementAt(index);
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
            if (!checkModel.card)
                check.card = false;
            else check.card = checkModel.card;
            db.Checks.Add(check);
            Save();
            return check.number_of_check;
        }

        public void UpdateCheck(CheckModel checkModel)
        {
            Check check = db.Checks.Find(checkModel.number_of_check);
            check.date_and_time = (DateTime)checkModel.date_and_time;
            check.number_of_card_FK = checkModel.number_of_card_FK;
            check.total_cost = checkModel.total_cost;
            check.card = checkModel.card;
            if (checkModel.bonus!=null)
            check.bonus = checkModel.bonus;
            Save();
        }

        public void CreateStroka_check_and_postavka(Stroka_check_and_postavkaModel stroka_check_and_postavkaModel)
        {
            Stroka_check_and_postavka stroka_check_and_postavka = new Stroka_check_and_postavka();
            stroka_check_and_postavka.id_stroka_check = stroka_check_and_postavkaModel.id_stroka_check;
            stroka_check_and_postavka.id_stroka_postavka = stroka_check_and_postavkaModel.id_stroka_postavka;
            stroka_check_and_postavka.kolvo_product_in_stroka_postavka = stroka_check_and_postavkaModel.kolvo_product_in_stroka_postavka;
            db.Stroka_check_and_postavka.Add(stroka_check_and_postavka);
            Save();
        }

        /*public bool Save()
            {
                if (db.SaveChanges() > 0) return true;
                return false;
            }*/

        public Exception Save()
        {
            try
            {
                bool ok = false;
                if (db.SaveChanges() > 0)
                    ok = true;
                return null;
            }
            catch (Exception Err)
            {
                return Err;
            }
        }

    }
}
