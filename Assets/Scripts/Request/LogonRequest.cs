using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using System.Runtime.InteropServices;

public class LogonRequest : BaseRequest
{
    public LogonPanel logonPanel;

    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Logon;
        base.Awake();
    }

    private void Update()
    {
        if (pack != null)
        {
            logonPanel.OnResponse(pack);
            pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }

    public void SendRequest(string user, string pass)
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        LoginPack loginPack = new LoginPack();
        loginPack.Username = user;
        loginPack.Password = pass;
        pack.Loginpack = loginPack;

        base.SendRequest(pack);
    }
}
