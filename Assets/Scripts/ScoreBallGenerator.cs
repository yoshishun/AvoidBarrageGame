using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBallGenerator : MonoBehaviour
{
    // ��������������������������������������������������������������������������������������
    // �����o�[�ϐ�
    // ��������������������������������������������������������������������������������������
    const int BALL_TYPE = 2;
    const int BIGBALL_TYPE= 3;

    [SerializeField] BallManager scoreBallManager;

    public float shotSpeed = 200;

    // ��������������������������������������������������������������������������������������
    // �֐�
    // ��������������������������������������������������������������������������������������

    void Start()
    {
        StartCoroutine("GenerateAllDirection");
    }

    // ��������������������������������������������������������������������������������������
    // �R���[�`��
    // ��������������������������������������������������������������������������������������

    IEnumerator GenerateAllDirection()
    {
        while (true)
        {
            for (int angle = 0; angle < 360; angle += 15)
            {
                scoreBallManager.Add(BALL_TYPE, this, angle, shotSpeed);
            }
            yield return new WaitForSeconds(2f);
            scoreBallManager.Add(BIGBALL_TYPE, this, 170, shotSpeed);
        }
    }

}
