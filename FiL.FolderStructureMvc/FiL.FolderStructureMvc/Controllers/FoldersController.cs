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
                var id1 = 1;
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
    }
}
