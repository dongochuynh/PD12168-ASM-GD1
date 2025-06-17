using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN ASM_MN;

    void Awake()
    {
        scoreKeeper = ScoreKeeper.Instance;
        ASM_MN = ASM_MN.Instance;
    }

    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();

        // Assuming you need to pass playerName, score, and regionID to YC1
        string playerName = "Player1"; // Replace with actual player name
        int score = scoreKeeper.GetScore(); // Replace with actual score
        int regionID = 1; // Replace with actual region ID

        ASM_MN.YC1(playerName, score, regionID);
        ASM_MN.YC2();
        ASM_MN.YC3();
        ASM_MN.YC4();
        ASM_MN.YC5();
        ASM_MN.YC6();
        ASM_MN.YC7();
    }
}
