using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // メンバー変数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    [SerializeField] CircleShieldManager circleShieldManager;
    [SerializeField] GameObject brokenPlayerEffect;
    [SerializeField] Slider lifePointGauge;
    [SerializeField] Slider shieldGauge;
    [SerializeField] LayerMask blockLayer;

    Rigidbody2D rb2d;
    Animator animator;
    CircleShieldManager circleShield;

    // スコア取得時デリゲート
    public delegate void GetScore(int score);
    public GetScore getScore;

    int speed = 0;
    int jumpPower = 400;
    public bool isDamaged = false;
    public bool isBreakPlayer = false;
    public bool isUnrivaled = false;

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // 関数
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lifePointGauge.value = 3;
        shieldGauge.value = 0;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if (x == 0)
        {
            // 止まっている
            direction = DIRECTION_TYPE.STOP;
            animator.SetBool("isRunning", false);
        }
        else if (x > 0)
        {
            // →
            direction = DIRECTION_TYPE.RIGHT;
            animator.SetBool("isRunning", true);
        }
        else if (x < 0)
        {
            // ←
            direction = DIRECTION_TYPE.LEFT;
            animator.SetBool("isRunning", true);
        }

        if (IsGround())
        {
            if (Input.GetKeyDown("space"))
            {
                rb2d.AddForce(Vector2.up * jumpPower);
            }
        }
        if (Input.GetKeyDown("q") && shieldGauge.value >= 1)
        {
            if (circleShield == null)
            {
                circleShield = Instantiate(circleShieldManager, this.transform.position, Quaternion.identity);
                shieldGauge.value = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 4;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -4;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

        //ダメージを受けた時の処理
        if (isDamaged)
        {
            StartCoroutine("DamegedPlayerEffect");
        }
        if (shieldGauge.value == 0)
        {
            StartCoroutine("ShieldGaugeIncrement");
        }
    }

    bool IsGround()
    {
        // 始点と終点を作成
        Vector3 leftStartPoint = transform.position - Vector3.right * 0.05f - Vector3.up * 0.45f;
        Vector3 rightStartPoint = transform.position + Vector3.right * 0.17f - Vector3.up * 0.45f;
        Vector3 endPoint = transform.position - Vector3.up * 0.1f - Vector3.up * 0.45f;
        Debug.DrawLine(leftStartPoint, endPoint);
        Debug.DrawLine(rightStartPoint, endPoint);

        return Physics2D.Linecast(leftStartPoint, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartPoint, endPoint, blockLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void BreakPlayer()
    {
        // isBrokenPlayer = true;
        Instantiate(brokenPlayerEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
        isBreakPlayer = true;
        // SceneManager.LoadScene("Title");
    }

    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
    // コルーチン
    // ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊

    IEnumerator DamegedPlayerEffect()
    {
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        isDamaged = false;
        yield break;
    }

    IEnumerator ShieldGaugeIncrement()
    {
        while (shieldGauge.value < 1)
        {
            shieldGauge.value += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
}
