using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Player_View : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public override void OnNetworkSpawn()
    {
        ScoreManager.instance.player1Score.OnValueChanged += UpdateScore;
        GameObject scoreObject = GameObject.Find("Score");
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
    }

    private void UpdateScore(int previousValue, int newValue)
    {
        scoreText.text = "Player 1 Score: " + ScoreManager.instance.player1Score.Value.ToString() + "\nPlayer 2 Score: " + ScoreManager.instance.player2Score.Value.ToString();
    }

    
}
