using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShieldManager : MonoBehaviour
{
    // –––––––––––––––––––––––––––––––––––––––––––
    // ŠÖ”
    // –––––––––––––––––––––––––––––––––––––––––––

    void Update()
    {
        StartCoroutine("UntilDestroyShield");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBall")
        {
            Destroy(collision.gameObject);
        }
    }

    // –––––––––––––––––––––––––––––––––––––––––––
    // ƒRƒ‹[ƒ`ƒ“
    // –––––––––––––––––––––––––––––––––––––––––––

    IEnumerator UntilDestroyShield()
    {
        yield return new WaitForSeconds(2f);
        float level = Mathf.Abs(Mathf.Sin(Time.time * 7));
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        yield break;
    }
}
