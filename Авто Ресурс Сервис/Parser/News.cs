﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Авто_Ресурс_Сервис.Parser
{
    public class News
    {
        //public int id { get; set; }
        public string NameNews { get; set; }
        public string BaseInfo { get; set; }
        public string AllInfo { get; set; }
        public string NewsLinkSrc { get; set; }
        public DateTime DateTime { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeTypeOfData { get; set; }

    }
}
