namespace HaberApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;
    using System.Web.Mvc;

    [Table("Haber")]
    public partial class Haber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Haber()
        {
            Resim1 = new HashSet<Resim>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Baþlýk alanýný gereklidir.")]
        [StringLength(150)]
        public string Baslik { get; set; }

        [Required(ErrorMessage ="Özet alaný gereklidir.")]
        [StringLength(300)]
        public string Ozet { get; set; }
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage ="Ýçerik alaný gereklidir")]
        public string Icerik { get; set; }

        public DateTime YayimTarihi { get; set; }

        public Nullable<Guid> YazarID { get; set; }

        public int KategoriID { get; set; }

        public int TipID { get; set; }
        [Column(TypeName = "image")]
        public byte[] Resim { get; set; }
        [Column(TypeName = "image")]
        public byte[] KucukResim { get; set; }        

        public int Goruntulenme { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Tip Tip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resim> Resim1 { get; set; }
    }
}
