using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponInfo : MonoBehaviour
{
    #region public ����

    #endregion

    #region private ����
    CWeaponStats weapon;
    #endregion

    /// <summary>
    /// ���� ���� �г��� ũ��� �ؽ�Ʈ�� �����Ѵ�.
    /// </summary>
    /// <param name="weapon">���� ����</param>
    public void SetItemInfoPanel(CWeaponStats weapon)
    {
        this.weapon = weapon;

        //SetPanelSize();
        //SetItemInfoText();
        //SetItemImage();
    }


    
}