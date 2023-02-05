using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aula3108.Models
{
    public class ProdutosDbContext : DbContext
    {
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("Ordering");

            modelBuilder.Entity<Produtos>()
                .ToTable("Produto");

            modelBuilder.Entity<Produtos>().HasKey(a => a.IdProduto);

            modelBuilder.Entity<Produtos>()
                .Property(a => a.IdProduto).HasColumnName("idProduto")
                .HasColumnType("int");

            modelBuilder.Entity<Produtos>()
                .Property(a => a.NomeProduto).HasColumnName("nomeProduto")
                .HasColumnType("varchar");

            modelBuilder.Entity<Produtos>()
                .Property(a => a.QuantEstoq).HasColumnName("quantEstoq")
                .HasColumnType("int");

            modelBuilder.Entity<Produtos>()
                .Property(a => a.VlrProduto).HasColumnName("vlrProduto")
                .HasColumnType("float");

            modelBuilder.Entity<Produtos>()
               .Property(a => a.Unidade).HasColumnName("unidade")
               .HasColumnType("int");

            modelBuilder.Entity<Produtos>()
                .Property(a => a.Peso).HasColumnName("peso")
                .HasColumnType("float");

            //modelBuilder.Entity<Produtos>()
            //    .Property(a => a.IdProduto).HasColumnName("idProduto")
            //    .HasColumnType("int");
        }
    }
}