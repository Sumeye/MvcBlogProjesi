namespace MvcBlogPortfolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        public int YorumId { get; set; }

        public string YorumIcerigi { get; set; }

        public int? MakaleID { get; set; }

        public DateTime? EklenmeTarihi { get; set; }

        [StringLength(50)]
        public string AdSoyad { get; set; }

        public int? Begeni { get; set; }

        public virtual Makale Makale { get; set; }
    }
}
