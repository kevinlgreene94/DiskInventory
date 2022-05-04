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
                    //context.DiskTables.Add(disk);
                    context.Database.ExecuteSqlRaw("execute sp_ins_disk @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] {disk.DiskName.ToString(), disk.ReleaseDate.ToString(), disk.GenreId.ToString(), disk.StatusId.ToString(),
                            disk.DiskTypeId.ToString()});
                else
                    //context.DiskTables.Update(disk);
                    context.Database.ExecuteSqlRaw("execute sp_upd_disk @p0, @p1, @p2, @p3, @p4, @p5",
                        parameters: new[] {disk.DiskId.ToString(),disk.DiskName.ToString(), disk.ReleaseDate.ToString(), disk.GenreId.ToString(), disk.StatusId.ToString(),
                            disk.DiskTypeId.ToString()});
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
            //context.DiskTables.Remove(disk);
            context.Database.ExecuteSqlRaw("execute sp_delete_disk @p0",
                parameters: disk.DiskId);
            context.SaveChanges();
            return RedirectToAction("Index", "Disk");
        }
    }
}
