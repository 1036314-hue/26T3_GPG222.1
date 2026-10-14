using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public static UnityEvent<bool> lightToggle = new UnityEvent<bool>();
}
