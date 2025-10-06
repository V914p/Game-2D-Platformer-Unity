using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

    private void Awake()
    {
        // Tìm tất cả CoreComponent trong children và thêm vào danh sách
        var components = GetComponentsInChildren<CoreComponent>();
        foreach (var component in components)
        {
            AddComponent(component);
        }
    }

    public void LogicUpdate()
    {
        foreach (CoreComponent component in CoreComponents)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponents.Contains(component))
        {
            CoreComponents.Add(component);
            Debug.Log($"{component.GetType().Name} added to CoreComponents of {gameObject.name}");
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponents.OfType<T>().FirstOrDefault();

        if (comp == null)
        {
            comp = GetComponentInChildren<T>();
            if (comp != null)
            {
                AddComponent(comp); // Thêm vào danh sách để lần sau không cần tìm lại
                Debug.Log($"{typeof(T).Name} found in children and added to CoreComponents of {gameObject.name}");
            }
            else
            {
                Debug.LogWarning($"{typeof(T).Name} not found on {gameObject.name}");
            }
        }
        return comp;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}