using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        animator.SetTrigger("Hit");
        Health--;
        isHit = true;
        animator.SetBool("InCombat", true);

        if (Health < 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
            Object.Destroy(this.gameObject, 15f);
        }
    }
}
