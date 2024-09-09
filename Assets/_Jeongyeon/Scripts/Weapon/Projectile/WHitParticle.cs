using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WHitParticle : MonoBehaviour
{
    public abstract void Return();

    public abstract void Play(Vector3 position);
}
