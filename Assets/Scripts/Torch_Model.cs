using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Torch_Model : NetworkBehaviour
{
    public NetworkVariable<bool> isOn = new NetworkVariable<bool>();

    public Light light;

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
        if (newValue)
            On_ClientView();
        else
            Off_ClientView();
    }

    public void On()
    {
        isOn.Value = true;
    }

    public void Off()
    {
        isOn.Value = false;
    }
    private void On_ClientView()
    {
        light.enabled = true;
    }

    private void Off_ClientView()
    {
        light.enabled = false;
    }
}
