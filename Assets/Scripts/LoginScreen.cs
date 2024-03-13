using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LoginScreen : MonoBehaviour
{

    public Button loginButton;
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

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
        loginButton.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnButtonClick() 
    {
         //if (Network.sharedInstance.HasAuthenticatedPreviously())
         //{
        //     Network.sharedInstance.Reconect();
        // }
        //  else
        // {
            Network.sharedInstance.RequestAuthenticationUniversal(usernameField.text, passwordField.text, m_AuthenticationRequestCompleted, m_AuthenticationRequestFailed);
       // }

    }
}
