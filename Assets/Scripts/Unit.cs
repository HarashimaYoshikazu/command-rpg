using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public UnitParamator _paramator;
    [SerializeField]
    Command[] command;
    [SerializeField]
    Unit _opponent;
    [SerializeField]
    Button buttonPrefab;
    [SerializeField]
    Transform _parent;
    private void Start()
    {
        _paramator = new UnitParamator();
        _paramator._attack = 5;
        _paramator._hp = 30;
        for(int i = 0;i< command.Length;i++)
        {
            var button = Instantiate(buttonPrefab, _parent);
            int index = i;
            button.GetComponentInChildren<Text>().text = command[index].Name;
            button.onClick.AddListener(() =>
            {         
                ExecuteCommand(index);
            });
        }
    }

    void ExecuteCommand(int index)
    {
        foreach(var skill in command[index].Skills)
        {
            skill.Action(_paramator,_opponent._paramator);
        }        
    }
}
