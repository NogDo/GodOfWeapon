using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

[Serializable]
public class JsonData
{
    public string hash;
    public string json;

    public JsonData(string hash, string json)
    {
        this.hash = hash;
        this.json = json;
    }
}

public class HashHelper
{
    public static string CreateHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hash;
        }
    }
}
