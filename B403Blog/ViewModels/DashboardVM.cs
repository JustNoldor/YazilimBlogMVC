using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B403Blog.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class DashboardVM
    {
        public IEnumerable<B403Blog.Models.EntityFramework.Makale> Makale { get; set; }
        public IEnumerable<B403Blog.Models.EntityFramework.Kategori> Kategori { get; set; }
        public IEnumerable<B403Blog.Models.EntityFramework.Kullanici> Kullanici { get; set; }
        public IEnumerable<B403Blog.Models.EntityFramework.Makale> MakaleSon { get; set; }


    }
}