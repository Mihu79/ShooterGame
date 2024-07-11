using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerRecoil()
    {
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }
    }
}
