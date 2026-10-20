using System;
using TMPro;
using Unity.Netcode;
using Unity.Services.Qos.V2.Models;
using UnityEngine;

public class Player_View : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText1;
    [SerializeField]
    private TextMeshProUGUI scoreText2;

    [SerializeField]   
    private Player_Model player_Model;
    private void OnEnable()
    {
        GameObject scoreObject1 = GameObject.Find("Score1");
        scoreText1 = scoreObject1.GetComponent<TextMeshProUGUI>();
        GameObject scoreObject2 = GameObject.Find("Score2");
        scoreText2 = scoreObject2.GetComponent<TextMeshProUGUI>();
        player_Model = GetComponent<Player_Model>();
        player_Model.ScoreChange += ScoreUp;
    }

   

    public void ScoreUp(ulong playerId, int score)
    {

        Debug.Log("Player ID = " + playerId);
        
        if (playerId == 0)
        {
            Debug.Log("Player 1 score");
            scoreText1.text = "Player 1 Score: " + score.ToString();
        }
        if (playerId == 1)
        {
            Debug.Log("Player 2 score");
            scoreText2.text = "Player 2 Score: " + score.ToString();
        }
    }
}
