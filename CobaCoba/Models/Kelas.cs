using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CobaCoba.Models
{
    public class Kelas
    {
        public int KelasId { get; set; }
        [Display(Name ="Kelas")]
        public string tingkat { get; set; }
        [Display(Name ="Jurusan")]
        public string jurusan { get; set; }
    }
}
