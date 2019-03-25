//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace B403Blog.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            this.KullaniciRol = new HashSet<KullaniciRol>();
            this.Makale = new HashSet<Makale>();
            this.YazarTakip = new HashSet<YazarTakip>();
            this.YazarTakip1 = new HashSet<YazarTakip>();
            this.YazarTakip2 = new HashSet<YazarTakip>();
        }
    
        public int KullaniciId { get; set; }
        [Required(ErrorMessage = "Ad alan� zorunludur!")]
        public string Adi { get; set; }
        [Required(ErrorMessage = "Soyad alan� zorunludur!")]
        public string Soyadi { get; set; }
        [Required(ErrorMessage = "Kulan�c� Ad� alan� zorunludur!")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Parolar alan� zorunludur!")]
        public string Parola { get; set; }
        [Required(ErrorMessage = "Mail Adresi alan� zorunludur!")]
        public string MailAdres { get; set; }
        public Nullable<bool> Cinsiyet { get; set; }
        public Nullable<System.DateTime> DogumTarihi { get; set; }
        public System.DateTime KayitTarihi { get; set; }
        public Nullable<int> ResimID { get; set; }
        public Nullable<bool> Yazar { get; set; }
        public string Role { get; set; }
        public Nullable<bool> Aktif { get; set; }
    
        public virtual Resim Resim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KullaniciRol> KullaniciRol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makale> Makale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YazarTakip> YazarTakip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YazarTakip> YazarTakip1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YazarTakip> YazarTakip2 { get; set; }
    }
}
