using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Dictionary<Type, IEntityInitComponent> _compoDict;

    protected virtual void Awake()
    {
        _compoDict = new Dictionary<Type, IEntityInitComponent>();

        AddCompoDictionary();
        ComponentInit();
        ComponentAfterInit();
    }

    private void AddCompoDictionary()
    {
        GetComponentsInChildren<IEntityInitComponent>(true).ToList()
            .ForEach(component => _compoDict.Add(component.GetType(), component));
    }

    public void ComponentInit()
    {
        _compoDict.Values.ToList()
            .ForEach(component => component.Initialize(this));
    }

    public void ComponentAfterInit()
    {
        _compoDict.Values.OfType<IEntityAfterInitCompo>().ToList()
            .ForEach(component => component.AfterInit());
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnDestroy()
    {
        GetComponentsInChildren<IEntityDisposeComponent>().ToList()
            .ForEach(component => component.Dispose());
    }

    public T GetCompo<T>(bool isDerived = false) where T : class, IEntityInitComponent
    {
        if (_compoDict.TryGetValue(typeof(T), out IEntityInitComponent compo))
        {
            return compo as T;
        }

        if (!isDerived) return default;

        Type findType = _compoDict.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
        if (findType != null)
            return _compoDict[findType] as T;

        return default;
    }
}
