using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class DamageRequest : BaseRequest
{
    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Damage;
        base.Awake();
    }

    private void Update()
    {
        if (pack != null)
        {
            face.UpdategamePanelList(pack);
            face.Damage(pack);
            pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }

    public void SendRequest(Vector3 pos, Vector3 start, Vector3 end, string hituser, BulletType type)
    {
        MainPack pack = new MainPack();
        PosPack posPack = new PosPack();
        PlayerPack playerPack = new PlayerPack();
        BulletHitPack bulletHitPack = new BulletHitPack();

        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;

        bulletHitPack.PosX = pos.x;
        bulletHitPack.PosY = pos.y;
        bulletHitPack.PosZ = pos.z;
        bulletHitPack.Hituser = hituser;
        bulletHitPack.Bullettype = type;

        playerPack.Playername = hituser; // 此时的PlayerPack是被击中的玩家

        posPack.PosX = start.x;
        posPack.PosY = start.y;
        posPack.PosZ = start.z;

        playerPack.Pospack = posPack;
        pack.Bullethitpack = bulletHitPack;
        pack.Playerpack.Add(playerPack);

        base.SendRequest(pack);
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

        playerPack.Playername = hituser; // 此时的PlayerPack是被击中的玩家

        pack.Bullethitpack = bulletHitPack;
        pack.Playerpack.Add(playerPack);

        base.SendRequest(pack);
    }
}
