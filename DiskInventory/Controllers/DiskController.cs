using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;
namespace DiskInventory.Controllers
{
    public class DiskController : Controller
    {
        private disk_inventorykgContext context { get; set; }
        public DiskController(disk_inventorykgContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<DiskTable> disks = context.DiskTables.OrderBy(d => d.DiskName).Include(g => g.Genre).Include(s => s.Status).Include(d => d.DiskType).ToList();
            return View(disks);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.DiskGenres.OrderBy(g => g.Descrip).ToList();
            ViewBag.Statuses = context.DiskStatuses.OrderBy(s => s.Descrip).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Descrip).ToList();
            return View("Edit", new DiskTable());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.DiskGenres.OrderBy(g => g.Descrip).ToList();
            ViewBag.Statuses = context.DiskStatuses.OrderBy(s => s.Descrip).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Descrip).ToList();
            var disk = context.DiskTables.Find(id);
            return View(disk);
        }

        [HttpPost]
        public IActionResult Edit(DiskTable disk)
        {
            if (ModelState.IsValid)
            {
                if (disk.DiskId == 0)
                    context.DiskTables.Add(disk);
                else
                    context.DiskTables.Update(disk);
                context.SaveChanges();
                return RedirectToAction("Index", "Disk");
            }
            else
            {
                ViewBag.Action = (disk.DiskId == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.DiskGenres.OrderBy(g => g.Descrip).ToList();
                ViewBag.Statuses = context.DiskStatuses.OrderBy(s => s.Descrip).ToList();
                ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Descrip).ToList();
                return View(disk);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            ViewBag.Genres = context.DiskGenres.OrderBy(g => g.Descrip).ToList();
            ViewBag.Statuses = context.DiskStatuses.OrderBy(s => s.Descrip).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Descrip).ToList();
            var disk = context.DiskTables.Find(id);
            return View(disk);
        }
        [HttpPost]
        public IActionResult Delete(DiskTable disk)
        {
            context.DiskTables.Remove(disk);
            context.SaveChanges();
            return RedirectToAction("Index", "Disk");
        }
    }
}
