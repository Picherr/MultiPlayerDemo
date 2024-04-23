using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class PlayersRequest : BaseRequest
{
    private MainPack pack = null;

    public RoomPanel roomPanel;

    public override void Awake()
    {
        //requestCode = RequestCode.Room; // ����Ҫ����ֻ��Ҫ����
        actionCode = ActionCode.PlayerList;
        base.Awake();
    }

    private void Update()
    {
        if (pack != null)
        {
            roomPanel.UpdatePlayerList(pack);
            pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}