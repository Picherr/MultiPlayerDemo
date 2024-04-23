using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Button joinBtn;
    public Text title, num, state, map;

    public RoomListPanel roomListPanel;

    // Start is called before the first frame update
    void Start()
    {
        joinBtn.onClick.AddListener(OnJoinClick);
    }

    private void OnJoinClick()
    {
        roomListPanel.JoinRoom(title.text);
    }

    public void SetRoomInfo(string title, int curnum, int maxnum, int state, MapType maptype)
    {
        this.title.text = title;
        this.num.text = curnum + "/" + maxnum;
        switch (state)
        {
            case 0:
                this.state.text = "�ȴ�����";
                break;
            case 1:
                this.state.text = "��������";
                break;
            case 2:
                this.state.text = "��Ϸ��";
                break;
        }
        switch (maptype)
        {
            case MapType.Forest:
                this.map.text = "Forest";
                break;
            case MapType.Mountain:
                this.map.text = "Mountain";
                break;
            case MapType.YellowStone:
                this.map.text = "YellowStone";
                break;
        }
    }
}
