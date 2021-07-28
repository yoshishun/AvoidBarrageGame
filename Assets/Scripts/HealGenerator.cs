using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealGenerator : MonoBehaviour
{
    // –––––––––––––––––––––––––––––––––––––––––––
    // ƒƒ“ƒo[•Ï”
    // –––––––––––––––––––––––––––––––––––––––––––

    Vector3[] posArr =
    {
        new Vector3(5.04f, -1.82f, 0.0f),
        new Vector3(0.93f, 1.05f, 0.0f),
        new Vector3(-1.81f, -3.47f, 0.0f),
        new Vector3(-6.9f, -1.1f, 0.0f),
    };

    [SerializeField] PlayerTriggerCollider playerTriggerCollider;
    [SerializeField] HealManager healManager;

    GameObject healBox = null;

    int posIdx = 0;

    // –––––––––––––––––––––––––––––––––––––––––––
    // ŠÖ”
    // –––––––––––––––––––––––––––––––––––––––––––

    void Start()
    {

        playerTriggerCollider.getHealBox += destroyHealBox;
        healBox = Instantiate(healManager.gameObject);
        posIdx++;
    }

    void destroyHealBox()
    {
        Destroy(healBox);
        Invoke("GenerateHealBox", 4f);
    }

    void GenerateHealBox()
    {
        healBox = Instantiate(healManager.gameObject, posArr[posIdx], Quaternion.identity);
        if (posIdx == 3)
        {
            posIdx = 0;
        }
        else
        {
            posIdx++;
        }
    }
}
