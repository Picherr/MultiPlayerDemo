using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class FindRoomRequest : BaseRequest
{
    public RoomListPanel roomListPanel;

    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.FindRoom;
        base.Awake();
    }

    private void Update()
    {
        if (pack != null)
        {
            roomListPanel.FindRoomResponse(pack);
            pack = null;
        }
    }

    public void SendRequest()
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        pack.Str = "r";
        base.SendRequest(pack);
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
