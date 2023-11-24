using FiL.FolderStructureMvc.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace FiL.FolderStructureMvc.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFolders()
        {
            try
            {
                DirectoryInfo rootDir = new DirectoryInfo(_uploadFolder);
                int n = CountSlash(_uploadFolder, '\\');
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(_uploadFolder.ToString().Substring(_uploadFolder.LastIndexOf("\\") + 1));
                ListFolders(rootDir, n, sb);
                Console.WriteLine(sb.ToString());
                string[] text = sb.ToString().Split('\n', '\t', '\r', '\\');

                List<string> listFolders = text.ToList();
                ViewData["Folders"] = listFolders;                

                return View();

                
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        static void ListFolders(DirectoryInfo folder, int n, StringBuilder sb)
        {
            try
            {
                DirectoryInfo[] array = folder.GetDirectories();
                for (int i = 0; i < array.Length; i++)
                {
                    DirectoryInfo subfolder = array[i];
                    int m = CountSlash(subfolder.ToString(), '\\') - n;
                    sb.Append(string.Concat(Enumerable.Repeat("*", m)));
                    sb.AppendLine(subfolder.Name);
                    ListFolders(subfolder, n, sb);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Access denied to folder: {0}", folder.FullName);
            }
        }

        public static int CountSlash(string subfolder, char symbol)
        {
            IEnumerable<char> stringQuery =
                      from ch in subfolder.ToString()
                      where ch.Equals(symbol)
                      select ch;
            return stringQuery.ToList().Count();
        }

        private readonly string _uploadFolder = @"D:\!Projects\Test_UploadFile"; 

        


    }
    
}
