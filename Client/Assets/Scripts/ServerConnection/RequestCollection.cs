using Cysharp.Threading.Tasks;
using UnityEngine;


public class RequestCollection : MonoBehaviour
{
    [SerializeField] private ServerConnection m_serverConnection;

    public async UniTask<PlayerStats> UpdatePlayerStatsAsync(PlayerStats playerStats)
    {
        string responseMessage = await m_serverConnection.RequestAsync("/playerStats", "GET");

        PlayerStats stats = playerStats;

        if (responseMessage != "")
        {
            stats = JsonUtility.FromJson<PlayerStats>(responseMessage);
        }

        return stats;
    }

    public async UniTask<PlayerStats> UpgradeLevelAsync(PlayerStats playerStats)
    {
        string responseMessage = await m_serverConnection.RequestAsync("/playerStats", "POST", "UpgradeLevel");

        PlayerStats stats = playerStats;

        if (responseMessage != "")
        {
            stats = JsonUtility.FromJson<PlayerStats>(responseMessage);
        }

        return stats;
    }

    public async UniTask<PlayerStats> GetGoldFromMineAsync(PlayerStats playerStats)
    {
        string responseMessage = await m_serverConnection.RequestAsync("/playerStats", "POST", "GetGoldFromMine");

        PlayerStats stats = playerStats;

        if (responseMessage != "")
        {
            stats = JsonUtility.FromJson<PlayerStats>(responseMessage);
        }

        return stats;
    }
}
