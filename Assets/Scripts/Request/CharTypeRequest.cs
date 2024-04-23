using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTypeRequest : BaseRequest
{
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CharAttrType;
        base.Awake();
    }

    public void SendRequest(CharacterType type)
    {
        MainPack pack = new MainPack();
        PlayerPack playerPack = new PlayerPack();
        playerPack.Playername = face.UserName;
        playerPack.CharAttrType = SwitchType(type);
        pack.Playerpack.Add(playerPack);

        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        base.SendRequest(pack);
    }

    public int SwitchType(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Fire:
                return 1;
            case CharacterType.Ice:
                return 2;
            case CharacterType.Poison:
                return 3;
            default:
                return 0;
        }
    }
}
