using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Авто_Ресурс_Сервис.Parser
{
    public class Tires
    {
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Radius { get; set; }
        public string TypeOfTire { get; set; }
        public string Seasons { get; set; }
        public double Price { get; set; }
        public string Articul { get; set; }
        public string Info { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeTypeOfData { get; set; }
        public bool Recomended { get; set; }
    }
}
