using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager
{
    public UIManager(GameFace face) : base(face) { }

    private Dictionary<PanelType, BasePanel> panelDict = new Dictionary<PanelType, BasePanel>();
    private Dictionary<PanelType, string> panelPath = new Dictionary<PanelType, string>();

    private Stack<BasePanel> panelStack = new Stack<BasePanel>();

    private Transform canvasTransform;
    private MessagePanel messagePanel;

    public override void OnInit()
    {
        base.OnInit();
        InitPanel();
        canvasTransform = GameObject.Find("Canvas").transform;
        PushPanel(PanelType.Message);
        PushPanel(PanelType.Start);
    }

    /// <summary>
    /// 把UI显示在界面上
    /// </summary>
    /// <param name="panelType"></param>
    public BasePanel PushPanel(PanelType panelType)
    {
        Debug.Log("push");
        if(panelDict.TryGetValue(panelType,out BasePanel panel))
        {
            if(panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            panelStack.Push(panel);
            panel.OnEnter();
            Debug.Log("调用");
            return panel;
        }
        else
        {
            BasePanel panel1 = SpawnPanel(panelType);
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            panelStack.Push(panel1);
            panel1.OnEnter();
            Debug.Log("生成");
            return panel1;
        }
    }

    /// <summary>
    /// 关闭当前UI
    /// </summary>
    public void PopPanel()
    {
        if (panelStack.Count == 0) return;

        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();
        Debug.Log("弹出" + topPanel.name);

        BasePanel panel = panelStack.Peek();
        panel.OnRecovery();
        Debug.Log("调出" + panel.name);
    }

    /// <summary>
    /// 实例化对应的UI
    /// </summary>
    /// <param name="panelType"></param>
    public BasePanel SpawnPanel(PanelType panelType)
    {
        if(panelPath.TryGetValue(panelType,out string path))
        {
            GameObject obj = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            obj.transform.SetParent(canvasTransform, false);
            BasePanel panel = obj.GetComponent<BasePanel>();
            panel.SetUIMag = this;
            panelDict.Add(panelType, panel);
            return panel;
        }
        else
        {
            Debug.Log("空");
            return null;
        }
    }

    /// <summary>
    /// 初始化UI路径
    /// </summary>
    private void InitPanel()
    {
        string panelpath = "Panel/";
        string[] path = new string[] { "MessagePanel", "StartPanel", "LoginPanel", "LogonPanel", "RoomListPanel", "RoomPanel", "GamePanel", "PreparePanel", "GameOverPanel" };
        panelPath.Add(PanelType.Message, panelpath + path[0]);
        panelPath.Add(PanelType.Start, panelpath + path[1]);
        panelPath.Add(PanelType.Login, panelpath + path[2]);
        panelPath.Add(PanelType.Logon, panelpath + path[3]);
        panelPath.Add(PanelType.RoomList, panelpath + path[4]);
        panelPath.Add(PanelType.Room, panelpath + path[5]);
        panelPath.Add(PanelType.Game, panelpath + path[6]);
        panelPath.Add(PanelType.Prepare, panelpath + path[7]);
        panelPath.Add(PanelType.GameOver, panelpath + path[8]);
    }

    public void SetMessagePanel(MessagePanel message)
    {
        messagePanel = message;
    }

    public void ShowMessage(string str, bool sync = false)
    {
        messagePanel.ShowMessage(str, sync);
    }

    public BasePanel GetPanel(PanelType type)
    {
        return panelDict[type];
    }
}
