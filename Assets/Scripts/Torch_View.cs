using Unity.Netcode;
using UnityEngine;

public class Torch_View : NetworkBehaviour
{
    private Light thisLight;
    private Torch_Model torch_Model;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        thisLight = GetComponent<Light>();
        torch_Model = GetComponent<Torch_Model>();
        torch_Model.LightTorch += ToggleLight;
        //EventManager.lightToggle.AddListener(ToggleLight);

    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        //EventManager.lightToggle.RemoveListener(ToggleLight);
    }
    public void ToggleLight(bool isOn)
    {
        thisLight.enabled = isOn;
        EventManager.lightToggle.RemoveListener(ToggleLight);
    }
}
