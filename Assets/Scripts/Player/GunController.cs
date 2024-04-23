using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static bool AttackAbled = true;
    public static bool UltAbled = true;

    // private bool isFire = false;
    private float fireInterval = 0.5f;
    private float bulletSpeed = 10.0f;

    private GameObject AttackBullet;
    private GameObject UltBullet;
    private Transform AttackFirePos;
    private Transform UltFirePos;
    public FireRequest fireRequest;
    public DamageRequest damageRequest;
    public UltExtraDamageRequest ultExtraDamageRequest;

    private UpPos upPos;
    private AnimationController animationController;
    private CharacterRistic characterRistic;
    private CharacterType type; // 本地角色所选择的属性，决定应该释放哪种技能

    private GamePanel gamePanel;

    private void Start()
    {
        AttackBullet = Resources.Load("Prefabs/AttackBullet") as GameObject;
        UltBullet = Resources.Load("Prefabs/UltBullet") as GameObject;

        AttackFirePos = transform.Find("EyeView/AttackBulletPoint");
        UltFirePos = transform.Find("UltBulletPoint");

        upPos = gameObject.GetComponent<UpPos>();
        animationController = gameObject.GetComponent<AnimationController>();
        characterRistic = gameObject.GetComponent<CharacterRistic>();
        type = characterRistic.charactertype;

        gamePanel = GameObject.Find("GamePanel(Clone)").GetComponent<GamePanel>();
    }

    private void Update()
    {
        if (characterRistic.isLocal)
        {
            FireControl();
        }
    }

    private void FireControl()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            isFire = true;
            StartCoroutine("Fire");
        }
        if (Input.GetMouseButtonUp(0))
        {
            isFire = false;
            StopCoroutine("Fire");
        }*/
        if (Input.GetKeyDown(KeyCode.R) && AttackAbled)
        {
            Fire(AttackBullet, AttackFirePos, type);
            gamePanel.CDBegin("Attack", 2);
        }
        if (Input.GetKeyDown(KeyCode.T) && UltAbled)
        {
            Fire(UltBullet, UltFirePos, type);
            gamePanel.CDBegin("Ult", 5);
        }
    }

    private void Fire(GameObject bullet, Transform bulletpos, CharacterType type)
    {
        // 对于本机用户来说，需要即时生成自身发出的子弹
        if (bulletpos != null && bullet != null)
        {
            GameObject obj;
            Bullet b;
            switch (bullet.name)
            {
                // 普通攻击
                case "AttackBullet":
                    obj = Instantiate(bullet, bulletpos.position, bulletpos.rotation); // 生成子弹
                    obj.transform.Find(type.ToString()).gameObject.SetActive(true); // 将对应属性的子弹特效激活
                    obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * bulletSpeed; // 设定子弹速度

                    // 子弹的角度
                    Vector3 m_Rot = new Vector3();
                    m_Rot.x = transform.Find("EyeView").eulerAngles.x; // x轴角度为EyeView的x轴转向
                    m_Rot.y = transform.eulerAngles.y; // y轴角度为Player的y轴转向
                    m_Rot.z = 0; // z轴无角度

                    b = obj.GetComponent<Bullet>();
                    b.isLocal = true;
                    b.damageRequest = damageRequest;
                    b.start = bulletpos.position;
                    b.bulletType = TypeToBullet(type, "Attack");
                    fireRequest.SendRequest(bulletpos.position, m_Rot, b.bulletType);
                    break;

                case "UltBullet":
                    obj = Instantiate(bullet, bulletpos.position, Quaternion.identity); // 生成子弹
                    obj.transform.Find(type.ToString()).gameObject.SetActive(true); // 将对应属性的子弹特效激活
                    Debug.Log(type.ToString());
                    b = obj.GetComponent<Bullet>();
                    b.isLocal = true;
                    b.damageRequest = damageRequest;
                    b.ultExtraDamageRequest = ultExtraDamageRequest;
                    b.bulletType = TypeToBullet(type, "Ult");
                    b.type = type;
                    fireRequest.SendRequest(bulletpos.position, b.bulletType);
                    break;

                default:
                    break;
            }
            animationController.Attack();
            upPos.State = "Attack";
        }
    }

    private BulletType TypeToBullet(CharacterType type, string bullet)
    {
        if (bullet == "Attack")
        {
            switch (type)
            {
                case CharacterType.Fire:
                    return BulletType.FireAttack;
                case CharacterType.Ice:
                    return BulletType.IceAttack;
                case CharacterType.Poison:
                    return BulletType.PoisonAttack;
                default:
                    return BulletType.BulletNone;
            }
        }
        else
        {
            switch (type)
            {
                case CharacterType.Fire:
                    return BulletType.FireUlt;
                case CharacterType.Ice:
                    return BulletType.IceUlt;
                case CharacterType.Poison:
                    return BulletType.PoisonUlt;
                default:
                    return BulletType.BulletNone;
            }
        }
    }

    /// <summary>
    /// 开火
    /// </summary>
    /// <returns></returns>
    /*IEnumerator Fire()
    {
        while (isFire)
        {
            if (firePos != null && bullet != null)
            {
                // 对于本机用户来说，需要即时生成自身发出的子弹
                GameObject obj = Instantiate(bullet, firePos.position, firePos.rotation); // 生成子弹
                obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * bulletSpeed; // 设定子弹速度

                // 子弹的角度
                Vector3 m_Rot = new Vector3();
                m_Rot.x = transform.Find("EyeView").eulerAngles.x; // x轴角度为EyeView的x轴转向
                m_Rot.y = transform.eulerAngles.y; // y轴角度为Player的y轴转向
                m_Rot.z = 0; // z轴无角度

                Bullet b = obj.GetComponent<Bullet>();
                b.isLocal = true;
                b.damageRequest = damageRequest;
                b.start = firePos.position;
                fireRequest.SendRequest(firePos.position, m_Rot);

                animationController.Attack();
                upPos.State = "Attack";
                Debug.Log("攻击");
            }
            yield return new WaitForSeconds(fireInterval);
        }
    }*/
}
