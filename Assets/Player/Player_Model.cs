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

    //Calls when the object loads into the Network
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();  
        //Gets the Player View Script for Cross Script Reference
        _view = GetComponent<Player_View>();
        //Assigns to the event
        Score.OnValueChanged += OnScore;
        //Invokes Event to update score on entry
        ScoreChange?.Invoke(OwnerClientId, Score.Value);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }
    
    public void Movement(Vector2 _moveInput)
    {
        Debug.Log("Movement");
        _movementValue = _moveInput;
    }

    private void Update()
    {
        //Movement based on void Movement above
        Vector3 direction = new Vector3(_movementValue.x, 0, _movementValue.y).normalized;
        _rb.MovePosition(_rb.position + direction * (_walkSpeed * Time.deltaTime));
    }

    //Collision Gain Score
    private void OnCollisionEnter(Collision collision)
    {
        //Keeps the actions happening serverside
        if (!IsServer) return;
        //Check Object name
        if (collision.transform.name == "Ball(Clone)")
        {
            //Get the NetworkObject of the item so we can despawn
            NetworkObject networkBall = collision.transform.GetComponent<NetworkObject>();
            //Removes ball from list so we can spawn another 
            if (BallSpawner.instance.ballList.Contains(collision.gameObject))
            {
                BallSpawner.instance.ballList.Remove(collision.gameObject);
            }
            //Despawns and destroys ball
            networkBall.Despawn();
            //Update score
            ScoreUpdateRpc();

        }
    }

    private void OnScore(int previousValue, int newValue)
    {
        //Using ClientId to identify each players score and keep separate with the same variable name
        ScoreChange?.Invoke(OwnerClientId, Score.Value);
    }

    //Important info needs to be sent reliably
    [Rpc(SendTo.Server, Delivery = RpcDelivery.Reliable)]
    private void ScoreUpdateRpc()
    {
        //Updates Score to be higher 
        Score.Value++;
    }
}
