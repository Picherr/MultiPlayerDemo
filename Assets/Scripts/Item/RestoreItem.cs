using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreItem : MonoBehaviour
{
    public RestoreType restoreType;
    private RestoreRequest restoreRequest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            restoreRequest = other.gameObject.GetComponent<RestoreRequest>(); // ����ײ����player�ϻ�ȡRestoreRequest
            restoreRequest.SendRequest(other.gameObject.GetComponent<CharacterRistic>().username, restoreType);
            Debug.Log("�ظ�Ѫ��");
        }
    }
}
