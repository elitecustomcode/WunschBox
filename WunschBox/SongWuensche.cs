using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WunschBox
{
    public class SongWuensche
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string ip { get; set; }
        public string suche { get; set; }
        public string datum { get; set; }
        public string wunschtitel { get; set; }
        public string fuer_wen { get; set; }
        public string von_wen { get; set; }
        public string text { get; set; }
        public string gesendet { get; set; }
        public string sound_an { get; set; }
        public string user_dj { get; set; }
    }
}
