using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : UICanvas
{
    public Joystick joystick;
    public override void Setup()
    {
        base.Setup();
        LevelManager.Instance.m_MainPlayer.joystick = joystick;
    }
    public override void Close()
    {
        base.Close();
        LevelManager.Instance.m_MainPlayer.joystick = null;
    }
}
