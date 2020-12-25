namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Check")]
    public partial class Check
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Check()
        {
            Line_of_check = new HashSet<Line_of_check>();
        }

        [Key]
        public int number_of_check { get; set; }

        public DateTime date_and_time { get; set; }

        [Column(TypeName = "money")]
        public decimal? total_cost { get; set; }

        public int? number_of_card_FK { get; set; }

        public decimal? bonus { get; set; }

        public bool card { get; set; }

        public virtual Bonus_card Bonus_card { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line_of_check> Line_of_check { get; set; }
    }
}
