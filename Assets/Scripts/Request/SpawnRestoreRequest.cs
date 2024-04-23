using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRestoreRequest : BaseRequest
{
    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.SpawnRestore;
        base.Awake();
    }

    public void SendRequest(MainPack pack)
    {
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        base.SendRequest(pack);
    }
}
