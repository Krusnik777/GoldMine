using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private string m_name;
    [SerializeField] private string m_password;

    public string Name => m_name;
    public string Password => m_password;

    public void SetPlayerInfo(string name, string password)
    {
        m_name = name;
        m_password = password;
    }

    public string GetPasswordHash()
    {
        StringBuilder sb = new StringBuilder();

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(m_password));

            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
        }

        //Debug.Log(sb.ToString());

        return sb.ToString();
    }
}
