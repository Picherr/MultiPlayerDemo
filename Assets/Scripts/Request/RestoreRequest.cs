using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreRequest : BaseRequest
{
    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Restore;
        base.Awake();
    }

    public void SendRequest(string player, RestoreType type)
    {
        MainPack pack = new MainPack();
        RestorePack restorePack = new RestorePack();

        restorePack.User = player;
        restorePack.Restoretype = type;

        pack.Restorepack = restorePack;
        base.SendRequest(pack);
    }
}
