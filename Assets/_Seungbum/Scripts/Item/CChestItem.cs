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
    /// ���� �������� ����Ѵ�.
    /// </summary>
    /// <param name="character">ĳ����</param>
    protected virtual void Use(Character character) 
    {
        Destroy(gameObject);
    }
}
