using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageNotificationController : MonoBehaviour
{
    public GameObject messageNotification;

    private bool isShown;

    void Start()
    {
        isShown = false;
        messageNotification.SetActive(false);

        InvokeRepeating("ShowNotification", 2.0f, 2.0f);
    }

    private void ShowNotification()
    {
        if (isShown)
        {
            messageNotification.SetActive(false);
            isShown = false;
        }
        else
        {
            messageNotification.SetActive(true);
            isShown = true;
        }
    }
}
