using System;
using System.Collections.Generic;

using UnityEngine;

using Scripts;
using Scripts.Enums;
using Scripts.Gates;
using Scripts.Utilities;
using Scripts.Gates.Prefabs;

using Sugar.Interpreter;

public sealed class CircuitManager : SingletonPattern<CircuitManager>
{
    [SerializeField] private List<Gate> prefabs;
    
    [SerializeField] private PrefabGate prefab;
    [SerializeField] private ProgrammableGate programmable;
    
    [SerializeField] private Transform instanceParent;
    [SerializeField] private EditorUtilities utilities;
    
    void Start()
    {
        Interpreter.Initialise();
    }

    public void CreatePrefab(int code)
    {
        var type = (GateType)code;
        foreach (var builtIn in prefabs)
        {
            if (builtIn.Type == type)
                Instantiate(builtIn, instanceParent);   
        }
    }
    
    public void CreateCustom(PrefabData data)
    {
        var instance = Instantiate(prefab, instanceParent);
        instance.Initialise(data);
    }
    
    public void CreateProgrammable(int inCount, int outCount, string code, string name)
    {
        Instantiate(programmable, instanceParent).GetComponent<ProgrammableGate>().Initialise(inCount, outCount, code, name);
    }

    public void TogglePrefabPrompt() => utilities.TogglePrefabPrompt();
    public void ToggleProgrammablePrompt() => utilities.ToggleProgrammablePrompt();
    
    public void ToggleDeletePrompt(bool toggle) => utilities.ToggleDeletePrompt(toggle);
    public void ToggleErrorPrompt(string message, int inCount, int outCount, string source)
    {
        utilities.ToggleErrorPrompt(message);
        
        utilities.ToggleProgrammablePrompt();
        utilities.InitialiseProgrammablePrompt(inCount, outCount, source);
    } 
}
