using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private Text m_goldText;
    [SerializeField] private Text m_levelText;

    private void Update()
    {
        m_goldText.text = m_playerController.PlayerStats.Gold.ToString();
        m_levelText.text = m_playerController.PlayerStats.Level.ToString();
    }
}
