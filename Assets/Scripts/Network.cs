using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
    public delegate void AuthenticationRequestCompleted();
    public delegate void AuthenticationRequestFailed();


    public static Network sharedInstance;

    private BrainCloudWrapper m_BrainCloud;
    
    private void Awake()
    {
        sharedInstance = this;

        DontDestroyOnLoad(gameObject);

        m_BrainCloud = gameObject.AddComponent<BrainCloudWrapper>();
        m_BrainCloud.Init();

        Debug.Log("BrainCloud Client Version: " + m_BrainCloud.Client.BrainCloudClientVersion);

    }

    // Update is called once per frame
    void Update()
    {
        m_BrainCloud.RunCallbacks();
    }

    public bool IsAuthenticated()
    {
        return m_BrainCloud.Client.Authenticated;
    }

    public void RequestAnnonymousAuthentication(AuthenticationRequestCompleted authenticationRequestCompleted = null,AuthenticationRequestFailed authenticationRequestFailed = null)
    {
        BrainCloud.SuccessCallback successCallback = (responseData, cbObject) =>
        {
            Debug.Log("RequestAnnonymousAuthentication success: " + responseData);

            if (authenticationRequestCompleted != null)
                authenticationRequestCompleted();
        };

        BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbObject) =>
        {
            Debug.Log("RequestAnnonymousAuthentication failed: " + statusMessage);

            if (authenticationRequestFailed != null)
                authenticationRequestFailed();
        };

        m_BrainCloud.AuthenticateAnonymous(successCallback, failureCallback);

    }

}
