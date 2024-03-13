using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{

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
        return false;
    }
}
