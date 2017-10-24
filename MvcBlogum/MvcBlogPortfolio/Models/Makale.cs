namespace MvcBlogPortfolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Makale")]
    public partial class Makale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Makale()
        {
            Yorum = new HashSet<Yorum>();
            Etiket = new HashSet<Etiket>();
        }

        public int MakaleId { get; set; }

        [StringLength(100)]
        public string Baslik { get; set; }

        public string Icerik { get; set; }

        public DateTime? EklenmeTarihi { get; set; }

        public int? KategoriID { get; set; }

        public int? GoruntulenmeSayisi { get; set; }

        public int? Begeni { get; set; }

        public int? KullaniciID { get; set; }

        public int? ResimID { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanicilar Kullanicilar { get; set; }

        public virtual Kullanicilar Kullanicilar1 { get; set; }

        public virtual Resim Resim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiket> Etiket { get; set; }
    }
}
