using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class ArticlesController : Controller
    {
        private DB db = new DB();
        
        public ActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(Article article)
        {
            try
            {
                if (!ArticleFieldsNotEmply(article))
                {
                    ModelState.AddModelError("", "Заполните все обязательные поля");
                }

                if (ModelState.IsValid)
                {
                    article.UserID = int.Parse(Session["UserId"].ToString());
                    db.Articles.Add(article);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception err)
            {
                ModelState.AddModelError("", "Невозможно добавить рецепт");
            }
            return View(article);
        }

        private bool ArticleFieldsNotEmply(Article article)
        {
            return article.Title != null && article.Text != null;
        }
    }
}