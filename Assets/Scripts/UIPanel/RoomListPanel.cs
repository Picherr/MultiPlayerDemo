using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketGameProtocol;

public class RoomListPanel : BasePanel
{
    public Button backBtn, findBtn, createBtn;
    public InputField roomname;
    public InputField players;
    public Slider num;
    public Dropdown map;

    public Transform roomListTransform;
    public GameObject roomitem;

    public CreateRoomRequest createRoomRequest;
    public FindRoomRequest findRoomRequest;
    public JoinRoomRequest joinRoomRequest;
    public LogoutRequest logoutRequest;

    private void Start()
    {
        backBtn.onClick.AddListener(OnBackClick);
        findBtn.onClick.AddListener(OnFindClick);
        createBtn.onClick.AddListener(OnCreateClick);
    }

    private void Update()
    {
        players.text = num.value.ToString();
    }

    /// <summary>
    /// 注销登录
    /// </summary>
    private void OnBackClick()
    {
        logoutRequest.SendRequest(face.UserName);
        uiMag.PopPanel();
    }

    private void OnFindClick()
    {
        findRoomRequest.SendRequest();
    }

    private void OnCreateClick()
    {
        if (roomname.text == "")
        {
            uiMag.ShowMessage("房间名不能为空！");
            return;
        }
        int mapIndex = map.value;
        MapType mapType = IndexToMaptype(mapIndex);
        createRoomRequest.SendRequest(roomname.text, (int)num.value, mapType);
    }

    public void FindRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Succeed:
                uiMag.ShowMessage("查询成功！一共有" + pack.Roompack.Count + "个房间");
                break;
            case ReturnCode.Fail:
                uiMag.ShowMessage("查询出错！");
                break;
            case ReturnCode.NotRoom:
                uiMag.ShowMessage("当前没有房间！");
                break;
            default:
                uiMag.ShowMessage("房间不存在！");
                break;
        }
        UpdateRoomList(pack);
    }

    public void CreateRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Succeed:
                uiMag.ShowMessage("创建成功！");
                RoomPanel roomPanel = uiMag.PushPanel(PanelType.Room).GetComponent<RoomPanel>();
                roomPanel.UpdatePlayerList(pack);
                break;
            case ReturnCode.Fail:
                uiMag.ShowMessage("创建失败！");
                break;
            default:
                Debug.Log("def");
                break;
        }
    }

    private void UpdateRoomList(MainPack pack)
    {
        // 清空房间列表
        for (int i = 0; i < roomListTransform.childCount; i++)
        {
            Destroy(roomListTransform.GetChild(i).gameObject);
        }

        foreach (RoomPack room in pack.Roompack)
        {
            RoomItem item = Instantiate(roomitem, Vector3.zero, Quaternion.identity).GetComponent<RoomItem>();
            item.roomListPanel = this;
            item.gameObject.transform.SetParent(roomListTransform);
            item.SetRoomInfo(room.Roomname, room.Curnum, room.Maxnum, room.State, room.Maptype);
        }
    }

    public void JoinRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Succeed:
                uiMag.ShowMessage("加入房间成功！");
                RoomPanel roomPanel = uiMag.PushPanel(PanelType.Room).GetComponent<RoomPanel>();
                roomPanel.UpdatePlayerList(pack);
                break;
            case ReturnCode.Fail:
                uiMag.ShowMessage("加入房间失败！");
                break;
            default:
                Debug.Log("def");
                break;
        }
    }

    private MapType IndexToMaptype(int index)
    {
        switch (index)
        {
            case 0:
                return MapType.Forest;
            case 1:
                return MapType.Mountain;
            case 2:
                return MapType.YellowStone;
            default:
                return MapType.MapNone;
        }
    }

    public void JoinRoom(string roomname)
    {
        joinRoomRequest.SendRequest(roomname);
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
