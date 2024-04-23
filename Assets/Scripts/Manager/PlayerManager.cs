using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerManager : BaseManager
{
    public PlayerManager(GameFace face) : base(face) { }

    private static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public static void ClearPlayers()
    {
        players.Clear();
    }

    private GameObject player;
    private GameObject AttackBullet;
    private GameObject UltBullet;

    private float bulletSpeed = 10.0f;

    public override void OnInit()
    {
        base.OnInit();
        player = Resources.Load("Prefabs/Player") as GameObject;
        AttackBullet = Resources.Load("Prefabs/AttackBullet") as GameObject;
        UltBullet = Resources.Load("Prefabs/UltBullet") as GameObject;
    }

    public string CurPlayerID
    {
        get;set;
    }

    public void addPlayer(MainPack pack)
    {
        Transform spawnPos;
        GameObject obj;
        foreach(var p in pack.Playerpack)
        {
            Debug.Log("添加角色" + p.Playername);
            spawnPos = GameObject.Find("SpawnPos" + p.SpawnPos.ToString()).transform;
            obj = GameObject.Instantiate(player, spawnPos.position, spawnPos.rotation);
            if (p.Playername.Equals(face.UserName))
            {
                // 创建本地角色
                obj.AddComponent<UpPosRequest>();
                obj.AddComponent<UpPos>();

                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.isLocal = true;
                characterRistic.username = p.Playername;
                characterRistic.charactertype = SwitchType(p.CharAttrType);

                FireRequest fireRequest = obj.AddComponent<FireRequest>();
                DamageRequest damageRequest = obj.AddComponent<DamageRequest>();
                UltExtraDamageRequest ultExtraDamageRequest = obj.AddComponent<UltExtraDamageRequest>();

                GunController gunController = obj.AddComponent<GunController>();
                gunController.fireRequest = fireRequest;
                gunController.damageRequest = damageRequest;
                gunController.ultExtraDamageRequest = ultExtraDamageRequest;

                obj.transform.Find("EyeView/Camera").AddComponent<Camera>();

                face.SkillsImage(characterRistic.charactertype);
            }
            else
            {
                // 创建其他客户端的角色
                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.isLocal = false;
                characterRistic.username = p.Playername;

                obj.AddComponent<RemoteCharacter>();
            }
            players.Add(p.Playername, obj);
            Restart.PanelsToDestroy.Add(obj);
        }
    }

    public void removePlayer(string id)
    {
        if(players.TryGetValue(id, out GameObject obj))
        {
            GameObject.Destroy(obj);
            players.Remove(id);
        }
        else
        {
            Debug.Log("移除角色出错！");
        }
    }

    public void GameExit()
    {
        foreach (var VARIABLE in players.Values)
        {
            GameObject.Destroy(VARIABLE);
        }
        players.Clear();
    } 

    public void UpPos(MainPack pack)
    {
        PosPack posPack = pack.Playerpack[0].Pospack;

        if (players.TryGetValue(pack.Playerpack[0].Playername, out GameObject obj))
        {
            Vector3 Pos = new Vector3(posPack.PosX, posPack.PosY, posPack.PosZ);
            Vector3 Rot = new Vector3(posPack.RotX, posPack.RotY, posPack.RotZ);

            obj.GetComponent<RemoteCharacter>().SetState(Pos, Rot);
            // 其他用户播放动画
            obj.GetComponent<AnimationController>().movespeed = posPack.MoveSpeed;
            if (posPack.State != "")
            {
                obj.GetComponent<AnimationController>().Invoke(posPack.State, 0f);
            }
        }
    }

    public void spawnBullet(MainPack pack)
    {
        Debug.Log("执行PlayerManager的spawnBullet函数");

        Vector3 pos, rot;
        GameObject obj;
        switch (pack.Bulletpack.Bullettype)
        {
            case BulletType.FireAttack:
                pos = new Vector3(pack.Bulletpack.PosX, pack.Bulletpack.PosY, pack.Bulletpack.PosZ);
                rot = new Vector3(pack.Bulletpack.RotX, pack.Bulletpack.RotY, 0);
                obj = GameObject.Instantiate(AttackBullet, pos, Quaternion.Euler(rot));
                obj.transform.Find("Fire").gameObject.SetActive(true);
                obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * bulletSpeed;
                break;
            case BulletType.IceAttack:
                pos = new Vector3(pack.Bulletpack.PosX, pack.Bulletpack.PosY, pack.Bulletpack.PosZ);
                rot = new Vector3(pack.Bulletpack.RotX, pack.Bulletpack.RotY, 0);
                obj = GameObject.Instantiate(AttackBullet, pos, Quaternion.Euler(rot));
                obj.transform.Find("Ice").gameObject.SetActive(true);
                obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * bulletSpeed;
                break;
            case BulletType.PoisonAttack:
                pos = new Vector3(pack.Bulletpack.PosX, pack.Bulletpack.PosY, pack.Bulletpack.PosZ);
                rot = new Vector3(pack.Bulletpack.RotX, pack.Bulletpack.RotY, 0);
                obj = GameObject.Instantiate(AttackBullet, pos, Quaternion.Euler(rot));
                obj.transform.Find("Poison").gameObject.SetActive(true);
                obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * bulletSpeed;
                break;

            case BulletType.FireUlt:
                pos = new Vector3(pack.Bulletpack.PosX, pack.Bulletpack.PosY, pack.Bulletpack.PosZ);
                obj = GameObject.Instantiate(UltBullet, pos, Quaternion.identity);
                obj.transform.Find("Fire").gameObject.SetActive(true);
                break;
            case BulletType.IceUlt:
                pos = new Vector3(pack.Bulletpack.PosX, pack.Bulletpack.PosY, pack.Bulletpack.PosZ);
                obj = GameObject.Instantiate(UltBullet, pos, Quaternion.identity);
                obj.transform.Find("Ice").gameObject.SetActive(true);
                break;
            case BulletType.PoisonUlt:
                pos = new Vector3(pack.Bulletpack.PosX, pack.Bulletpack.PosY, pack.Bulletpack.PosZ);
                obj = GameObject.Instantiate(UltBullet, pos, Quaternion.identity);
                obj.transform.Find("Poison").gameObject.SetActive(true);
                break;
            case BulletType.BulletNone:
                break;
        }
    }

    public void Damage(MainPack pack)
    {
        Debug.Log("执行PlayerManager的Damage函数");
    }

    public CharacterType SwitchType(int charattrtype)
    {
        switch (charattrtype)
        {
            case 1:
                return CharacterType.Fire;
            case 2:
                return CharacterType.Ice;
            case 3:
                return CharacterType.Poison;
            default:
                return CharacterType.None;
        }
    }
}
