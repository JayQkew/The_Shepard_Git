using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    public GameObject notificationBar;
    public TextMeshProUGUI notificationText;
    public Animator anim;
    public float hoverTime;
    [Space(10)]
    [TextArea]
    public string[] tutorialNotifications;
    [Space(10)]
    [TextArea]
    public string[] dayNotifications;


    private void Awake()
    {
        Instance = this;
        anim = notificationBar.GetComponent<Animator>();
    }

    private void Update()
    {
    }

    public void Notification(string text) => StartCoroutine(PopUpNotification(text));

    public void SetNotification(string text)
    {
        notificationText.richText = true;
        notificationText.text = text;
        anim.SetBool("notification", true);
    }

    public void RetractNotification()
    {
        anim.SetBool("notification", false);
    }

    public IEnumerator PopUpNotification(string text)
    {
        SetNotification(text);
        yield return new WaitForSeconds(hoverTime);
        RetractNotification();
    }
}
