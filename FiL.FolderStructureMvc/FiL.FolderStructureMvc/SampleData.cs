using FiL.FolderStructureMvc.Entities;

namespace FiL.FolderStructureMvc
{
    public class SampleData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Folders.Any())
            {
                return;
            }
            context.Folders.AddRange(
                new Folder
                {
                    Name = "Creating Digital Images", //id = 1
                    ParentId = null
                },
                new Folder
                {
                    Name = "Resources", //id = 2
                    ParentId = 1
                },
                new Folder
                {
                    Name = "Evidence", //id = 3
                    ParentId = 1
                },
                new Folder
                {
                    Name = "Graphic Products", //id = 4
                    ParentId = 1
                },
                new Folder
                {
                    Name = "Primary Sources", //id = 5
                    ParentId = 2
                },
                new Folder
                {
                    Name = "Secondary Sources", //id = 6
                    ParentId = 2
                },
                new Folder
                {
                    Name = "Process", //id = 7
                    ParentId = 4
                },
                new Folder
                {
                    Name = "Final Product", //id = 8
                    ParentId = 4
                }


            );
            context.SaveChanges();
        }
    }
}
