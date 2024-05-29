using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RequestCollection m_requestCollection;
    [SerializeField] private PlayerStats m_playerStats;
    public PlayerStats PlayerStats => m_playerStats;

    public async void UpdatePlayerStats()
    {
        m_playerStats = await m_requestCollection.UpdatePlayerStatsAsync(m_playerStats);
    }

    public async void UpgradeLevel()
    {
        m_playerStats = await m_requestCollection.UpgradeLevelAsync(m_playerStats);
    }

    public async void GetGoldFromMine()
    {
        m_playerStats = await m_requestCollection.GetGoldFromMineAsync(m_playerStats);
    }

    public void StartPlayerUpdate()
    {
        StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        UpdatePlayerStats();

        while (true)
        {
            yield return new WaitForSeconds(1);
            UpdatePlayerStats();
        }
    }
}
