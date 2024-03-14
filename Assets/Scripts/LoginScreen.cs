using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LoginScreen : MonoBehaviour
{

    public Button loginButton;
    public Button cookieButton;
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    public GameObject loginCanvas;
    public GameObject cookieCanvas;
    public int score = 0;
    public TMP_Text scoreText;

    private Network.AuthenticationRequestCompleted m_AuthenticationRequestCompleted;
    private Network.AuthenticationRequestFailed m_AuthenticationRequestFailed;

    public void Set(Network.AuthenticationRequestCompleted authenticationRequestCompleted, Network.AuthenticationRequestFailed authenticationRequestFailed)
    {
        m_AuthenticationRequestCompleted = authenticationRequestCompleted;
        m_AuthenticationRequestFailed = authenticationRequestFailed;
    }
    // Start is called before the first frame update
    void Start()
    {

        cookieCanvas.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void OnLoginButtonClick() 
    {
         //if (Network.sharedInstance.HasAuthenticatedPreviously())
         //{
        //     Network.sharedInstance.Reconect();
        // }
        //  else
        // {
        Network.sharedInstance.RequestAuthenticationUniversal(usernameField.text, passwordField.text, m_AuthenticationRequestCompleted, m_AuthenticationRequestFailed);
        loginCanvas.SetActive(false);
        cookieCanvas.SetActive(true);
       // }

    }

    public void OnCookieButtonClick()
    {
        
        score = score++;
        
    }
}
