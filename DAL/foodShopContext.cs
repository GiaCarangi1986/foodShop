namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class foodShopContext : DbContext
    {
        public foodShopContext()
            : base("name=foodShopContext")
        {
        }

        public virtual DbSet<Bonus_card> Bonus_card { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Check> Checks { get; set; }
        public virtual DbSet<Line_of_check> Line_of_check { get; set; }
        public virtual DbSet<Line_of_postavka> Line_of_postavka { get; set; }
        public virtual DbSet<Postavka> Postavkas { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bonus_card>()
                .HasMany(e => e.Checks)
                .WithRequired(e => e.Bonus_card)
                .HasForeignKey(e => e.number_of_card_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.id_categoria_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Check>()
                .Property(e => e.total_cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Check>()
                .HasMany(e => e.Line_of_check)
                .WithOptional(e => e.Check)
                .HasForeignKey(e => e.number_of_check_FK);

            modelBuilder.Entity<Line_of_check>()
                .Property(e => e.cost_for_buyer)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Line_of_postavka>()
                .Property(e => e.own_cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Postavka>()
                .Property(e => e.itogo_cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Postavka>()
                .HasMany(e => e.Line_of_postavka)
                .WithRequired(e => e.Postavka)
                .HasForeignKey(e => e.number_of_postavka_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.now_cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Line_of_check)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.code_of_product_FK);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Line_of_postavka)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.code_of_product_FK)
                .WillCascadeOnDelete(false);
        }
    }
}
