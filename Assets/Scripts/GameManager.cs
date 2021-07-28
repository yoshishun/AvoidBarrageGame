using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // メンバー変数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    [SerializeField] Text ScoreCount;
    [SerializeField] Text ElapsedTimeCount;
    [SerializeField] PlayerManager playerManager;

    int TotalScore = 0;
    float ElapsedTime = 0.0f;

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // 関数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    void Start()
    {
        // デリゲート登録
        playerManager.getScore += AddScore;
    }

    void Update()
    {
        AddElapsedTime();
    }

    public void AddScore(int score)
    {
        TotalScore += score;
        ScoreCount.text = TotalScore.ToString();
    }

    void AddElapsedTime()
    {
        if (!playerManager.isBreakPlayer)
        {
            ElapsedTime += Time.deltaTime;
            ElapsedTimeCount.text = ElapsedTime.ToString("f2") + "s";
        }
    }
}
