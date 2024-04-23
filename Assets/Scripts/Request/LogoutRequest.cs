using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutRequest : BaseRequest
{
    public RoomListPanel roomListPanel;

    private MainPack pack;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Logout;
        base.Awake();
    }

    private void Update()
    {
        
    }

    public void SendRequest(string user)
    {
        Debug.Log("Ω¯»ÎLogoutRequest");
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        LoginPack loginPack = new LoginPack();
        loginPack.Username = user;
        pack.Loginpack = loginPack;

        base.SendRequest(pack);
    }
}
