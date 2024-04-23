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
            restoreRequest = other.gameObject.GetComponent<RestoreRequest>(); // 从碰撞到的player上获取RestoreRequest
            restoreRequest.SendRequest(other.gameObject.GetComponent<CharacterRistic>().username, restoreType);
            Debug.Log("回复血量");
        }
    }
}
