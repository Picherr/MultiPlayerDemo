using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketGameProtocol;
using System;

public class LoginPanel : BasePanel
{
    public LoginRequest loginRequest;
    public InputField user, pass;
    public Button loginBtn, switchBtn;

    private void Start()
    {
        loginBtn.onClick.AddListener(OnLoginClick);
        switchBtn.onClick.AddListener(SwitchLogon);
    }

    private void OnLoginClick()
    {
        if (user.text == "" || pass.text == "")
        {
            Debug.LogWarning("用户名或密码不能为空！");
            return;
        }
        loginRequest.SendRequest(user.text, pass.text);
    }

    private void SwitchLogon()
    {
        uiMag.PushPanel(PanelType.Logon);
    }

    public void OnResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Succeed:
                uiMag.ShowMessage("登录成功！");
                face.UserName = user.text;
                uiMag.PushPanel(PanelType.RoomList); // 登录成功则进入房间列表界面
                break;
            case ReturnCode.AlreadyLogin:
                uiMag.ShowMessage("该账号已登录！");
                break;
            case ReturnCode.Fail:
                uiMag.ShowMessage("该账号不存在！");
                break;
            default:
                Debug.Log("LoginPanel-def");
                break;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Enter();
    }

    public override void OnPause()
    {
        base.OnPause();
        Exit();
    }

    public override void OnRecovery()
    {
        base.OnRecovery();
        Enter();
    }

    public override void OnExit()
    {
        base.OnExit();
        Exit();
    }

    private void Enter()
    {
        gameObject.SetActive(true);
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }
}
