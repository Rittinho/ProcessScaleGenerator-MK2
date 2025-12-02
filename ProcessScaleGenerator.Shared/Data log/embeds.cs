using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Data_log
{
    public class embeds(string title, string description)
    {
        public string title { get; set; } = title;
        public string description { get; set; } = description;
        public ulong color { get; set; } = 16401476;
        public thumbnail thumbnail { get; set; }
    }
}
