using Microsoft.EntityFrameworkCore;
using UrbanTech.Models;
namespace UrbanTech.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<ChamadoModel> Chamados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChamadoModel>(entity =>
            {
                entity.ToTable("Chamados");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.DescricaoChamado).IsRequired();
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
    }
}
