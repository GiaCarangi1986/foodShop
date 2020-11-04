namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Line_of_check = new HashSet<Line_of_check>();
            Line_of_postavka = new HashSet<Line_of_postavka>();
        }

        [Key]
        public int code_of_product { get; set; }

        [Column(TypeName = "money")]
        public decimal now_cost { get; set; }

        public int? scor_godnosti_O { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        public int id_categoria_FK { get; set; }

        public virtual Categoria Categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line_of_check> Line_of_check { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line_of_postavka> Line_of_postavka { get; set; }
    }
}
