using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Article
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        [Display(Name = "Название*")]
        public string Title { get; set; }

        [Display(Name = "Текст статьи")]
        public string Text { get; set; }
    }
}