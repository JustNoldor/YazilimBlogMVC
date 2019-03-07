using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B403Blog.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class UyelerVM
    {
        public IEnumerable<B403Blog.Models.EntityFramework.Kullanici> Kullanici1 { get; set; }
        public IEnumerable<B403Blog.Models.EntityFramework.Rol> Rol { get; set; }
        public IEnumerable<B403Blog.Models.EntityFramework.KullaniciRol> KullaniciRol { get; set; }

    }
}