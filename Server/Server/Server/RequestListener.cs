using System;
using System.IO;
using System.Net;
using System.Text;

namespace Server
{
    public class RequestListener
    {
        private PlayerList playerList;
        private HttpListener listener;
        private ResponseCollection responseCollection;

        public RequestListener(PlayerList playerList, ResponseCollection responseCollection, string prefix)
        {
            this.playerList = playerList;
            this.responseCollection = responseCollection;

            listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.AuthenticationSchemes = AuthenticationSchemes.Basic;
        }

        public async void StartRequestListen()
        {
            listener.Start();

            while (true)
            {
                // Получение запроса
                HttpListenerContext context = await listener.GetContextAsync();

                HttpListenerBasicIdentity identity = (HttpListenerBasicIdentity)context.User.Identity;

                PlayerInfo playerInfo = new PlayerInfo(identity.Name, identity.Password);

                if (!playerList.CheckPlayerExist(playerInfo))
                {
                    Console.WriteLine($"Request was received from a user {identity.Name} who is not in the database");

                    if (context.Request.HttpMethod == "GET")
                    {
                        string responseMessage = "Denied";

                        SendResponseAsync(context.Response, playerInfo, responseMessage);

                        await context.Response.OutputStream.FlushAsync();
                    }

                    if (context.Request.HttpMethod == "POST")
                    {
                        string responseMessage = responseCollection.GetResponseForPOST(context.Request.RawUrl, "SignUpNewPlayer", playerInfo);

                        if (responseMessage != "") SendResponseAsync(context.Response, playerInfo, responseMessage);

                        await context.Response.OutputStream.FlushAsync();
                    }

                    continue;
                }

                Console.WriteLine($"REQUEST: USER: {identity.Name}; METHOD: {context.Request.HttpMethod}; URL: {context.Request.RawUrl};");

                if (context.Request.HttpMethod == "GET")
                {
                    string responseMessage = responseCollection.GetResponseForGET(context.Request.RawUrl, playerInfo);

                    if (responseMessage != "") SendResponseAsync(context.Response, playerInfo, responseMessage);

                    await context.Response.OutputStream.FlushAsync();
                }

                if (context.Request.HttpMethod == "POST")
                {
                    string content = "";

                    StreamReader inputStream = new StreamReader(context.Request.InputStream);
                    content = inputStream.ReadToEnd();
                    Console.WriteLine($"CONTENT: {content};");

                    string responseMessage = responseCollection.GetResponseForPOST(context.Request.RawUrl, content, playerInfo);

                    if (responseMessage != "") SendResponseAsync(context.Response, playerInfo, responseMessage);

                    await context.Response.OutputStream.FlushAsync();
                }
            }
        }

        public async void SendResponseAsync(HttpListenerResponse response, PlayerInfo playerInfo, string responseText)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(responseText);

            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            await output.WriteAsync(buffer, 0, buffer.Length);

            Console.WriteLine($"RESPONSE: USER: {playerInfo.Name}; METHOD: POST; CONTENT: {responseText};");
        }
    }
}
