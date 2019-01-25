using System;
using System.Linq;
using System.Web.Mvc;
using Blog.Models;

namespace recipes_book.Controllers
{
    public class AccountController : Controller
    {
        private DB db = new DB();

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "Name, Surname, Email, Password, ConfirmPassword")]User user)
        {
            try
            {
                string error = ValidateRegistration(user);

                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError("", error);
                }

                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    Auth(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception err)
            {
                ModelState.AddModelError("", "Невозможно зарегистрировать нового пользователя");
            }
            return View(user);
        }

        public ActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth([Bind(Include = "Email, Password")]User user)
        {
            try
            {
                if (!AuthFieldsNotEmply(user))
                {
                    ModelState.AddModelError("", "Заполните все обязательные поля");
                }

                if (ModelState.IsValid)
                {
                    User usr = db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                    if (usr != null)
                    {
                        Session.Add("UserId", usr.ID.ToString());
                        Session.Add("UserName", usr.Name.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с указанным Email или паролем не существует");
                    }
                }
            }
            catch (Exception err)
            {
                ModelState.AddModelError("", "Невозможно выполнить вход");
            }
            return View(user);
        }

        public ActionResult LogOut()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }

        private bool RegistationFieldsNotEmply(User user)
        {
            return user.Name != null && user.Surname != null && user.Email != null && user.Password != null && user.ConfirmPassword != null;
        }

        private bool AuthFieldsNotEmply(User user)
        {
            return user.Email != null && user.Password != null;
        }

        private string ValidateRegistration(User user)
        {
            if (db.Users.Where(u => u.Email == user.Email).Count() > 0)
            {
                return "Пользователь с указанным Email уже существует";
            }

            if (!RegistationFieldsNotEmply(user))
            {
                return "Заполните все обязательные поля";
            }

            if (user.Password != user.ConfirmPassword)
            {
                return "Пароли не совпадают";
            }

            return "";
        }
    }
}