using System.Collections;
using UnityEngine;

public class NotificationControllerTutorial : MonoBehaviour
{
    public GameObject messageNotification;
    public GameObject messageMenu;

    public float frequencyOfOccurrence = 2.0f;

    private bool isNotificationShown;
    private bool isMessageMenuOpen;

    private bool canOpenMessageMenu;
    private bool canCloseMessageMenu;

    void Start()
    {
        messageNotification.SetActive(false);
        messageMenu.SetActive(false);

        isNotificationShown = false;
        isMessageMenuOpen = false;

        InvokeRepeating("ShowNotification", 1.0f, frequencyOfOccurrence); // call the (method, in n seconds, every n seconds) 
    }

    private void Update()
    {
        // Open menu by pushing 'R'
        if (CanOpenMessageMenu())
        {
            OpenMessageMenu();
        }

        // Close menu by pushing 'Esc'
        if (CanCloseMessageMenu())
        {
            CloseMessageMenu();
        }
    }

    // Attached to notification button
    public void OpenMessageMenu()
    {
        messageMenu.SetActive(true);
        isMessageMenuOpen = true;

        messageNotification.SetActive(false); // I do not want to see new notification while I am reading messages
    }

    // Attached to the close button in the message menu
    public void CloseMessageMenu()
    {
        messageMenu.SetActive(false);
        isMessageMenuOpen = false;

        StartCoroutine(Delay(frequencyOfOccurrence)); // I do not want to see notification at once I closed the message menu
    }

    private void ShowNotification()
    {
        if (isNotificationShown || isMessageMenuOpen)
        {
            messageNotification.SetActive(true);
            isNotificationShown = true;
        }
        else
        {
            messageNotification.SetActive(true);
            isNotificationShown = true;
        }
    }

    private bool CanOpenMessageMenu()
    {
        if (Input.GetKeyDown(KeyCode.R) && isNotificationShown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanCloseMessageMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isMessageMenuOpen)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
