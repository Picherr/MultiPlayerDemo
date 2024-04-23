using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class CreateRoomRequest : BaseRequest
{
    public RoomListPanel roomListPanel;

    private MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        base.Awake();
    }

    private void Update()
    {
        if (pack != null)
        {
            roomListPanel.CreateRoomResponse(pack);
            pack = null;
        }
    }

    public void SendRequest(string roomname, int maxnum, MapType map)
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        RoomPack room = new RoomPack();
        room.Roomname = roomname;
        room.Maxnum = maxnum;
        room.Maptype = map;
        pack.Roompack.Add(room);

        base.SendRequest(pack);
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
