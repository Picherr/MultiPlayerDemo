using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreparePanel : BasePanel
{
    private bool isEnabledFst = false;

    public Button FireBtn;
    public Button IceBtn;
    public Button PoisonBtn;
    public Button BackBtn;

    private RoomPanel roomPanel;
    public CharTypeRequest charTypeRequest;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // ��һ�μ��Ĭ��ѡ�л����԰�ť
        if (!isEnabledFst)
        {
            isEnabledFst = true;
            EventSystem.current.SetSelectedGameObject(FireBtn.gameObject);
        }
        // ֮���ٽ���ʱ��ѡ����ǰѡ��İ�ť
        else
        {
            switch (roomPanel.type)
            {
                case CharacterType.Fire:
                    EventSystem.current.SetSelectedGameObject(FireBtn.gameObject);
                    break;
                case CharacterType.Ice:
                    EventSystem.current.SetSelectedGameObject(IceBtn.gameObject);
                    break;
                case CharacterType.Poison:
                    EventSystem.current.SetSelectedGameObject(PoisonBtn.gameObject);
                    break;
                case CharacterType.None:
                    break;
            }
        }
        
    }

    private void Start()
    {
        FireBtn.onClick.AddListener(OnFireClick);
        IceBtn.onClick.AddListener(OnIceClick);
        PoisonBtn.onClick.AddListener(OnPoisonClick);
        BackBtn.onClick.AddListener(OnBackClick);

        roomPanel = uiMag.GetPanel(PanelType.Room) as RoomPanel;
    }

    private void OnFireClick()
    {
        roomPanel.type = CharacterType.Fire;
    }

    private void OnIceClick()
    {
        roomPanel.type = CharacterType.Ice;
    }

    private void OnPoisonClick()
    {
        roomPanel.type = CharacterType.Poison;
    }

    private void OnBackClick()
    {
        // ���ȷ�Ϻ���ѡ����Ϣ����
        charTypeRequest.SendRequest(roomPanel.type);
        uiMag.PopPanel();
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
