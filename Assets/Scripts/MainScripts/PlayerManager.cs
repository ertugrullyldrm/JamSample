using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public int currentHealth;
    public float currentWaterRate;
    public float waterDecreaseRate = 1f; // Su azalma oranı

    public void Kill()
    {
        Debug.Log("Game Over");
        // ölme animasyonu
        Destroy(gameObject); // Bu şart olmayabilir
        // LoadScene-GameOverScene
    }

    
}
