using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : SkillBase
{
    [SerializeField, Header("ƒ_ƒ[ƒW”{—¦"), Range(0, 1000)]
    int _magnification = 100;
    [SerializeField, Header("UŒ‚‰ñ”"), Range(1, 10)]
    int _attackCount = 1;

    protected override void Execute(UnitParamator source, UnitParamator target)
    {
        for(int i = 0;i< _attackCount;i++)
        {
            target.Damage(source._attack * _magnification / 100);
        }       
    }
}
