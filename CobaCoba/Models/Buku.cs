using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CobaCoba.Models
{
    public class Buku
    {
        public int BukuId { get; set; }
        [Display(Name = "Judul")]
        public string judul { get; set; }
        [Display(Name = "Author")]
        public string penulis { get; set; }
        [Display(Name = "Tahun Terbit")]
        public string tahunTerbit { get; set; }
        [Display(Name = "Harga")]
        public string harga { get; set; }
    }
}
