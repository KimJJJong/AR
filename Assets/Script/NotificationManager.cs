using System;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    private void Start()
    {
        SetupAndroidNotifications();

    }

    private void SetupAndroidNotifications()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            var notification = new AndroidNotification
            {
                Title = "돌아와!!!!!!!!",
                Text = "안돌아오면 소리지를거ㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑABC abc 123 !@#!ㅎ",
                FireTime = DateTime.Now.AddSeconds(10), // 10초 후에 알림 전송
            };

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }

    }


}
