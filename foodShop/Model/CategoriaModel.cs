using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace foodShop
{
    class CategoriaModel
    {
        public int id_categoria { get; set; }
        public string name { get; set; }

        public CategoriaModel() { }
        public CategoriaModel(Categoria categoria)
        {
            id_categoria = categoria.id_categoria;
            name = categoria.name;
        }
    }
}
