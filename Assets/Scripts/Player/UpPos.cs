using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpPos : MonoBehaviour
{
    public float moveSpeed;
    public string State;

    private UpPosRequest upPosRequest;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0f;
        State = "";
        upPosRequest = GetComponent<UpPosRequest>();
        InvokeRepeating("UpPosFun", 1, 1f / 10f);
    }

    private void UpPosFun()
    {
        Vector3 Pos = transform.position;
        Vector3 Rot = transform.eulerAngles;
        upPosRequest.SendRequest(Pos, Rot, moveSpeed, State);
        State = "";
    }
}
