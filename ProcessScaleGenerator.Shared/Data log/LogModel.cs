using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Data_log
{
    public class LogModel(string content, List<embeds> embeds)
    {
        public string content { get; set; } = content;
        public List<embeds> embeds { get; set; } = embeds;
        public string username { get; set; } = "Toyota logs";
        public string avatar_url { get; set; } = "https://www.freeiconspng.com/uploads/toyota-logo-png-23.PNG";
        public List<string> attachments { get; set; } = [];
    }
}
