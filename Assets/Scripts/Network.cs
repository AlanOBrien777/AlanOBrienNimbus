using UnityEngine;
using BrainCloud.JsonFx;
using LitJson;



public class Network : MonoBehaviour
{
    public delegate void AuthenticationRequestCompleted();
    public delegate void AuthenticationRequestFailed();

    public delegate void UpdateUserNameRequestCompleted();
    public delegate void UpdateUserNameRequestFailed();


    public static Network sharedInstance;

    private BrainCloudWrapper m_BrainCloud;

    private string m_Username;
    
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

   // public string GetUserName()
    //{
       // return m_Username;
   // }

    //public bool HasAuthenticatedPreviously()
    //{
    //    return m_BrainCloud.GetStoredProfileId() != "" && m_BrainCloud.GetStoredAnonymousId() != "";
   // }
//
    public bool IsAuthenticated()
    {
        return m_BrainCloud.Client.Authenticated;
    }

   // public void Reconect(AuthenticationRequestCompleted authenticationRequestCompleted = null, AuthenticationRequestFailed authenticationRequestFailed = null) 
   // {
   //     BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
   //     {
    //        Debug.Log("Reconnect authentication success: " + responseData);
//
    //        HandleAuthenticationSuccess(responseData, cbObject, authenticationRequestCompleted);
    //    };

    //    BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
    //    {
     //       Debug.Log("Reconnect authentication failed: " + statusMessage);
//
     //       if (authenticationRequestFailed != null)
      //          authenticationRequestFailed();
      //  };

     //   m_BrainCloud.Reconnect(successCallback, failureCallback);
  //  }


    public void RequestAnnonymousAuthentication(AuthenticationRequestCompleted authenticationRequestCompleted = null,AuthenticationRequestFailed authenticationRequestFailed = null)
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

    public void RequestAuthenticationUniversal(string userID, string password, AuthenticationRequestCompleted authenticationRequestCompleted = null, AuthenticationRequestFailed authenticationRequestFailed = null )
    {
        BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
        {
            Debug.Log("Universal Authentication success: " + responseData);

            HandleAuthenticationSuccess(responseData, cbObject, authenticationRequestCompleted);
        };

        BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
        {
            Debug.Log("Universal Authentication failed: " + statusMessage);

            if (authenticationRequestFailed != null)
                authenticationRequestFailed();
        };

        m_BrainCloud.AuthenticateUniversal(userID, password,true, successCallback, failureCallback);

        
    }

    [System.Obsolete]
    public void RequestUserName(string username,UpdateUserNameRequestCompleted updateUserNameRequestCompleted = null,UpdateUserNameRequestFailed updateUserNameRequestFailed = null)
    {
       
        
            BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
            {
                Debug.Log("Username Request Success: " + responseData);

                JsonData jsonData = JsonMapper.ToObject(responseData);
                m_Username = jsonData["data"]["playerCount"].ToString();

                if(updateUserNameRequestCompleted != null)
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

    private void HandleAuthenticationSuccess(string responseData, object cbObject, AuthenticationRequestCompleted authenticationRequestCompleted)
    {
        JsonData jsonData = JsonMapper.ToObject(responseData);
        m_Username = jsonData["data"]["loginCount"].ToString();
        Debug.Log(m_Username);

        if(authenticationRequestCompleted != null)
            authenticationRequestCompleted();
    }

}
