using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public string userId;
    public string userName;

    public UserData()
    {
        
    }

    public UserData(string userId, string userName)
    {
        this.userId = userId;
        this.userName = userName;
    }
}
