using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Torch_Model : NetworkBehaviour
{
    public NetworkVariable<bool> isOn = new NetworkVariable<bool>();
    private Torch_View torchView;
    public event Action <bool> LightTorch;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        torchView = GetComponent<Torch_View>();
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
        //EventManager.lightToggle.AddListener(torchView.ToggleLight);
        //EventManager.lightToggle?.Invoke(newValue);
        LightTorch?.Invoke(newValue);
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
