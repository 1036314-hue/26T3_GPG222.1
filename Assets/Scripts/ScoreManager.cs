using Unity.Netcode;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager instance;
    public NetworkVariable<int> player1Score = new NetworkVariable<int>();
    public NetworkVariable<int> player2Score = new NetworkVariable<int>();

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void ScoreUp(ulong playerId)
    {
        
        Debug.Log("Player ID = " + playerId);
        if (!IsServer) return;
        if (playerId == 0)
        {
            Debug.Log("Player 1 score");
            player1Score.Value++;
        }
        if(playerId == 1)
        {
            Debug.Log("Player 2 score");
            player2Score.Value++;
        }
    }
}
