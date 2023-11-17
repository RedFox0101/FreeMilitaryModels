using System;
using System.Collections.Generic;

public class StateMachine <T> where T : State
{
    private Dictionary<Type, T> _statesDictionary;
    private T _state;

    public T State => _state;

    public void SetInfo(Dictionary<Type, T> statesDictionary)
    {
        _statesDictionary = statesDictionary;
    }

    public void ChangeState(Type key)
    {
        if(!_statesDictionary.ContainsKey(key))
        {
            UnityEngine.Debug.Log($"This Key \"{key}\" is false");
            return;
        }

        _state?.StopState();
        _state = _statesDictionary[key];
        _state?.StartState();
    }
}
