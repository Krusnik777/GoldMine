using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanel : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject m_startPanel;
    [SerializeField] private GameObject m_connectionPanel;
    [SerializeField] private Text m_connectionText;
    [SerializeField] private InputField m_nicknameField;
    [SerializeField] private InputField m_passwordField;
    [SerializeField] private Button m_signInButton;
    [SerializeField] private Button m_signUpButton;
    [Header("Components")]
    [SerializeField] private PlayerInfo m_playerInfo;
    [SerializeField] private ServerConnection m_serverConnection;
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private GoldMine m_goldMine;

    public async void SignIn()
    {
        m_connectionPanel.SetActive(true);
        m_connectionText.text = "Trying to connect...";

        m_playerInfo.SetPlayerInfo(m_nicknameField.text, m_passwordField.text);

        string response = await m_serverConnection.CheckConnection("/playerStats");

        if (response != "Denied" && response != "Error")
        {
            m_playerController.StartPlayerUpdate();
            m_goldMine.enabled = true;
            m_startPanel.SetActive(false);
            enabled = false;
        }
        else if (response == "Denied") DisplayConnectionPanel("Access Denied");
        else if (response == "Error") DisplayConnectionPanel("Connection Error");
    }

    public async void SignUp()
    {
        m_connectionPanel.SetActive(true);
        m_connectionText.text = "Trying to sign up...";

        m_playerInfo.SetPlayerInfo(m_nicknameField.text, m_passwordField.text);

        string response = await m_serverConnection.CheckConnection("/playerStats");

        if (response != "Denied" && response != "Error") DisplayConnectionPanel("Already Signed Up");
        else if (response == "Error") DisplayConnectionPanel("Connection Error");
        else if (response == "Denied")
        {
            string responseMessage = await m_serverConnection.RequestAsync("/playerStats", "POST", "SignUpNewPlayer");
            Debug.Log(responseMessage);
            DisplayConnectionPanel("Registration Completed", "green");
        }
    }

    private void Awake()
    {
        if (m_goldMine.enabled) m_goldMine.enabled = false;
        if (m_connectionPanel.activeInHierarchy) m_connectionPanel.SetActive(false);

        m_signInButton.onClick.AddListener(SignIn);
        m_signUpButton.onClick.AddListener(SignUp);
    }

    private void OnDisable()
    {
        m_signInButton.onClick.RemoveListener(SignIn);
        m_signUpButton.onClick.RemoveListener(SignUp);
    }

    private void Update()
    {
        m_signInButton.interactable = !string.IsNullOrEmpty(m_nicknameField.text) && !string.IsNullOrEmpty(m_passwordField.text);
        m_signUpButton.interactable = !string.IsNullOrEmpty(m_nicknameField.text) && !string.IsNullOrEmpty(m_passwordField.text);
    }

    private async void DisplayConnectionPanel(string message, string colorName = "")
    {
        if (m_connectionText != null)
        {
            m_connectionText.text = message;
            var defaultColor = m_connectionText.color;
            if (colorName == "") m_connectionText.color = Color.red;
            if (colorName == "green") m_connectionText.color = Color.green;

            await UniTask.WaitForSeconds(1);

            m_connectionText.color = defaultColor;
            if (m_connectionPanel != null) m_connectionPanel.SetActive(false);
        }
    }
}
