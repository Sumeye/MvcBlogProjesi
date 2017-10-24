using MvcBlogPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBlogPortfolio.Tools
{
    public class Myblog
    {
        public Makale makale { get; set; }
        public List<Kategori> kategori { get; set; }
        public List<Makale> makaleler { get; set; }
        public List<Kullanicilar> kullanici { get; set; }
        public List<Yorum> yorum { get; set; }

    }
}