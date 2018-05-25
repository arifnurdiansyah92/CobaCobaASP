using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CobaCoba.Models
{
    public class Siswa
    {
        public int SiswaId { get; set; }
        [Display(Name="Nama")]
        [Required]
        [StringLength(15,ErrorMessage ="Nama Tidak Boleh Lebih dari 15 karakter!")]
        public string nama { get; set; }
        [Display(Name ="Kelas")]
        [Required]
        public int KelasId { get; set; }
        public Kelas Kelas { get; set; }
    }
}
