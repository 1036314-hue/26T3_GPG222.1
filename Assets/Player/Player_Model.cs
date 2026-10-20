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
    public event Action <ulong, int> ScoreChange;
    public NetworkVariable<int> Score = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();  
        _view = GetComponent<Player_View>();
        Score.OnValueChanged += OnScore;
        ScoreChange?.Invoke(OwnerClientId, Score.Value);
    }

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

    private void OnScore(int previousValue, int newValue)
    {
        ScoreChange?.Invoke(OwnerClientId, Score.Value);
    }

    [Rpc(SendTo.Server, Delivery = RpcDelivery.Reliable)]
    private void ScoreUpdateRpc()
    {
        Score.Value++;
    }
}
