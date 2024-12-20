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
                Title = "宜焼人!!!!!!!!",
                Text = "照宜焼神檎 社軒走研暗ちちちちちちちちちちちABC abc 123 !@#!ぞ",
                FireTime = DateTime.Now.AddSeconds(10), // 10段 板拭 硝顕 穿勺
            };

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }

    }


}
