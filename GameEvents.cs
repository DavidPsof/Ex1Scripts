using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> OnDoorStateChange;

    public void DoorChangeState(int instanceID)
    {
        if (OnDoorStateChange != null)
        {
            OnDoorStateChange(instanceID);
        }
    }
}