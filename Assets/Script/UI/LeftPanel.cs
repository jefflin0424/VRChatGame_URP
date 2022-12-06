using System;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanel : MonoBehaviour
{
    GameManager instance;

    [SerializeField]
    Toggle toggle;

    public Action<bool> playerShootCallback;

    void Start()
    {
        instance = GameManager.Instance;

        //var playerControl = instance.player.GetComponent<CameraFlyControl>();

        //playerShootCallback = x => playerControl.Shoot();

        toggle.isOn = false;
    }

    public void ChangePlayerShoot(bool on)
    {
        toggle.isOn = on;
    }
}
