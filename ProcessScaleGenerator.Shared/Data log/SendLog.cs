using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Data_log;

public static class SendLog
{
    public static void Log(Exception message)
    {       
        LogModel log = new(
            $"Exeção encontrada no {message.Source}",
            new List<embeds>
            {                
                new embeds("Menssagem do erro", message.Message?? "No messenge available"),
                new embeds("Caminho do erro", message.StackTrace?? "No stack trace available")
            });

        Send(log);
    }
    private static void Send(object message)
    {
        string url = "https://discord.com/api/webhooks/1445045857119371368/wTLu3xeXws0uFDeVES2J-PysnxoPK-_K72gwARSWM_O_3SXpFg1TgZCn1AVv9-5STKpL";

        var content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
            HttpResponseMessage response = client.PostAsync(url, content).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
        }
    }
}
