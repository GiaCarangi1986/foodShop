namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Line_of_postavka
    {
        [Key]
        [Column("line_of_postavka")]
        public int line_of_postavka1 { get; set; }

        public int? kolvo_of_product { get; set; }

        public int? ostalos_product { get; set; }

        public DateTime? date_of_preparing { get; set; }

        public bool spisano { get; set; }

        [Column(TypeName = "money")]
        public decimal own_cost { get; set; }

        public int code_of_product_FK { get; set; }

        public int number_of_postavka_FK { get; set; }

        public virtual Postavka Postavka { get; set; }

        public virtual Product Product { get; set; }
    }
}
