using UnityEngine;
using Unity.Netcode;
using Unity.Services.Matchmaker.Models;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BallSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    private List<GameObject> ballList;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        base.OnNetworkSpawn();
        ballList = new List<GameObject>();
    }
     

    private void Update()
    {
        if (!IsServer) return;
        int playerCount = NetworkManager.Singleton.ConnectedClientsList.Count;
        Debug.Log(playerCount);
        if(playerCount == 2 && ballList.Count <= 4)
        {
            Debug.Log("We are in the if statement");
            GameObject ball = Instantiate(ballPrefab);
            NetworkObject networkBall = ball.GetComponent<NetworkObject>();
            ballList.Add(ball);
            networkBall.Spawn();

        }
    }
}
