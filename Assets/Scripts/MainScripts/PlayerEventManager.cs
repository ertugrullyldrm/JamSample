using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour
{
    public delegate void DelegatePlayerDamageTake();
    public static event DelegatePlayerDamageTake EventPlayerDamageTake;


    void Update()
    {
        if (EventPlayerDamageTake != null)
        {
            EventPlayerDamageTake();
        }
    }
}
