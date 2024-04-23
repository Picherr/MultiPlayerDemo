using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class FireRequest : BaseRequest
{
    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Fire;
        base.Awake();
    }

    public void SendRequest(Vector3 pos, Vector3 rot, BulletType type)
    {
        MainPack pack = new MainPack();
        BulletPack bulletPack = new BulletPack();
        bulletPack.PosX = pos.x;
        bulletPack.PosY = pos.y;
        bulletPack.PosZ = pos.z;
        bulletPack.RotX = rot.x;
        bulletPack.RotY = rot.y;
        bulletPack.Bullettype = type;

        pack.Bulletpack = bulletPack;

        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;

        base.SendRequest(pack);
    }

    public void SendRequest(Vector3 pos, BulletType type)
    {
        MainPack pack = new MainPack();
        BulletPack bulletPack = new BulletPack();
        bulletPack.PosX = pos.x;
        bulletPack.PosY = pos.y;
        bulletPack.PosZ = pos.z;
        bulletPack.Bullettype = type;

        pack.Bulletpack = bulletPack;

        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;

        base.SendRequest(pack);
    }

    private void Update()
    {
        if (pack != null)
        {
            face.spawnBullet(pack);
            pack = null;
        }
    }
    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
