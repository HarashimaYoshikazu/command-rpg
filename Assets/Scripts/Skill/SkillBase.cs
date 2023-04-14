using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SkillBase
{
    [SerializeField, Header("コマンドの処理先")]
    protected TargetType _targetType;

    [SerializeField, Header("演出ID")]
    protected string _animationID;
    public void Action(UnitParamator sourceUnit, UnitParamator enemyUnit)
    {
        switch (_targetType)
        {
            case TargetType.None:
                break;
            case TargetType.Opponent:
                Execute(sourceUnit, enemyUnit);
                break;
            case TargetType.Myself:
                Execute(sourceUnit, sourceUnit);
                break;
        }
    }

    protected abstract void Execute(UnitParamator source, UnitParamator target);
}
