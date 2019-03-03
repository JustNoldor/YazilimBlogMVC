namespace B403Blog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Yazar = new HashSet<Yazar>();
        }

        public int KullaniciId { get; set; }

        [Required]
        [StringLength(50)]
        public string Adi { get; set; }

        [Required]
        [StringLength(50)]
        public string Soyadi { get; set; }

        [Required]
        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        [Required]
        [StringLength(200)]
        public string Parola { get; set; }

        [Required]
        [StringLength(50)]
        public string MailAdres { get; set; }

        public bool? Cinsiyet { get; set; }

        public DateTime? DogumTarihi { get; set; }

        public DateTime KayitTarihi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yazar> Yazar { get; set; }
    }
}
