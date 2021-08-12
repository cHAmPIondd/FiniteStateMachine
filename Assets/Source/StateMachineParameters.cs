using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineParameters
{
    private Dictionary<string, object> parameterValues = new Dictionary<string, object>();
    public void AddParameter(string _key, object _value)
    {
        parameterValues.Add(_key, _value);
    }
    public void RemoveParameter(string _key)
    {
        parameterValues.Remove(_key);
    }
    public T GetValue<T>(string _key)
    {
        object value;
        var containKey = parameterValues.TryGetValue(_key, out value);
        if (containKey)
        {
            if (value is T)
                return (T)value;
            else
                Debug.LogWarning(string.Format("Parameter type '{0}' does not match.", _key));
        }
        else
        {
            Debug.LogWarning(string.Format("Parameter '{0}' does not exist.", _key));
        }
        return default(T);
    }
    public void SetValue<T>(string _key, T _value)
    {
        if (!parameterValues.ContainsKey(_key))
        {
            Debug.LogWarning(string.Format("Parameter '{0}' does not exist.", _key));
        }
        else
        {
            parameterValues[_key] = _value;
        }
    }
}
