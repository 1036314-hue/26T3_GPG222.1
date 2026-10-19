using System;
using Unity.Netcode;
using UnityEngine;

public class Player_Model : NetworkBehaviour
{
    private Player_View _view;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private int _walkSpeed;
    private Vector2 _movementValue;


    public void Movement(Vector2 _moveInput)
    {
        Debug.Log("Movement");
        _movementValue = _moveInput;
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_movementValue.x, 0, _movementValue.y).normalized;
        _rb.MovePosition(_rb.position + direction * (_walkSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;
        if (collision.transform.name == "Ball(Clone)")
        {
            NetworkObject networkBall = collision.transform.GetComponent<NetworkObject>();
            if (BallSpawner.instance.ballList.Contains(collision.gameObject))
            {
                BallSpawner.instance.ballList.Remove(collision.gameObject);
            }
            networkBall.Despawn();

            ScoreUpdateRpc();

        }
    }

    [Rpc(SendTo.ClientsAndHost, Delivery = RpcDelivery.Reliable)]
    private void ScoreUpdateRpc()
    {
        ulong playerId = NetworkManager.Singleton.LocalClientId;
        ScoreManager.instance.ScoreUp(playerId);
    }
}
