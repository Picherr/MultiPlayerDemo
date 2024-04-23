using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketGameProtocol;
using UnityEngine.SceneManagement;

public class RoomPanel :BasePanel
{
    public Button backBtn, sendBtn, startBtn, prepareBtn;
    public InputField inputtext;
    public Scrollbar scrollbar;

    public Text chattext;
    public Transform content;

    public GameObject UserItemObj;

    public CharacterType type;

    public ExitRoomRequest exitRoomRequest;
    public ChatRequest chatRequest;
    public StartGameRequest startGameRequest;

    private void Start()
    {
        backBtn.onClick.AddListener(OnBackClick);
        sendBtn.onClick.AddListener(OnSendClick);
        startBtn.onClick.AddListener(OnStartClick);
        prepareBtn.onClick.AddListener(OnPrepareClick);
    }

    private void OnBackClick()
    {
        exitRoomRequest.SendRequest();
        chattext.text = "";
    }

    private void OnSendClick()
    {
        if (inputtext.text == "")
        {
            uiMag.ShowMessage("�������ݲ���Ϊ�գ�");
            return;
        }
        chatRequest.SendRequest(inputtext.text);
        chattext.text += "�ң�" + inputtext.text + "\n";
        inputtext.text = "";
    }

    private void OnStartClick()
    {
        startGameRequest.SendRequest();
        chattext.text = "";
    }

    private void OnPrepareClick()
    {
        uiMag.PushPanel(PanelType.Prepare);
    }

    public void GameStarting(MainPack packs)
    {
        GamePanel gamePanel = uiMag.PushPanel(PanelType.Game).GetComponent<GamePanel>();
        gamePanel.UpdateList(packs);
        gamePanel.LoadMap(packs);
    }

    /// <summary>
    /// ˢ������б�
    /// </summary>
    /// <param name="pack"></param>
    public void UpdatePlayerList(MainPack pack)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        foreach (PlayerPack player in pack.Playerpack)
        {
            UserItem userItem = Instantiate(UserItemObj, Vector3.zero, Quaternion.identity).GetComponent<UserItem>();
            userItem.gameObject.transform.SetParent(content);
            userItem.SetPlayerInfo(player.Playername);
        }
    }

    public void ExitRoomResponse()
    {
        uiMag.PopPanel();
    }

    public void ChatResponse(string str)
    {
        chattext.text += str + "\n";
    }

    public void StartGameResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Fail:
                uiMag.ShowMessage("��ʼ��Ϸʧ�ܣ������Ƿ�����");
                break;
            case ReturnCode.Succeed:
                uiMag.ShowMessage("��Ϸ��������");
                break;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Enter();
    }

    public override void OnPause()
    {
        base.OnPause();
        Exit();
    }

    public override void OnRecovery()
    {
        base.OnRecovery();
        Enter();
    }

    public override void OnExit()
    {
        base.OnExit();
        Exit();
    }

    private void Enter()
    {
        gameObject.SetActive(true);
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }
}
