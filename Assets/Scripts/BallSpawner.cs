using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;


public class BallSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    public List<GameObject> ballList;
    public static BallSpawner instance;

    //On awake to make it a singleton
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    //Making sure we create the ball list serverside
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        base.OnNetworkSpawn();
        ballList = new List<GameObject>();
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }

    //Spawn balls repeatedly 
    private void Update()
    {
        //Keeps Serverside
        if (!IsServer) return;
        int playerCount = NetworkManager.Singleton.ConnectedClientsList.Count;
        Debug.Log(playerCount);
        //Checks Playercount and ball list amount 
        if (playerCount == 2 && ballList.Count <= 4)
        {
            Debug.Log("We are in the if statement");
            //Spawn in random location
            float x = Random.Range(-4.5f, 4.5f);
            float z = Random.Range(-4.5f, 4.5f);
            //Instantiate summons gameobject :_( Not networked
            GameObject ball = Instantiate(ballPrefab, new Vector3(x, 0, z), Quaternion.identity);
            //Turning gameobject into networked object
            NetworkObject networkBall = ball.GetComponent<NetworkObject>();
            ballList.Add(ball);
            networkBall.Spawn();

        }
    }
}
