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


    }
}
