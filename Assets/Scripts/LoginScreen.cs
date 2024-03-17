using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class LoginScreen : MonoBehaviour
{

    public Button loginButton;
    public Button cookieButton;
    public Button GuestButton;
    public Button logOutButton;
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField newUsernameField;
    public GameObject newusername;
    public GameObject loginCanvas;
    public GameObject cookieCanvas;
    public GameObject highScoreText;
    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text userNameText;

   

    private Network.AuthenticationRequestCompleted m_AuthenticationRequestCompleted;
    private Network.AuthenticationRequestFailed m_AuthenticationRequestFailed;

    private Network.PostScoreRequestCompleted m_PostScoreRequestCompleted;
    private Network.PostScoreRequestFailed m_PostScoreRequestFailed;

    private Network.BrainCloudLogOutCompleted m_BrainCloudLogOutCompleted;
    private Network.BrainCloudLogOutFailed m_BrainCloudLogOutFailed;

    public void Set(Network.AuthenticationRequestCompleted authenticationRequestCompleted, Network.AuthenticationRequestFailed authenticationRequestFailed, Network.PostScoreRequestCompleted postScoreRequestCompleted,Network.PostScoreRequestFailed postScoreRequestFailed, Network.BrainCloudLogOutCompleted brainCloudLogOutCompleted, Network.BrainCloudLogOutFailed brainCloudLogOutFailed)
    {
        m_AuthenticationRequestCompleted = authenticationRequestCompleted;
        m_AuthenticationRequestFailed = authenticationRequestFailed;
        m_PostScoreRequestCompleted = postScoreRequestCompleted;
        m_PostScoreRequestFailed = postScoreRequestFailed;
        m_BrainCloudLogOutCompleted = brainCloudLogOutCompleted;
        m_BrainCloudLogOutFailed = brainCloudLogOutFailed;
    }
    // Start is called before the first frame update
    void Start()
    {

        cookieCanvas.SetActive(false);
        highScoreText.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if(score > Network.sharedInstance.highscore)
        {
            highScoreText.SetActive(true);
        }
    }

    public void OnLoginButtonClick()
    {
       // if (Network.sharedInstance.HasAuthenticatedPreviously())
       // {
      //      Network.sharedInstance.Reconect();
      //  }
      //  else
      //  {
            Network.sharedInstance.RequestAuthenticationUniversal(usernameField.text, passwordField.text, m_AuthenticationRequestCompleted, m_AuthenticationRequestFailed);

      //  }
        loginCanvas.SetActive(false);
        cookieCanvas.SetActive(true);
        userNameText.text = "Username: " + usernameField.text.ToString();

       // Debug.Log(Network.sharedInstance.GetUserName());
        //  Network.sharedInstance.RequestUserName(usernameField.text.ToString());
        usernameField.text = "";
        passwordField.text = "";

        Network.sharedInstance.RequestLeaderboard(Constants.kBrainCloudMainLeaderbaordID,OnLeaderboardRequestCompleted);
    }

    public void LoginAsGuest()
    {
        loginCanvas.SetActive(false);
        cookieCanvas.SetActive(true);
        Network.sharedInstance.RequestAnnonymousAuthentication(m_AuthenticationRequestCompleted, m_AuthenticationRequestFailed);
        userNameText.text = "Guest";
        newusername.SetActive(false);
    }

    

    public void OnCookieButtonClick()
    {
        // Debug.Log("Is this working?");
        score = score + 1;
    }

    public void OnUsernameButtonClick()
    {
        userNameText.text = "Username: " + newUsernameField.text.ToString();
        Network.sharedInstance.RequestUserName(newUsernameField.text.ToString());
        newUsernameField.text = "";
    }

    public void LogOutButton()
    {
        loginCanvas.SetActive(true);
        cookieCanvas.SetActive(false);
        highScoreText.SetActive(false);
        Network.sharedInstance.PostScoreToLeaderboard(Constants.kBrainCloudMainLeaderbaordID,score,Network.sharedInstance.GetUserName(),m_PostScoreRequestCompleted,m_PostScoreRequestFailed);
        Network.sharedInstance.RequestUserName(Network.sharedInstance.GetUserName());
        score = 0;

        Network.sharedInstance.LogOut(m_BrainCloudLogOutCompleted,m_BrainCloudLogOutFailed) ;
    }

    public void OnLeaderboardRequestCompleted(Leaderboard leaderboard)
    {
        LeaderboardsManager.sharedInstance.AddLeaderboard(leaderboard);
    }


}
