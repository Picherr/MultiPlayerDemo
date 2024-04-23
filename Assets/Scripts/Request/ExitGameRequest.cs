using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameRequest : BaseRequest
{
    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.ExitGame;
        base.Awake();
    }

    public void SendRequest()
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        pack.Str = "r";
        base.SendRequest(pack);
    }

    private void Update()
    {
        if (pack != null)
        {
            face.GameExit();
            pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
