using UnityEngine;
using UnityEngine.UI;

public class UIGoldMine : MonoBehaviour
{
    [SerializeField] private GoldMine m_goldMine;
    [SerializeField] private Text m_text;

    private void Start()
    {
        m_goldMine.EventOnReady += OnMineReady;
    }

    private void OnDestroy()
    {
        m_goldMine.EventOnReady -= OnMineReady;
    }

    private void OnMineReady(bool ready) => m_text.gameObject.SetActive(ready);
}
