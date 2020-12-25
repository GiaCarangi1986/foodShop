namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Postavka")]
    public partial class Postavka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Postavka()
        {
            Line_of_postavka = new HashSet<Line_of_postavka>();
        }

        [Key]
        public int number_of_postavka { get; set; }

        public DateTime date_of_postavka { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line_of_postavka> Line_of_postavka { get; set; }
    }
}
