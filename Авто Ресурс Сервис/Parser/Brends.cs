using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Авто_Ресурс_Сервис.Parser
{
    class Brends
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public bool OnMain { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeTypeOfData { get; set; }
        public int Position { get; set; }
    }
}
