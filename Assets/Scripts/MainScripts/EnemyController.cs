using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                // Attack the target
                FaceTarget();
            }
        }

        if (distance < 2f)
        {
            Invoke("IdleAnimation", 0.15f);
        }

        if (distance > 2f)
        {
            Invoke("RunAnimation", 0.15f);
        }

    }

    void IdleAnimation()
    {
        enemy.enemyAnim.SetTrigger("Idle");
    }

    void RunAnimation()
    {
        enemy.enemyAnim.SetTrigger("Run");
    }

    void PunchAnimation()
    {
        enemy.enemyAnim.SetTrigger("Punch");
    }

    void KickAnimation()
    {
        enemy.enemyAnim.SetTrigger("Kick");
    }

    /*

    void Enemy2IdleAnim()
    {
        enemy.anim.SetTrigger("idle");
    }

    void Enemy2RunAnim()
    {
        enemy.anim.SetTrigger("run");
    }

    void Enemy2PunchAnim()
    {
        enemy.anim.SetTrigger("Punch");
    }

    void Enemy2KickAnim()
    {
        enemy.anim.SetTrigger("Kick");
    }

    */

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
