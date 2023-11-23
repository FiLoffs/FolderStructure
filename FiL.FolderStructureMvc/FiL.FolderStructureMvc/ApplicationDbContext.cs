using FiL.FolderStructureMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiL.FolderStructureMvc
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name);
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.SubFolders)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

    }
}
