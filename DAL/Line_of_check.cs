namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Line_of_check
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Line_of_check()
        {
            Stroka_check_and_postavka = new HashSet<Stroka_check_and_postavka>();
        }

        [Key]
        public int line_number_of_check { get; set; }

        public int much_of_products { get; set; }

        [Column(TypeName = "money")]
        public decimal cost_for_buyer { get; set; }

        public int number_of_check_FK { get; set; }

        public int code_of_product_FK { get; set; }

        public virtual Check Check { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stroka_check_and_postavka> Stroka_check_and_postavka { get; set; }
    }
}
