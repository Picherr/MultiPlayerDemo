using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    public float movespeed
    {
        set;get;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetParam();
    }

    public void SetParam()
    {
        animator.SetFloat("MoveSpeed", movespeed);
    }

    public void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Damaged()
    {
        if (animator != null)
        {
            animator.SetTrigger("Damaged");
        }
    }
}
