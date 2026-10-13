using Unity.Netcode;
using UnityEngine;

public class LightToggler : NetworkBehaviour
{
    public Light light;

    [Rpc(target:SendTo.ClientsAndHost)]
    public void Toggle_Rpc(bool state)
    {
        light.enabled = state;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log("On Network Spawn");

        if (IsServer)
        {
            Toggle_Rpc(light.enabled);
        }
    }
}
