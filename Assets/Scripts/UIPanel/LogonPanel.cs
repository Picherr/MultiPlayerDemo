using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketGameProtocol;

public class LogonPanel : BasePanel
{
    public LogonRequest logonRequest;
    public InputField user, pass;
    public Button logonBtn, switchBtn;

    private void Start()
    {
        logonBtn.onClick.AddListener(OnLogonClick);
        switchBtn.onClick.AddListener(SwitchLogin);
    }

    private void OnLogonClick()
    {
        if (user.text == "" || pass.text == "")
        {
            Debug.LogWarning("�û��������벻��Ϊ�գ�");
            return;
        }
        logonRequest.SendRequest(user.text, pass.text);
    }

    private void SwitchLogin()
    {
        uiMag.PopPanel();
    }

    public void OnResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Succeed:
                uiMag.ShowMessage("ע��ɹ���");
                uiMag.PushPanel(PanelType.Login); // ע��ɹ���ص���¼����
                break;
            case ReturnCode.AlreadyLogon:
                uiMag.ShowMessage("���˺��Ѿ�ע�ᣡ");
                break;
            case ReturnCode.Fail:
                uiMag.ShowMessage("ע��ʧ�ܣ�");
                break;
            default:
                Debug.Log("def");
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
