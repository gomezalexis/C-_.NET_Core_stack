using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BankAccounts.Models;
using System.Globalization;

namespace BankAccounts.Controllers
{
    public class AccountController : Controller
    {
        private BankContext _context;
        public AccountController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/bankaccount")]
        public IActionResult ShowAccount()
        {
            CultureInfo us = new CultureInfo("en-US");
            User theUser = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            ViewBag.theUser = theUser;
            double balance = theUser.balance;
            ViewBag.totalBalance = balance.ToString("c", us);
            return View();
        }

    //     [HttpPost]
    //     [Route("/addtransaction")]
    //     public IActionResult AddTransaction(Transaction transaction){

    //     }
    }
}