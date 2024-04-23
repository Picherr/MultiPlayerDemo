using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public GameObject item;
    public Transform listTransform;
    public Text timetext;
    public Button exitBtn;

    public Image Attack;
    public Image Ult;
    public GameObject[] Maps;
    public Transform AttackCD;
    public Transform UltCD;

    private float starttime;

    private Dictionary<string, PlayerInfoItem> itemList = new Dictionary<string, PlayerInfoItem>();

    public ExitGameRequest exitGameRequest;
    public GameOverRequest gameOverRequest;

    private void OnEnable()
    {
        starttime = Time.time;
    }

    private void Start()
    {
        exitBtn.onClick.AddListener(OnExitClick);
    }

    private void FixedUpdate()
    {
        timetext.text = Mathf.Clamp((int)(Time.time - starttime), 0, 300).ToString();
        // 时间到达，无人获胜，平局
        if (timetext.text.ToString() == "300")
        {
            gameOverRequest.SendRequest();
        }
    }

    private void OnExitClick()
    {
        exitGameRequest.SendRequest();
        face.GameExit();
    }

    public void UpdateList(MainPack packs)
    {
        for(int i = 0; i < listTransform.childCount; i++)
        {
            Destroy(listTransform.GetChild(i).gameObject);
        }
        itemList.Clear();

        foreach(var p in packs.Playerpack)
        {
            GameObject obj = Instantiate(item, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(listTransform);
            PlayerInfoItem pInfo = obj.GetComponent<PlayerInfoItem>();
            pInfo.Set(p.Playername, p.Hp);
            itemList.Add(p.Playername, pInfo);
        }
    }

    public void LoadMap(MainPack pack)
    {
        MapType map = pack.Roompack[0].Maptype;
        GameObject obj;
        switch (map)
        {
            case MapType.Forest:
                obj = GameObject.Instantiate(Maps[0], Vector3.zero, Quaternion.identity);
                Restart.PanelsToDestroy.Add(obj);
                break;
            case MapType.Mountain:
                obj = GameObject.Instantiate(Maps[1], Vector3.zero, Quaternion.identity);
                Restart.PanelsToDestroy.Add(obj);
                break;
            case MapType.YellowStone:
                obj = GameObject.Instantiate(Maps[2], Vector3.zero, Quaternion.identity);
                Restart.PanelsToDestroy.Add(obj);
                break;
        }
    }

    public void UpdateValue(string id, int v)
    {
        if(itemList.TryGetValue(id,out PlayerInfoItem pInfo))
        {
            pInfo.Up(v);
        }
        else
        {
            Debug.LogWarning("获取不到对应的角色信息");
        }
    }

    public void SkillsImage(CharacterType type)
    {
        string typeStr = type.ToString();
        Sprite sprite;
        sprite = Resources.Load<Sprite>("Skills/" + typeStr + "/Attack") as Sprite;
        Attack.sprite = sprite;
        sprite = Resources.Load<Sprite>("Skills/" + typeStr + "/Ult") as Sprite;
        Ult.sprite = sprite;
    }

    public void CDBegin(string skill, int cd)
    {
        switch (skill)
        {
            case "Attack":
                AttackCD.gameObject.SetActive(true);
                AttackCD.GetComponent<cdTime>().cdBegin(cd);
                GunController.AttackAbled = false;
                break;
            case "Ult":
                UltCD.gameObject.SetActive(true);
                UltCD.GetComponent<cdTime>().cdBegin(cd);
                GunController.UltAbled = false;
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
