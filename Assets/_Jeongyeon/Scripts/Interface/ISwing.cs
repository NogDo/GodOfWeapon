using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwing
{
    public IEnumerator PrePareSwing(float setY);
    public IEnumerator Swing();
}
