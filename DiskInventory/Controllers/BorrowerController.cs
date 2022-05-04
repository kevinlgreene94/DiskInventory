﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private disk_inventorykgContext context { get; set; }
        public BorrowerController(disk_inventorykgContext ctx) 
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Borrower> borrowers = context.Borrowers.OrderBy(b => b.Lname).ThenBy(b => b.Fname).ToList();
            return View(borrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Borrower = context.Borrowers.OrderBy(b => b.Fname).ToList();
            return View("Edit", new Borrower());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Borrower = context.Borrowers.OrderBy(b => b.Fname).ToList();
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)
                    //context.Borrowers.Add(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_borrower @p0, @p1, @p2",
                        parameters: new[] { borrower.Fname.ToString(), borrower.Lname.ToString(), borrower.PhoneNum.ToString() });
                else
                    //context.Borrowers.Update(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_borrower @p0, @p1, @p2, @p3",
                        parameters: new[] { borrower.BorrowerId.ToString(), borrower.Fname.ToString(), borrower.Lname.ToString(), borrower.PhoneNum.ToString() });
                context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                ViewBag.Borrower = context.Borrowers.OrderBy(b => b.Fname).ToList();
                return View(borrower);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            ViewBag.Borrower = context.Borrowers.OrderBy(b => b.Fname).ToList();
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            //context.Borrowers.Remove(borrower);
            context.Database.ExecuteSqlRaw("execute sp_delete_borrower @p0",
                parameters: new[] { borrower.BorrowerId.ToString() });
            context.SaveChanges();
            return RedirectToAction("Index", "Borrower");
        }
    }
}
