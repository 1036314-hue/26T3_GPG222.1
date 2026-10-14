using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Torch_Model : NetworkBehaviour
{
    public NetworkVariable<bool> isOn = new NetworkVariable<bool>();

    

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        OnValueChanged(false, isOn.Value);

    }

    private void OnEnable()
    {
        isOn.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        isOn.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(bool previousValue, bool newValue)
    {
        EventManager.lightToggle?.Invoke(newValue);
    }

    public void On()
    {
        isOn.Value = true;
    }

    public void Off()
    {
        isOn.Value = false;
    }
   
}
