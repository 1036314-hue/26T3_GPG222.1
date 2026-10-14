using Unity.Netcode;
using UnityEngine;

public class Torch_View : NetworkBehaviour
{
    private Light thisLight;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        thisLight = GetComponent<Light>();
        EventManager.lightToggle.AddListener(ToggleLight);
        
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        EventManager.lightToggle.RemoveListener(ToggleLight);
    }
    private void ToggleLight(bool isOn)
    {
        thisLight.enabled = isOn;
    }
}
