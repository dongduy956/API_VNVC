using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Config;

namespace VNVCWEBAPI.Common.Library
{
    public class Firebase
    {
        public async Task<string> PushNotification(string deviceId, string title, string body, string? image)
        {
            using (var client = new HttpClient())
            {

                var firebaseOptionsServerId = FirebaseSettings.ApplicationId;
                var firebaseOptionsSenderId = FirebaseSettings.SenderId;

                client.BaseAddress = new Uri("https://fcm.googleapis.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                    $"key={firebaseOptionsServerId}");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={firebaseOptionsSenderId}");


                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = body,
                        title = title,
                    },
                    priority = "high"
                };

                var json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync("/fcm/send", httpContent);
                return await result.Content.ReadAsStringAsync();
            }

        }
    }
}
