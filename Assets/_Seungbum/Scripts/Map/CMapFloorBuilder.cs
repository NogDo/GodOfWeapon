using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapFloorBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private ����
    CMapPart mapPart;
    #endregion

    public CMapPart GetMapPart()
    {
        return mapPart;
    }

    public void BuildFloor()
    {

    }

    public void BuildDownWall()
    {

    }

    public void BuildUpWall()
    {

    }

    public void BuildDeco()
    {

    }
}