using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    public float attackSpeed = 1f;
    public float attackDelay =.6f;
    private  float attackCooldown = 0f;

    // public event System.Action OnAttack;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = .8f;
    public LayerMask enemyLayers;

    void Start ()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update ()
    {
        attackCooldown -= Time.deltaTime;
    }
    
    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            if (targetStats != null)
            {
                StartCoroutine(DoDamage(targetStats, attackDelay));
            }
            
            // Play attack animation
            animator.SetTrigger("Attack");
            
            attackCooldown = 1f / attackSpeed;
        }        
    }

    public Collider[] EnemiesInRange()
    {
        // Detect enemies within range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        return hitEnemies;
    }

    IEnumerator DoDamage ( CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
