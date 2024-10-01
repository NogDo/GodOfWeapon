using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineParticle : MonoBehaviour
{
    #region Public Fields
    public GameObject[] combineParticleTrail;
    public GameObject[] combineParticleExplosion;
    #endregion


    public IEnumerator CombineBazier(CWeaponStats sourceWeapon, int i)
    {
        combineParticleTrail[i].transform.parent = null;
        combineParticleExplosion[i].transform.parent = null;
        combineParticleTrail[i].transform.position = sourceWeapon.transform.position;
        combineParticleExplosion[i].transform.position = sourceWeapon.transform.position + Vector3.right / 3;
        combineParticleTrail[i].SetActive(true);
        combineParticleExplosion[i].SetActive(true);
        Vector3 endPostion = UIManager.Instance.baseWeapon.transform.position + Vector3.right /3;
        Vector3 startPostion = sourceWeapon.transform.position + Vector3.right / 3;
        Vector3 midlePosition = (endPostion + startPostion) / 2 + (Vector3.up*2f);
        Vector3 startToMidlePostion;
        Vector3 midleToEndPosition;
        float time = 0;
        float duration = 1.0f;
        while (time <= duration)
        {
            startToMidlePostion = Vector3.Lerp(startPostion, midlePosition, time / duration);
            midleToEndPosition = Vector3.Lerp(midlePosition, endPostion, time / duration);
            combineParticleTrail[i].transform.position = Vector3.Lerp(startToMidlePostion, midleToEndPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        combineParticleTrail[i].transform.position = endPostion;
        combineParticleTrail[i].SetActive(false);
        combineParticleExplosion[i].SetActive(false);
        combineParticleTrail[i].transform.parent = transform;
        combineParticleExplosion[i].transform.parent = transform;
        Debug.Log("컴바인완료");
        yield return null;
    }
}
