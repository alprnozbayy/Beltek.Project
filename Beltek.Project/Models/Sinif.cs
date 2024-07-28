using System.ComponentModel.DataAnnotations;

namespace Beltek.Project.Models
{
    public class Sinif
    {
        public int SinifId { get; set; }
        public string SinifAdi { get; set; }
        public int Kontenjan { get; set; }
        public ICollection<Ogrenci> Ogrenciler { get; set; }
    }
}
