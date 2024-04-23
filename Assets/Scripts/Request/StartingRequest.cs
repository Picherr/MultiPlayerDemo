using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using System.Linq;
using UnityEngine.SceneManagement;

public class StartingRequest : BaseRequest
{
    private MainPack isstart = null;

    public RoomPanel roomPanel;

    private Music musicController;

    public override void Awake()
    {
        actionCode = ActionCode.Starting;
        musicController = GameObject.Find("BGM").GetComponent<Music>();
        base.Awake();
    }

    private void Update()
    {
        if (isstart != null)
        {
            Debug.Log("游戏正式开始！");
            roomPanel.GameStarting(isstart);
            face.addPlayer(isstart);
            musicController.GamePlay();
            isstart = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        isstart = pack;
    }
}
