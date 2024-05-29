using UnityEngine;
using UnityEngine.Events;

public class GoldMine : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private float m_timeForMine;

    private float timer = 0;

    private bool readyToMine;

    public event UnityAction<bool> EventOnReady;

    private void Start()
    {
        timer = m_timeForMine;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > m_timeForMine)
        {
            readyToMine = true;
            timer = 0;
            EventOnReady?.Invoke(readyToMine);
        }
    }

    private void OnMouseDown()
    {
        if (!readyToMine) return;

        m_playerController.GetGoldFromMine();
        readyToMine = false;
        EventOnReady?.Invoke(readyToMine);
    }
}
