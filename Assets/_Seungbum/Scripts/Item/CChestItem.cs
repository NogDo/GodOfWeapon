using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            Use(character);
        }
    }

    /// <summary>
    /// 상자 아이템을 사용한다.
    /// </summary>
    /// <param name="character">캐릭터</param>
    protected virtual void Use(Character character) 
    {
        Destroy(gameObject);
    }
}
