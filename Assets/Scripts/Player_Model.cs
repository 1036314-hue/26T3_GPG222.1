using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Model : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Update()
    {
        // Local only. Not networked
        if (IsLocalPlayer)
        {
            // New inputsystem is better
            if (InputSystem.GetDevice<Keyboard>().spaceKey.wasPressedThisFrame)
            {
                GetBigOrDieTrying_RequestToServer_Rpc();
            }
        }
    }


    // Function that ONLY runs on the server. Typically for client controller code when they press buttons etc
    [Rpc(target:SendTo.Server, InvokePermission = RpcInvokePermission.Owner, Delivery = RpcDelivery.Reliable)]
    private void GetBigOrDieTrying_RequestToServer_Rpc()
    {
        // This is running on the server. Check if it's legal/not cheating
        GetBigOrDieTrying_ServerToClients_Rpc();
    }


    // Function that runs from the Server TO ALL clients
    [Rpc(target:SendTo.ClientsAndHost, InvokePermission = RpcInvokePermission.Everyone, Delivery = RpcDelivery.Reliable)]
    private void GetBigOrDieTrying_ServerToClients_Rpc()
    {
        // This is bugged
        transform.localScale = transform.localScale + new Vector3(1.25f, 1.25f, 1.25f);
    }

}
