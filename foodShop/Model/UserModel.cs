using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    public class UserModel
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public UserModel() { }
        public UserModel(User user)
        {
            id = user.id;
            login = user.login;
            password = user.password;
        }
    }
}
