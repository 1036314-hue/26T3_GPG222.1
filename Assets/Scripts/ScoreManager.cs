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
        if(playerId == 0)
        {
            player1Score.Value++;
        }
      if(playerId == 1)
        {
            player2Score.Value++;
        }
    }
}
