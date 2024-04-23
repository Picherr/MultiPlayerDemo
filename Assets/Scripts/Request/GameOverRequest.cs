using SocketGameProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRequest : BaseRequest
{
    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.GameOver;
        base.Awake();
    }

    private void Update()
    {
        if (pack != null)
        {
            face.GameOver(pack);
            pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }

    public void SendRequest()
    {
        MainPack pack = new MainPack();
        pack.Requestcode = RequestCode.Game;
        pack.Actioncode = ActionCode.GameOver;
        pack.Str = "None ";
        base.SendRequest(pack);
    }
}
