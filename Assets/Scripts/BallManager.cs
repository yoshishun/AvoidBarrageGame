using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    // 
    // o[Ο
    // 

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

    // 
    // Φ
    // 

    public void Add(int type, MonoBehaviour center, float angle, float speed)
    {
        this.angle = angle;
        this.speed = speed;

        GameObject ball = null;
        if (type == (int)BallType.EnemyBall)
        {
            // EnemyBallΆ¬
            ball = Instantiate(EnemyBallPrefab, center.transform.position, Quaternion.identity);
        }
        else if (type == (int)BallType.ScoreBall)
        {
            // ScoreBallΆ¬
            ball = Instantiate(ScoreBallPrefab, center.transform.position, Quaternion.identity);
        }
        else if (type == (int)BallType.BigScoreBall)
        {
            // ScoreBallΆ¬
            ball = Instantiate(BigScoreBallPrefab, center.transform.position, Quaternion.identity);
        }

        SetShotAngle(ball);
        Destroy(ball, UnityEngine.Random.value * 8);
    }

    void SetShotAngle(GameObject ball)
    {
        // EnemyBall¬xEϋόέθ
        //pxπWApΙΟ·
        float rad = this.angle * Mathf.Deg2Rad; 

        //rad(WAp)©η­ΛpxNgπμ¬
        double addforceX = Math.Sin(rad);
        double addforceY = Math.Cos(rad);
        Vector2 shotVector = new Vector2((float)addforceX, (float)addforceY);

        var rb2d = ball.GetComponent<Rigidbody2D>();
        rb2d.AddForce(shotVector * this.speed);
    }

}
