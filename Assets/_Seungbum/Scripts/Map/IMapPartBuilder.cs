using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapPartBuilder
{
    CMapPart GetMapPart();
    void BuildDownWall();
    void BuildUpWall();
    void BuildFloor();
    void BuildDeco();
}