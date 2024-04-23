using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class StartGameRequest : BaseRequest
{
    private MainPack pack = null;

    public RoomPanel roomPanel;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.StartGame;
        base.Awake();
    }

    public void SendRequest()
    {
        MainPack pack = new MainPack();
        PlayerPack playerPack = new PlayerPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        pack.Str = "r";
        base.SendRequest(pack);
    }

    private void Update()
    {
        if (pack != null)
        {
            roomPanel.StartGameResponse(pack);
            pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
