using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button startBtn;
    public Text text;

    private void Start()
    {
        startBtn.onClick.AddListener(StartButtonClick);
        text.text = ClientManager.nativeip;
    }

    private void StartButtonClick()
    {
        uiMag.PushPanel(PanelType.Login);
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
