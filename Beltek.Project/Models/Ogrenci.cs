using System.Drawing;

namespace Beltek.Project.Models
{
    public class Ogrenci
    {
        public int OgrenciId { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public int Numara { get; set; }
        public int SinifId { get; set; }
        public Sinif Sinif { get; set; }
    }
}
