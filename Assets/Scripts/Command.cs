using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Command :ScriptableObject
{
    [SerializeField, Header("���O")]
    string _name;
    public string Name => _name;

    [SerializeReference, SubclassSelector,Header("�R�}���h���\������X�L��")]
    SkillBase[] _skills;
    public SkillBase[] Skills => _skills; 
}
