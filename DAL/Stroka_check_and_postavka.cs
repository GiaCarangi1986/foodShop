namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stroka_check_and_postavka
    {
        public int id { get; set; }

        public int id_stroka_check { get; set; }

        public int id_stroka_postavka { get; set; }

        public int kolvo_product_in_stroka_postavka { get; set; }

        public virtual Line_of_check Line_of_check { get; set; }

        public virtual Line_of_postavka Line_of_postavka { get; set; }
    }
}
