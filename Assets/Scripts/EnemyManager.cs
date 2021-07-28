using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // メンバー変数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    const int BALL_TYPE = 1;

    Vector2[] posArr =
    {
        new Vector2(-9f, 3.7f),
        new Vector2(-9f, -3.5f),
        new Vector2(8.9f, -3.5f),
        new Vector2(8.9f, 3.7f),
    };

    [SerializeField] BallManager enemyBallManager;

    public float shotSpeed = 200;
    int posNum = 0;

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // 関数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    void Start()
    {
        StartCoroutine("Move");
    }

    private void Update()
    {
        
    }

    void ShotAllDirection()
    {
        for (int angle = 0; angle < 360; angle += 44)
        {
            enemyBallManager.Add(BALL_TYPE, this, angle, shotSpeed);
        }
    }

    void ShotHalfDirection()
    {
        for (int angle = 0; angle <= 180; angle += 29)
        {
            enemyBallManager.Add(BALL_TYPE, this, angle, shotSpeed);
        }
    }

    void MovePosition()
    {
        transform.position = posArr[posNum];
        if (posNum == 3)
        {
            posNum = 0;
        }
        else
        {
            posNum++;
        }
    }

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // コルーチン
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    IEnumerator Move()
    {
        while (true)
        {
            ShotAllDirection();
            yield return new WaitForSeconds(1f);
            StartCoroutine("ShotAllDirectionInterval");
            ShotHalfDirection();
            yield return new WaitForSeconds(2f);
            MovePosition();
        }
    }

    IEnumerator ShotAllDirectionInterval()
    {
        for (int angle = 0; angle < 360; angle += 44)
        {
            enemyBallManager.Add(BALL_TYPE, this, angle, shotSpeed);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
