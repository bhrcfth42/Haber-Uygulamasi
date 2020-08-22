namespace HaberPortal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resim")]
    public partial class Resim
    {
        public int Id { get; set; }

        public int HaberID { get; set; }

        [Required]
        [StringLength(150)]
        public string Ozet { get; set; }

        [Column("Resim", TypeName = "image")]
        [Required]
        public byte[] Resim1 { get; set; }

        public virtual Haber Haber { get; set; }
    }
}
