using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

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
            List<DiskTable> disks = context.DiskTables.OrderBy(d => d.DiskName).ThenBy(d => d.DiskType).ToList();
            return View(disks);
        }
    }
}
