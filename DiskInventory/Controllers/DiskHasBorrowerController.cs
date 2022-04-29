using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class DiskHasBorrowerController : Controller
    {
        private disk_inventorykgContext context { get; set; }
        public DiskHasBorrowerController(disk_inventorykgContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var diskhasborrowers = context.DiskHasBorrowers
                .Include(d => d.Disk).OrderBy(d => d.Disk.DiskName).Include(b => b.Borrower).ToList();
            return View(diskhasborrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            DiskHasBorrowerViewModel checkout = new DiskHasBorrowerViewModel();
            checkout.BorrowDate = DateTime.Now;
            checkout.Disks = context.DiskTables.OrderBy(d => d.DiskName).ToList();
            checkout.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            return View("Edit", checkout);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var diskhasborrower = context.DiskHasBorrowers.Find(id);
            DiskHasBorrowerViewModel checkout = new DiskHasBorrowerViewModel();
            checkout.Disks = context.DiskTables.OrderBy(d => d.DiskName).ToList();
            checkout.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            checkout.BorrowerId = diskhasborrower.BorrowerId;
            checkout.DiskId = diskhasborrower.DiskId;
            checkout.BorrowDate = diskhasborrower.BorrowDate;
            checkout.ReturnDate = diskhasborrower.ReturnDate;
            return View(checkout);
        }
        [HttpPost]
        public IActionResult Edit(DiskHasBorrowerViewModel diskhasborrowerviewmodel)
        {
            DiskHasBorrower checkout = new DiskHasBorrower();
            if(ModelState.IsValid)
            {
                checkout.DiskHasBorrowerId = diskhasborrowerviewmodel.DiskHasBorrowerId;
                checkout.BorrowerId = diskhasborrowerviewmodel.BorrowerId;
                checkout.DiskId = diskhasborrowerviewmodel.DiskId;
                checkout.BorrowDate = diskhasborrowerviewmodel.BorrowDate;
                checkout.ReturnDate = diskhasborrowerviewmodel.ReturnDate;
                if(checkout.DiskHasBorrowerId == 0)
                {
                    context.DiskHasBorrowers.Add(checkout);
                }
                else
                {
                    context.DiskHasBorrowers.Update(checkout);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            ViewBag.Action = (diskhasborrowerviewmodel.DiskHasBorrowerId == 0) ? "Add" : "Edit";
            return View(diskhasborrowerviewmodel);
        }
    }
}
