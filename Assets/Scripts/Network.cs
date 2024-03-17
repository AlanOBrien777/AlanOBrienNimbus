using UnityEngine;
using BrainCloud.JsonFx;
using LitJson;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;



public class Network : MonoBehaviour
{
    public delegate void AuthenticationRequestCompleted();
    public delegate void AuthenticationRequestFailed();

    public delegate void BrainCloudLogOutCompleted();
    public delegate void BrainCloudLogOutFailed();

    public delegate void UpdateUserNameRequestCompleted();
    public delegate void UpdateUserNameRequestFailed();

    public delegate void UpdatecoreRequestCompleted();
    public delegate void UpdatecoreRequestFailed();

    public delegate void LeaderboardRequestCompleted(Leaderboard leaderboard);
    public delegate void LeaderboardRequestFailed();

    public delegate void PostScoreRequestCompleted();
    public delegate void PostScoreRequestFailed();

    public delegate void HighScoreRequestCompleted();
    public delegate void HighScoreRequestFailed();

   
    public static Network sharedInstance;

    private BrainCloudWrapper m_BrainCloud;

    private string m_Username;
    public int highscore = 0;

    private void Awake()
    {
        sharedInstance = this;

        DontDestroyOnLoad(gameObject);

        m_BrainCloud = gameObject.AddComponent<BrainCloudWrapper>();
        m_BrainCloud.Init();

        Debug.Log("BrainCloud Client Version: " + m_BrainCloud.Client.BrainCloudClientVersion);

        m_Username = "";

    }

    // Update is called once per frame
    void Update()
    {
        m_BrainCloud.RunCallbacks();
    }

    public string GetUserName()
    {
       // Debug.Log(m_Username);
        return m_Username;
        
    }

    public bool HasAuthenticatedPreviously()
    {
        return m_BrainCloud.GetStoredProfileId() != "" && m_BrainCloud.GetStoredAnonymousId() != "";
    }

    public bool IsUsernameSaved()
    {
        return m_Username != "";
    }
    public bool IsAuthenticated()
    {
        return m_BrainCloud.Client.Authenticated;
    }

    public void LogOut(BrainCloudLogOutCompleted brainCloudLogOutCompleted = null, BrainCloudLogOutFailed brainCloudLogOutFailed = null)
    {
        BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
        {
            Debug.Log("Logout success: " + responseData);
            m_BrainCloud.ResetStoredAnonymousId();
            m_BrainCloud.ResetStoredProfileId();

            if (brainCloudLogOutCompleted != null)
                brainCloudLogOutCompleted();
            
        };

        BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
        {
            Debug.Log("Logout failed: " + statusMessage);

            if (brainCloudLogOutFailed != null)
                brainCloudLogOutFailed();
        };

        m_BrainCloud.PlayerStateService.Logout(successCallback, failureCallback);
    }


   // public void Reconect(AuthenticationRequestCompleted authenticationRequestCompleted = null, AuthenticationRequestFailed authenticationRequestFailed = null)
  //  {
    //    BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
    //    {
     //       Debug.Log("Reconnect authentication success: " + responseData);

     //       HandleAuthenticationSuccess(responseData, cbObject, authenticationRequestCompleted);
     //       JsonData jsonData = JsonMapper.ToObject(responseData);
     //       m_Username = jsonData["data"]["playerName"].ToString();
            
     //   };

     //   BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
    //    {
     //       Debug.Log("Reconnect authentication failed: " + statusMessage);

     //       if (authenticationRequestFailed != null)
    //            authenticationRequestFailed();
    //    };

     //   m_BrainCloud.Reconnect(successCallback, failureCallback);
  //  }


    public void RequestAnnonymousAuthentication(AuthenticationRequestCompleted authenticationRequestCompleted = null, AuthenticationRequestFailed authenticationRequestFailed = null)
    {
        BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
        {
            Debug.Log("RequestAnnonymousAuthentication success: " + responseData);

            HandleAuthenticationSuccess(responseData, cbObject, authenticationRequestCompleted);
        };

        BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
        {
            Debug.Log("RequestAnnonymousAuthentication failed: " + statusMessage);

            if (authenticationRequestFailed != null)
                authenticationRequestFailed();
        };

        m_BrainCloud.AuthenticateAnonymous(successCallback, failureCallback);

    }

    public void RequestAuthenticationUniversal(string userID, string password, AuthenticationRequestCompleted authenticationRequestCompleted = null, AuthenticationRequestFailed authenticationRequestFailed = null)
    {
        BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
        {
            Debug.Log("Universal Authentication success: " + responseData);

            HandleAuthenticationSuccess(responseData, cbObject, authenticationRequestCompleted);
            JsonData jsonData = JsonMapper.ToObject(responseData);
            // m_Username = jsonData["data"]["playerName"].ToString();
            m_Username = userID;
        };

        BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
        {
            Debug.Log("Universal Authentication failed: " + statusMessage);

            if (authenticationRequestFailed != null)
                authenticationRequestFailed();
        };

        m_BrainCloud.AuthenticateUniversal(userID, password, true, successCallback, failureCallback);


    }

    [System.Obsolete]
    public void RequestUserName(string username, UpdateUserNameRequestCompleted updateUserNameRequestCompleted = null, UpdateUserNameRequestFailed updateUserNameRequestFailed = null)
    {


        BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
        {
            Debug.Log("Username Request Success: " + responseData);

            JsonData jsonData = JsonMapper.ToObject(responseData);
            // m_Username = jsonData["data"]["playerName"].ToString();
            m_Username = username;

            if (updateUserNameRequestCompleted != null)
            {
                updateUserNameRequestCompleted();
            }
        };

        BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
        {
            Debug.Log("Username request failed: " + statusMessage);

            if (updateUserNameRequestFailed != null)
                updateUserNameRequestFailed();
        };

        m_BrainCloud.PlayerStateService.UpdateUserName(username, successCallback, failureCallback);



    }

    public void RequestLeaderboard(string leaderboardId,LeaderboardRequestCompleted leaderboardRequestCompleted = null,LeaderboardRequestFailed leaderboardRequestFailed = null) 
    {
        RequestLeaderboard(leaderboardId,Constants.kBrainCloudDefaultMinHighScoreIndex,Constants.kBrainCloudDefaultMaxHighScoreIndex,leaderboardRequestCompleted,leaderboardRequestFailed);
    }

    public void RequestLeaderboard(string leaderboardId,int startIndex,int endIndex, LeaderboardRequestCompleted leaderboardRequestCompleted = null,LeaderboardRequestFailed leaderboardRequestFailed = null)
    {
       
        
            BrainCloud.SuccessCallback successCallback = (responseData, cbObjext) =>
            {
                Debug.Log("RequestHighScore success" + responseData);
                JsonData jsonData = JsonMapper.ToObject(responseData);
                JsonData leaderboard = jsonData["data"]["leaderboard"];
         

                List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
                int rank = 0;
                string nickname;
                int score = 0;
                

                if (leaderboard.IsArray)
                {
                    for (int i = 0; i < leaderboard.Count; i++)
                    {
                        rank = int.Parse(leaderboard[i]["rank"].ToString());
                        nickname = leaderboard[i]["data"]["nickname"].ToString();
                        score = int.Parse(leaderboard[i]["score"].ToString());
                        if(score > highscore)
                        {
                            highscore = score;
                        }

                        leaderboardEntries.Add(new LeaderboardEntry(nickname,rank,score));
                    }
                }

                Leaderboard lb = new Leaderboard(leaderboardId, leaderboardEntries);

                if(leaderboardRequestCompleted != null)
                    leaderboardRequestCompleted(lb);

            };

            BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
            {
                Debug.Log("RequestHighscore failed " + statusMessage);

                if (leaderboardRequestFailed != null)
                    leaderboardRequestFailed();
            };

            m_BrainCloud.LeaderboardService.GetGlobalLeaderboardPage(leaderboardId,BrainCloud.BrainCloudSocialLeaderboard.SortOrder.HIGH_TO_LOW,startIndex,endIndex,successCallback,failureCallback);
        
        
    }

    public void PostScoreToLeaderboard(string leaderboardId, int score, PostScoreRequestCompleted postScoreRequestCompleted = null, PostScoreRequestFailed postScoreRequestFailed = null)
    {
        PostScoreToLeaderboard(leaderboardId,score,GetUserName(), postScoreRequestCompleted,postScoreRequestFailed);
    }

    public void PostScoreToLeaderboard(string leaderboardId, int score, string nickname, PostScoreRequestCompleted postScoreRequestCompleted = null, PostScoreRequestFailed postScoreRequestFailed = null)
    {
       
        
            BrainCloud.SuccessCallback successCallback = (responseData, cbObjext) =>
            {
                Debug.Log("postscore success" + responseData);
                if (postScoreRequestCompleted != null)
                    postScoreRequestCompleted();
            };

            BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
            {
                Debug.Log("postscore failed" + statusMessage);
                if(postScoreRequestFailed != null)
                    postScoreRequestFailed();
            };

            string jsonOtherData = "{\"nickname\":\"" + nickname + "\"}";
            m_BrainCloud.LeaderboardService.PostScoreToLeaderboard(leaderboardId, score, jsonOtherData, successCallback, failureCallback);


        
        
        
            Debug.Log("postcore failed not authenticated");
            if (postScoreRequestFailed != null)
                postScoreRequestFailed();
        
    }

    private void HandleAuthenticationSuccess(string responseData, object cbObject, AuthenticationRequestCompleted authenticationRequestCompleted)
    {
        JsonData jsonData = JsonMapper.ToObject(responseData);
        m_Username = jsonData["data"]["playerName"].ToString();
        // Debug.Log(m_Username);

        if (authenticationRequestCompleted != null)
            authenticationRequestCompleted();
    }

    







}
