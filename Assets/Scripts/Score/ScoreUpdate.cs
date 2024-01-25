using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdate : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnSumScore.AddListener(UpdateScore);
    }
    public void UpdateScore(int score)
    {
        GetComponent<TextMeshProUGUI>().text = $"Score:  {score}";
        ///GetComponent<TextMeshProUGUI>().text = $"Time: {GameManager.timeInGame}";

    }

    private void OnDisable()
    {
        GameManager.OnSumScore.RemoveListener(UpdateScore); 
    }
}
