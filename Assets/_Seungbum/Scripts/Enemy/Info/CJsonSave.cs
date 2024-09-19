using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class CJsonSave : MonoBehaviour
{
    #region public ����
    public List<EnemyStats> enemystatsList;
    #endregion

    /// <summary>
    /// �� ���� ���� ����Ʈ�� Json���� �Ľ��� �����Ѵ�.
    /// </summary>
    public void SaveEnemyStatsList()
    {
        string path = $"{Application.streamingAssetsPath}/Enemys_Data.json";
        string json = JsonConvert.SerializeObject(enemystatsList, Formatting.Indented);

        File.WriteAllText(path, json);
    }
}