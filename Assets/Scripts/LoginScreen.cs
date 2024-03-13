using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class LoginScreen : MonoBehaviour
{

    public Button loginButton;
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
        if (Network.sharedInstance.HasAuthenticatedPreviously())
        {
            Network.sharedInstance.Reconect();
        }
        else
        {
            Network.sharedInstance.RequestAnnonymousAuthentication();
        }
    }
}
