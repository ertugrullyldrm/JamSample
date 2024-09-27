using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>
{
    T Health { get; set; }

    void DamageFist(T damageTaken);

    void DamageKick(T damageTaken);

    void TakeFistAnimation();
    void TakeKickAnimation();
}
