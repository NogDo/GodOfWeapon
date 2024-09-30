using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStartGate : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            CStageManager.Instance.StartStage();
        }
    }
}