using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttack<int>, IDamageable<int>
{
    public int Health { get; set; }

    public float WaterRate { get; set; } = 100;

    public int FistAttack { get; set; } = 10;
    public int KickAttack { get; set; } = 15;

    public PlayerManager playerManager;
    public Animator playerAnim;

    public HealthBar healthBar;
    public BodyWaterRate waterRateBar;

    void Start()
    {
        playerManager.currentHealth = Health;
        healthBar.SetMaxHealth(Health);
        playerManager.currentWaterRate = WaterRate;
        waterRateBar.SetMaxWaterRate(WaterRate);

        PlayerEventManager.EventPlayerDamageTake += SetHealthBar;
    }

    void Update()
    {
        WaterLoss();

    }

    public void WaterLoss()
    {
        // BURAYA ÖLME SESİ EKLEME KİLL'DE OLACAK ZATEN
        playerManager.currentWaterRate -= playerManager.waterDecreaseRate * Time.deltaTime; // Sürekli olarak su kaybı yaşıyor.
        if (playerManager.currentWaterRate <= 0f)
        {
            playerManager.Kill();
        }
    }

    public void DamageFist(int damageTaken)
    {
        playerManager.currentHealth -= damageTaken;
        SetHealthBar();

        Invoke("TakeFistAnimation", 0.15f);

        if (playerManager.currentHealth <= 0)
        {
            playerManager.Kill();
        }
    }

    public void DamageKick(int damageTaken)
    {
        playerManager.currentHealth -= damageTaken;
        SetHealthBar();

        Invoke("TakeKickAnimation", 0.15f);

        if (playerManager.currentHealth <= 0)
        {
            playerManager.Kill();
        }
    }

    void OnEnable()
    {
        PlayerEventManager.EventPlayerDamageTake += SetHealthBar;
    }

    void OnDisable()
    {
        PlayerEventManager.EventPlayerDamageTake -= SetHealthBar;
    }

    public void SetHealthBar()
    {
        healthBar.SetHealth(playerManager.currentHealth); // Bu satır hata vermeye devam ederse bu event'ı sil ve eventsız şekilde yaz
                                            // DamageFist ve DamageKick içine "currentHealth -= damageTaken" satırından sonra
                                            // " healthBar.SetHealth(currentHealth); " bunu yaz
    }

    public void TakeFistAnimation()
    {
        // KARAKTERİN acı çekme sesi VE RAKİBİN YUMRUK SESİ (TakeFistAnimation'U VEYA SADECE BU SESİ EVENT İLE YAP)
        playerAnim.SetTrigger("TakeFistPlayer");
    }

    public void TakeKickAnimation()
    {
        // KARAKTERİN acı çekme sesi VE RAKİBİN YUMRUK SESİ (TakeKickAnimation'U VEYA SADECE BU SESİ EVENT İLE YAP)
        playerAnim.SetTrigger("TakeKickPlayer");
    }
}
