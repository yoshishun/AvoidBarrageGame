using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerCollider : MonoBehaviour
{
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // メンバー変数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    [SerializeField] PlayerManager playerManager;
    [SerializeField] Slider lifePointGauge;
    [SerializeField] HealManager healManager;

    public delegate void GetHealBox();
    public GetHealBox getHealBox;

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // 関数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "EnemyBall":
                if (!playerManager.isUnrivaled)
                {
                    Destroy(collision.gameObject);
                    playerManager.isDamaged = true;
                    StartCoroutine("PlayerUnrivaledTime");

                    lifePointGauge.value--;
                    if (lifePointGauge.value == 0)
                    {
                        playerManager.BreakPlayer();
                    }
                }
                break;
            case "ScoreBall":
                Destroy(collision.gameObject);
                playerManager.getScore(100);
                break;
            case "BigScoreBall":
                Destroy(collision.gameObject);
                playerManager.getScore(1000);
                break;
            case "Heal":
                getHealBox();

                if (lifePointGauge.value != 3)
                {
                    lifePointGauge.value++;
                }
                break;
        }
    }

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // コルーチン
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    IEnumerator PlayerUnrivaledTime()
    {
        playerManager.isUnrivaled = true;
        yield return new WaitForSeconds(1f);
        playerManager.isUnrivaled = false;
        yield break;
    }
}
