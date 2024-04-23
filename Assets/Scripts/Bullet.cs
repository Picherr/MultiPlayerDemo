using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isLocal = false;
    public BulletType bulletType;
    public DamageRequest damageRequest;
    public UltExtraDamageRequest ultExtraDamageRequest;
    public Vector3 start, end;

    public CharacterType type;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isLocal)
        {
            //»÷ÖÐ
            switch (bulletType)
            {
                case BulletType.FireAttack:
                case BulletType.IceAttack:
                case BulletType.PoisonAttack:
                    end = other.transform.position;
                    Debug.Log(other.gameObject.GetComponent<CharacterRistic>().username);
                    damageRequest.SendRequest(transform.position, start, end, 
                        other.gameObject.GetComponent<CharacterRistic>().username, bulletType);
                    Destroy(gameObject);
                    break;

                case BulletType.FireUlt:
                case BulletType.IceUlt:
                case BulletType.PoisonUlt:
                    StartCoroutine("Ult", other.gameObject.GetComponent<CharacterRistic>().username);
                    break;

                default:
                    break;
            }
        }
    }

    IEnumerator Ult(string hituser)
    {
        for(int i = 1; i <= 3; i++)
        {
            damageRequest.SendRequest(transform.position, hituser, bulletType);
            Debug.Log("µÚ" + i + "´Î¹¥»÷");
            
            yield return new WaitForSeconds(1);
        }
        yield break;
    }
}
