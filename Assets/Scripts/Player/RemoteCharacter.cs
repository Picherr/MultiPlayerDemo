using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteCharacter : MonoBehaviour
{
    private Transform selfTransform;

    private Vector3 selfPos;
    private Quaternion selfAngle;

    public void SetState(Vector3 selfpos, Vector3 selfangle)
    {
        selfPos = selfpos;
        selfAngle = Quaternion.Euler(selfangle.x, selfangle.y, selfangle.z);
    }

    private void Start()
    {
        selfTransform = transform;
        selfAngle = selfTransform.rotation;
        selfPos = selfTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (selfTransform == null || selfPos == null) return;
        selfTransform.position = Vector3.Lerp(selfTransform.position, selfPos, 0.25f);
        selfTransform.rotation = Quaternion.Slerp(selfTransform.rotation, selfAngle, 0.25f);
    }
}
