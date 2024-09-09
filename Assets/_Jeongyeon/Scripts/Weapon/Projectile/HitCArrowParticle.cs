using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCArrowParticle : WHitParticle
{
    #region Public Fields
    #endregion

    #region Private Fields
    private GameObject startParent;
    private WHitParticlePool hitParticlePool;
    #endregion

    private void Awake()
    {
        startParent = transform.parent.gameObject;
        hitParticlePool = GetComponentInParent<WHitParticlePool>();
    }

    public override void Play(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        Invoke("Return", 1.0f);
    }
    public override void Return()
    {
        transform.parent = startParent.transform;
        hitParticlePool.ReturnSParticle(this);
    }
}
