using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Command :ScriptableObject
{
    [SerializeField, Header("名前")]
    string _name;
    public string Name => _name;

    [SerializeReference, SubclassSelector,Header("コマンドを構成するスキル")]
    SkillBase[] _skills;
    public SkillBase[] Skills => _skills; 
}
