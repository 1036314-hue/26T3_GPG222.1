using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Control : NetworkBehaviour
{
    [SerializeField]
    private Player_Model _model;
    private PlayerAction _action;

    public override void OnNetworkSpawn()
    {
        if (!IsLocalPlayer) return;

        base.OnNetworkSpawn();
        _action = new PlayerAction();
        _model = GetComponent<Player_Model>();

        _action.Player.Move.performed += PlayerMovement;
        _action.Player.Move.canceled += PlayerMovement;
        _action.Player.Enable();
        Debug.Log("OnNetworkSpawn Works");
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        _action.Player.Disable();
        _action.Player.Move.canceled -= PlayerMovement;
        _action.Player.Move.performed -= PlayerMovement;
    }

    private void PlayerMovement(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        Vector2 _moveInput = context.ReadValue<Vector2>();
        MovementRpc(_moveInput);
        Debug.Log("Player Movement Works");
    }

    //Rpc sending to server so server handles the rest
    //Unreliable as sent every frame
    [Rpc(SendTo.Server, Delivery = RpcDelivery.Unreliable)]
    private void MovementRpc(Vector2 _moveInput)
    {
        _model.Movement(_moveInput);
        Debug.Log("Movement RPC Works");
    }
}

