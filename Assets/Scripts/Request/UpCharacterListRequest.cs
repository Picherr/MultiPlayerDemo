using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCharacterListRequest : BaseRequest
{
    private MainPack pack = null;

    public GamePanel gamePanel;
    
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.UpCharacterList;
        base.Awake();
    }


    private void Update()
    {
        if (pack != null)
        {
            gamePanel.UpdateList(pack);
            face.removePlayer(pack.Str);
            pack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
