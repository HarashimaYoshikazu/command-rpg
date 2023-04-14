using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParamator
{
    public int _hp;
    public int _attack;
    public void Damage(int damage)
    {
        _hp -= damage;
        Debug.Log($"åªç›ÇÃHP{_hp}");
    }
}
