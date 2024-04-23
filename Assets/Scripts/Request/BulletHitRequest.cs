using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class BulletHitRequest : BaseRequest
{
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Hit;
        base.Awake();
    }
}
