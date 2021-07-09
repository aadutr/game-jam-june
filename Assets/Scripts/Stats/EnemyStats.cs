using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Animator animator;
    
    public override void Die()
    {
        base.Die();

        // Death animation
        animator.SetTrigger("Die");
        animator.SetBool("hasDied", true);

        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject ()
    {
        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }
    
}
