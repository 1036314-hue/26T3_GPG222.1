using Unity.Netcode;
using UnityEngine;

public class LightToggler : NetworkBehaviour
{
    public Light light;

    [Rpc(target:SendTo.ClientsAndHost)]
    public void Toggle_Rpc()
    {
        light.enabled = !light.enabled;
    }
}
