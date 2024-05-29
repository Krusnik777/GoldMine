using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class ServerConnection : MonoBehaviour
{
    [SerializeField] private string m_baseIP = "http://192.168.0.192:88";
    [SerializeField] private PlayerInfo m_playerInfo;

    public async UniTask<string> CheckConnection(string resource)
    {
        WebRequest request = WebRequest.Create(m_baseIP + resource);
        request.Method = "GET";
        request.UseDefaultCredentials = true;
        request.PreAuthenticate = true;
        request.Credentials = new NetworkCredential(m_playerInfo.Name, m_playerInfo.GetPasswordHash());

        string responseText = "";

        try
        {
            WebResponse response = await request.GetResponseAsync();
            StreamReader stream = new StreamReader(response.GetResponseStream());
            responseText = await stream.ReadToEndAsync();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            responseText = "Error";
        }

        return responseText;
    }

    public async UniTask<string> RequestAsync(string resource, string method, string content = "")
    {
        // Отправление запроса
        WebRequest request = WebRequest.Create(m_baseIP + resource);
        request.Method = method;
        request.UseDefaultCredentials = true;
        request.PreAuthenticate = true;
        request.Credentials = new NetworkCredential(m_playerInfo.Name, m_playerInfo.GetPasswordHash());

        if (method == "POST")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            Stream requestStream = await request.GetRequestStreamAsync();
            await requestStream.WriteAsync(bytes, 0, bytes.Length);
        }

        // Получение ответа
        string responseText = "";
        try
        {
            WebResponse response = await request.GetResponseAsync();
            StreamReader stream = new StreamReader(response.GetResponseStream());
            responseText = await stream.ReadToEndAsync();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }

        return responseText;
    }
}
