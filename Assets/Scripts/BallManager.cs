using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // メンバー変数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    enum BallType
    {
        EnemyBall = 1, 
        ScoreBall = 2,
        BigScoreBall = 3,
    }

    [SerializeField] GameObject EnemyBallPrefab;
    [SerializeField] GameObject ScoreBallPrefab;
    [SerializeField] GameObject BigScoreBallPrefab;

    float angle = 0;
    float speed = 0;

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // 関数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    public void Add(int type, MonoBehaviour center, float angle, float speed)
    {
        this.angle = angle;
        this.speed = speed;

        GameObject ball = null;
        if (type == (int)BallType.EnemyBall)
        {
            // EnemyBall生成
            ball = Instantiate(EnemyBallPrefab, center.transform.position, Quaternion.identity);
        }
        else if (type == (int)BallType.ScoreBall)
        {
            // ScoreBall生成
            ball = Instantiate(ScoreBallPrefab, center.transform.position, Quaternion.identity);
        }
        else if (type == (int)BallType.BigScoreBall)
        {
            // ScoreBall生成
            ball = Instantiate(BigScoreBallPrefab, center.transform.position, Quaternion.identity);
        }

        SetShotAngle(ball);
        Destroy(ball, UnityEngine.Random.value * 8);
    }

    void SetShotAngle(GameObject ball)
    {
        // EnemyBall速度・方向設定
        //角度をラジアン角に変換
        float rad = this.angle * Mathf.Deg2Rad; 

        //rad(ラジアン角)から発射用ベクトルを作成
        double addforceX = Math.Sin(rad);
        double addforceY = Math.Cos(rad);
        Vector2 shotVector = new Vector2((float)addforceX, (float)addforceY);

        var rb2d = ball.GetComponent<Rigidbody2D>();
        rb2d.AddForce(shotVector * this.speed);
    }

}
