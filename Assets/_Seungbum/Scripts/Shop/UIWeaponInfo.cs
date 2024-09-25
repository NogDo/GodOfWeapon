using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponInfo : MonoBehaviour
{
    #region public 변수

    #endregion

    #region private 변수
    CWeaponStats weapon;
    #endregion

    /// <summary>
    /// 무기 정보 패널의 크기와 텍스트를 설정한다.
    /// </summary>
    /// <param name="weapon">무기 정보</param>
    public void SetItemInfoPanel(CWeaponStats weapon)
    {
        this.weapon = weapon;

        //SetPanelSize();
        //SetItemInfoText();
        //SetItemImage();
    }


    
}