using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text wintext;
    public Button exitBtn;

    private Music musicController;

    private void Start()
    {
        exitBtn.onClick.AddListener(OnExitClick);
        musicController = GameObject.Find("BGM").GetComponent<Music>();
    }

    private void OnExitClick()
    {
        uiMag.PopPanel();
        uiMag.PopPanel();
        uiMag.PopPanel();

        GamePanel gamePanel = uiMag.GetPanel(PanelType.Game) as GamePanel;
        gamePanel.AttackCD.gameObject.SetActive(false);
        gamePanel.UltCD.gameObject.SetActive(false);

        Restart.DestroyPanels();

        musicController.PrePlay();
    }

    public void GameOver(MainPack pack)
    {
        wintext.text = pack.Str + "WIN£¡";
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
