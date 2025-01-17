﻿using NeighborlyHands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace NeighborlyHands.Controllers
{
    public class HomeController : Controller
    {
        private UsersDBEntities _db = new UsersDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Users.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Login
        //https://www.c-sharpcorner.com/article/simple-login-application-using-Asp-Net-mvc/
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var obj = _db.Users.Where(a => a.Username.Equals(user.Username) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["Username"] = obj.Username.ToString();
                    Session["Password"] = obj.Password.ToString();
                    return RedirectToAction("UserDashboard");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Password"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserDashboard()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude ="Id")] User userToAdd)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _db.Users.Add(userToAdd);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var userToEdit = (from u in _db.Users
                              where u.Id == id
                              select u).First();
            return View(userToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(User userToEdit)
        {
            var originalUser = (from u in _db.Users
                                where u.Id == userToEdit.Id
                                select u).First();

            if (!ModelState.IsValid)
            {
                return View(originalUser);
            }

            _db.Entry(originalUser).CurrentValues.SetValues(userToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Home/Delete/5
        /*
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
