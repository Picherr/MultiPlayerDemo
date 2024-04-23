using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RotSpeed = 180.0f;
    public float moveSpeed = 5.0f;

    [Range(1, 2)]
    public float RotRatio = 1.0f;
    public float fireInterval = 0.5f;

    private Transform playerTR;
    private Transform eyeviewTR;
    private Transform GroundCheckPoint;

    private float x_RotOffset; // x旋转的偏移
    public float x_Limit = 30;

    public float checksphereradius = 0.5f;
    public float gravity = -3.0f;
    public float verticalVelocity = 0;
    private bool isGround = false;
    private float maxHeight = 1.5f;
    private LayerMask layermask;

    private UpPos upPos;
    private CharacterController characterController;
    private AnimationController animationController;
    private CharacterRistic characterRistic;

    private void Start()
    {
        playerTR = transform;
        eyeviewTR = transform.Find("EyeView");
        GroundCheckPoint = transform.Find("GroundCheckPoint");
        layermask = LayerMask.GetMask("Ground");

        upPos = gameObject.GetComponent<UpPos>();
        characterController = gameObject.GetComponent<CharacterController>();
        animationController = gameObject.GetComponent<AnimationController>();
        characterRistic = gameObject.GetComponent<CharacterRistic>();
    }

    private void Update()
    {
        if (characterRistic.isLocal)
        {
            RotControl();
            Move();
        }
    }

    /// <summary>
    /// 控制视角的旋转
    /// </summary>
    private void RotControl()
    {
        if (playerTR == null || eyeviewTR == null) return;

        float offset_x = Input.GetAxis("Mouse X"); // X的偏移量控制Player水平方向的旋转
        float offset_y = Input.GetAxis("Mouse Y"); // y的偏移量控制EyeView垂直方向的旋转

        playerTR.Rotate(Vector3.up * offset_x * RotSpeed * RotRatio * Time.deltaTime);
        x_RotOffset -= offset_y * RotSpeed * RotRatio * Time.deltaTime;
        x_RotOffset = Mathf.Clamp(x_RotOffset, -x_Limit, x_Limit);

        Quaternion curLocalRot = Quaternion.Euler(new Vector3(x_RotOffset, eyeviewTR.localEulerAngles.y, eyeviewTR.localEulerAngles.z));
        eyeviewTR.localRotation = curLocalRot;
    }

    /// <summary>
    /// 控制角色的移动和跳跃
    /// </summary>
    private void Move()
    {
        if (characterController == null) return;

        Vector3 motionValue = Vector3.zero;

        // 获取键盘输入
        float h_value = Input.GetAxis("Horizontal"); // 左右移动
        float v_value = Input.GetAxis("Vertical"); // 前后移动

        motionValue += this.transform.forward * moveSpeed * v_value * Time.deltaTime; // 前后方向的位移
        motionValue += this.transform.right * moveSpeed * h_value * Time.deltaTime; // 左右方向的位移

        // 跳跃
        verticalVelocity += gravity * Time.fixedDeltaTime;

        motionValue += Vector3.up * verticalVelocity * Time.fixedDeltaTime;

        if (GroundCheckPoint != null)
        {
            isGround = Physics.CheckSphere(GroundCheckPoint.position, checksphereradius, layermask);
            if (isGround && verticalVelocity < 0)
            {
                isGround = true;
                verticalVelocity = 0;
            }
        }

        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = -Mathf.Sqrt(maxHeight * 2 / -gravity) * gravity;
            }
        }

        characterController.Move(motionValue);

        if (animationController != null)
        {
            animationController.movespeed = moveSpeed * v_value;
            upPos.moveSpeed = moveSpeed * v_value;
        }
    }
}
