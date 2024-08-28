using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMap : MonoBehaviour
{
    #region private º¯¼ö
    Transform tfFloor;
    Transform tfLeftUp;
    Transform tfLeftDown;
    Transform tfRightUp;
    Transform tfRightDown;

    CMapFloorBuilder floorBuilder;
    #endregion

    void Awake()
    {
        tfFloor = transform.GetChild(0);
        tfLeftUp = transform.GetChild(1);
        tfLeftDown = transform.GetChild(2);
        tfRightUp = transform.GetChild(3);
        tfRightDown = transform.GetChild(4);

        floorBuilder = tfFloor.GetComponent<CMapFloorBuilder>();
    }


    public void SerFloorPart()
    {

    }


    public void SetLeftUpPart()
    {

    }
}