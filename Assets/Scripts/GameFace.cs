using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using System.Net.Sockets;

public class GameFace : MonoBehaviour
{
    private ClientManager clientManager;
    private RequestManager requestManager;
    private UIManager uiManager;
    private PlayerManager playerManager;

    public string UserName
    {
        set;get;
    }

    private static GameFace face;

    public static GameFace Face
    {
        get
        {
            if (face == null)
            {
                face = GameObject.Find("GameFace").GetComponent<GameFace>();
            }
            return face;
        }
    }

    void Awake()
    {
        uiManager = new UIManager(this);
        clientManager = new ClientManager(this);
        requestManager = new RequestManager(this);
        playerManager = new PlayerManager(this);

        uiManager.OnInit();
        clientManager.OnInit();
        requestManager.OnInit();
        playerManager.OnInit();
    }

    private void OnDestroy()
    {
        clientManager.OnDestroy();
        requestManager.OnDestroy();
        uiManager.OnDestroy();
        playerManager.OnDestroy();
    }

    private float recTime = 0;
    public bool isRec = false;

    private void Update()
    {
        if (isRec)
        {
            Debug.Log("接受消息时间间隔：" + (Time.time - recTime));
            recTime = Time.time;
            isRec = false;
        }
    }

    public void Send(MainPack pack)
    {
        clientManager.Send(pack);
    }

    public void SendTo(MainPack pack)
    {
        pack.User = UserName;
        clientManager.SendTo(pack);
    }

    public void HandleResponse(MainPack pack)
    {
        requestManager.HandleResponse(pack);
    }

    public void AddRequest(BaseRequest request)
    {
        requestManager.AddRequest(request);
    }

    public void RemoveRequest(ActionCode action)
    {
        requestManager.RemoveRequest(action);
    }

    public void ShowMessage(string str, bool sync = false)
    {
        uiManager.ShowMessage(str, sync);
    }

    public void SetSelfID(string id)
    {
        playerManager.CurPlayerID = id;
    }

    public void addPlayer(MainPack packs)
    {
        playerManager.addPlayer(packs);
    }

    public void removePlayer(string id)
    {
        playerManager.removePlayer(id);
    }

    public void GameExit()
    {
        playerManager.GameExit();
        uiManager.PopPanel();
        uiManager.PopPanel();
    }

    public void UpPos(MainPack pack)
    {
        playerManager.UpPos(pack);
    }

    public void spawnBullet(MainPack pack)
    {
        playerManager.spawnBullet(pack);
    }

    public void Damage(MainPack pack)
    {
        playerManager.Damage(pack);
    }

    public void UpdategamePanelList(MainPack pack)
    {
        GamePanel panel = uiManager.GetPanel(PanelType.Game) as GamePanel;
        panel.UpdateList(pack);
    }

    public void SkillsImage(CharacterType type)
    {
        GamePanel panel = uiManager.GetPanel(PanelType.Game) as GamePanel;
        panel.SkillsImage(type);
    }

    public void GameOver(MainPack pack)
    {
        GameOverPanel panel = uiManager.PushPanel(PanelType.GameOver).GetComponent<GameOverPanel>();
        panel.GameOver(pack);
    }
}