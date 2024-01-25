using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //ScoreUpdate textUpdateScore;
    public static int whenPlayerPassCube;

    public static UnityEvent<int> OnSumScore = new UnityEvent<int>();
    public void Start()
    {
        whenPlayerPassCube = 0;       
    }

    public static void UpdatewhenPlayerPassCube(int points)
    {
        whenPlayerPassCube += points;
        OnSumScore?.Invoke(whenPlayerPassCube);
        //ScoreUpdate.UpdateScore();
    }


}
