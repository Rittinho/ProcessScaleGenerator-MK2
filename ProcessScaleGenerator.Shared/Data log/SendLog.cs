using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Data_log;

public static class SendLog
{
    public static void Log(object message)
    {
        string url = "https://discord.com/api/webhooks/1445045857119371368/wTLu3xeXws0uFDeVES2J-PysnxoPK-_K72gwARSWM_O_3SXpFg1TgZCn1AVv9-5STKpL";
        var webRequest = WebRequest.Create(url);
        webRequest.ContentType = "application/json";
        webRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
        {
            streamWriter.Write(JsonSerializer.Serialize(message));
        }
        
        webRequest.GetResponse().Dispose();
    }
}
