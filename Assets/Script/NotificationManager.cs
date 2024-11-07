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
                Title = "���ƿ�!!!!!!!!",
                Text = "�ȵ��ƿ��� �Ҹ������Ť���������������������ABC abc 123 !@#!��",
                FireTime = DateTime.Now.AddSeconds(10), // 10�� �Ŀ� �˸� ����
            };

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }

    }


}
