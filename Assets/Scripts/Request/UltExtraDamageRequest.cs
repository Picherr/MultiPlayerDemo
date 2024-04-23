using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltExtraDamageRequest : BaseRequest
{
    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.UltExtraDamage;
        base.Awake();
    }

    public void SendRequest(Vector3 pos, string hituser, BulletType type)
    {
        MainPack pack = new MainPack();
        PlayerPack playerPack = new PlayerPack();
        BulletHitPack bulletHitPack = new BulletHitPack();

        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;

        bulletHitPack.PosX = pos.x;
        bulletHitPack.PosY = pos.y;
        bulletHitPack.PosZ = pos.z;
        bulletHitPack.Hituser = hituser;
        bulletHitPack.Bullettype = type;

        playerPack.Playername = hituser;

        pack.Bullethitpack = bulletHitPack;
        pack.Playerpack.Add(playerPack);

        base.SendRequest(pack);
    }
}
