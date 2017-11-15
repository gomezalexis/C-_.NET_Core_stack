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

        private bool greenLight(double currentBal , double newTran)
        {
            if(newTran + currentBal >= 0){
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("/bankaccount")]
        public IActionResult ShowAccount()
        {
            CultureInfo us = new CultureInfo("en-US");
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            // Getting the updated version of the user in session
            User theUser = _context.Users.SingleOrDefault(user => user.userId == theSession.userId);
            // all the transactions of user
            List<Transaction> allUserTransactions = _context.Transactions.Where(transaction => transaction.userId == theUser.userId).ToList();
            // for if there is not enough money send a message
            ViewBag.amountError = TempData["amountError"];
            // Display user properties
            ViewBag.theUser = theUser;
            // display all the transacciones
            ViewBag.allTransactions = allUserTransactions;
            // ViewBag.allTransactions = theUser.transactions;
            // Send the balance to the view
            double balance = theUser.balance;
            ViewBag.totalBalance = balance.ToString("c", us);
            return View();
        }

        [HttpPost]
        [Route("/addtransaction")]
        public IActionResult AddTransaction(Transaction transaction){
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            //Getting updated balance
            User theUser = _context.Users.SingleOrDefault(user => user.userId == theSession.userId);
            // putting something in ViewBag.user if there is an error in form
            ViewBag.theUser = theUser;
            if(ModelState.IsValid){
                //Is already valid in sum but we need to check if there is enough
                //money available
                if(greenLight(theUser.balance, transaction.amount))
                {
                    Transaction newTransaction = new Transaction
                    {
                        amount = transaction.amount,
                        createdAt = DateTime.Now,
                        userId = theUser.userId
                    };
                    _context.Add(newTransaction);
                    // bringing up the user to update the balance
                    System.Console.WriteLine("- - - - - - -");
                    System.Console.WriteLine(newTransaction.user.firstName);
                    // User updateUser = _context.Users.SingleOrDefault(user => user.userId == theUser.userId);
                    // updateUser.balance += newTransaction.amount;
                    newTransaction.user.balance += newTransaction.amount;
                    _context.SaveChanges();
                    // theUser.balance += newTransaction.amount;
                    return RedirectToAction("ShowAccount");
                }

                TempData["amountError"] = "You don't have enough Money";
                return RedirectToAction("ShowAccount");
            }
            return View("ShowAccount", transaction);
        }
    }
}