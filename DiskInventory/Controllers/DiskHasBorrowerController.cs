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
                .Include(d => d.Disk).OrderBy(d => d.Disk.DiskName)
                .Include(b => b.Borrower).ToList();
            return View(diskhasborrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            DiskHasBorrowerViewModel checkout = new DiskHasBorrowerViewModel();
            checkout.CurrentVM.BorrowDate = DateTime.Now;
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
            checkout.CurrentVM.DiskHasBorrowerId = diskhasborrower.DiskHasBorrowerId;
            checkout.CurrentVM.BorrowerId = diskhasborrower.BorrowerId;
            checkout.CurrentVM.DiskId = diskhasborrower.DiskId;
            checkout.CurrentVM.BorrowDate = diskhasborrower.BorrowDate;
            checkout.CurrentVM.ReturnDate = diskhasborrower.ReturnDate;
            return View(checkout);
        }
        [HttpPost]
        public IActionResult Edit(DiskHasBorrowerViewModel diskhasborrowerviewmodel)
        {
            DiskHasBorrower checkout = new DiskHasBorrower();
            if(ModelState.IsValid)
            {
                checkout.DiskHasBorrowerId = diskhasborrowerviewmodel.CurrentVM.DiskHasBorrowerId;
                checkout.BorrowerId = diskhasborrowerviewmodel.CurrentVM.BorrowerId;
                checkout.DiskId = diskhasborrowerviewmodel.CurrentVM.DiskId;
                checkout.BorrowDate = diskhasborrowerviewmodel.CurrentVM.BorrowDate;
                checkout.ReturnDate = diskhasborrowerviewmodel.CurrentVM.ReturnDate;
                if(checkout.DiskHasBorrowerId == 0)
                {
                    //context.DiskHasBorrowers.Add(checkout);
                    if (checkout.ReturnDate != null)
                    {
                        context.Database.ExecuteSqlRaw("execute sp_ins_disk_has_borrower @p0, @p1, @p2, @p3",
                            parameters: new[] {checkout.BorrowerId.ToString(), checkout.DiskId.ToString(),
                        checkout.BorrowDate.ToString(), checkout.ReturnDate.ToString()});
                        TempData["message"] = "Checkout Updated!";
                    }
                    else
                    {
                        context.Database.ExecuteSqlRaw("execute sp_ins_disk_has_borrower @p0, @p1, @p2, @p3",
                            parameters: new[] { checkout.BorrowerId.ToString(), checkout.DiskId.ToString(),
                        checkout.BorrowDate.ToString(), null});
                        TempData["message"] = "Checkout Updated!";
                    }
                }
                else
                {
                    //context.DiskHasBorrowers.Update(checkout);
                    if (checkout.ReturnDate != null)
                    {
                        context.Database.ExecuteSqlRaw("execute sp_upd_disk_has_borrower @p0, @p1, @p2, @p3, @p4",
                            parameters: new[] {checkout.DiskHasBorrowerId.ToString(), checkout.BorrowerId.ToString(), checkout.DiskId.ToString(),
                        checkout.BorrowDate.ToString(), checkout.ReturnDate.ToString()});
                        TempData["message"] = "Checkout Updated!";
                    }
                    else
                    {
                        context.Database.ExecuteSqlRaw("execute sp_upd_disk_has_borrower @p0, @p1, @p2, @p3, @p4",
                            parameters: new[] {checkout.DiskHasBorrowerId.ToString(), checkout.BorrowerId.ToString(), checkout.DiskId.ToString(),
                        checkout.BorrowDate.ToString(), null});
                        TempData["message"] = "Checkout Updated!";
                    }
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }

            ViewBag.Action = (diskhasborrowerviewmodel.CurrentVM.DiskHasBorrowerId == 0) ? "Add" : "Edit";
            diskhasborrowerviewmodel.Disks = context.DiskTables.OrderBy(d => d.DiskName).ToList();
            diskhasborrowerviewmodel.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            return View(diskhasborrowerviewmodel);
        }
    }
}
