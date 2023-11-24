using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiL.FolderStructureMvc;
using FiL.FolderStructureMvc.Entities;

namespace FiL.FolderStructureMvc.Controllers
{
    public class FoldersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoldersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (name == null)
            {
                var id1 = await _context.Folders.Where(f => f.ParentId == null).Select(f => f.Id).FirstOrDefaultAsync();
                name = await _context.Folders.Where(f => f.Id == id1).Select(f => f.Name).FirstOrDefaultAsync();

                SetName(name);
                
                return View(await _context.Folders.Where(c => c.ParentId == id1).ToListAsync());
            }

            SetName(name);

            var id = await _context.Folders.Where(f => f.Name.Equals(name)).Select(f => f.Id).FirstOrDefaultAsync();
            return View(await _context.Folders.Where(c => c.ParentId == id).ToListAsync());
        }

        private void SetName(string name)
        {
            ViewBag.Name = name;
            ViewData["Title"] = name;
        }

        public async Task<IActionResult> InitializeDefaultData()
        {
            if (_context.Folders.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Folders.AddRange(
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
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteData()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Folders]");
            return RedirectToAction(nameof(Index));
        }
    }
}
