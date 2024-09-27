using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<int>, IKillable, IAttack<int>
{

    public int Health { get; set; } = 50;

    public int FistAttack { get; set; } = 10;
    public int KickAttack { get; set; } = 15;

    public GameObject objectToDestroy;

    public Animator enemyAnim;

    public int currentHealth;

    // public Player player;

    public LayerMask playerLayers;
    public float attackRange2 = 0.5f;

    public Transform AttackPointEnemy;

    void Start()
    {
        currentHealth = Health;
    }

    void Update()
    {

    }

    public void DamageFist(int damageTaken)
    {
        currentHealth -= damageTaken;

        Invoke("TakeFistAnimation", 0.15f);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void DamageKick(int damageTaken)
    {
        currentHealth -= damageTaken;

        Invoke("TakeKickAnimation", 0.15f);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Debug.Log("Enemy died.");
        // anim.SetBool("enemyDead", true);    ---- Enemy ölme animasyonu
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
        Invoke("DestroyObject", 3f);
    }

    public void EnemyFist()
    {
        enemyAnim.SetTrigger("Fist");
        Collider[] hitPlayer1 = Physics.OverlapSphere(AttackPointEnemy.position, attackRange2, playerLayers);

        foreach (Collider player in hitPlayer1)
        {
            player.GetComponent<Player>().DamageFist(FistAttack);
        }
    }

    public void EnemyKick()
    {
        enemyAnim.SetTrigger("Kick");
        Collider[] hitPlayer2 = Physics.OverlapSphere(AttackPointEnemy.position, attackRange2, playerLayers);

        foreach (Collider player in hitPlayer2)
        {
            player.GetComponent<Player>().DamageKick(KickAttack);
        }
    }

    void DestroyObject()
    {
        Destroy(objectToDestroy);
    }

    public void TakeFistAnimation()
    {
        // RAKİBİN acı çekme sesi VE KARAKTERİMİZİN YUMRUK SESİ (TakeFistAnimation'U VEYA SADECE BU SESİ EVENT İLE YAP)
        enemyAnim.SetTrigger("TakeFistEnemy");
    }

    public void TakeKickAnimation()
    {
        // RAKİBİN acı çekme sesi VE KARAKTERİMİZİN YUMRUK SESİ (TakeKickAnimation'U VEYA SADECE BU SESİ EVENT İLE YAP)

        enemyAnim.SetTrigger("TakeKickEnemy");
    }

}
