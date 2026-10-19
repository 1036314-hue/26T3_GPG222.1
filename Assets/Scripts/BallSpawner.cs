using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;


public class BallSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    public List<GameObject> ballList;
    public static BallSpawner instance;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

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
        if (playerCount == 2 && ballList.Count <= 4)
        {
            Debug.Log("We are in the if statement");
            float x = Random.Range(-4.5f, 4.5f);
            float z = Random.Range(-4.5f, 4.5f);
            GameObject ball = Instantiate(ballPrefab, new Vector3(x, 0, z), Quaternion.identity);
            NetworkObject networkBall = ball.GetComponent<NetworkObject>();
            ballList.Add(ball);
            networkBall.Spawn();

        }
    }
}
