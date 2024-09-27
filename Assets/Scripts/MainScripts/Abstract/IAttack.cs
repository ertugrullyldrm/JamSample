using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack<T>
{
    T FistAttack { get; set; }
    T KickAttack { get; set; }
}
