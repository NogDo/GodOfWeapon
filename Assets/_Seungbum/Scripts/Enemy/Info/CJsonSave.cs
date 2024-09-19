using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class CJsonSave : MonoBehaviour
{
    #region public 변수
    public List<EnemyStats> enemystatsList;
    #endregion

    /// <summary>
    /// 적 스텟 정보 리스트를 Json으로 파싱해 저장한다.
    /// </summary>
    public void SaveEnemyStatsList()
    {
        string path = $"{Application.streamingAssetsPath}/Enemys_Data.json";
        string json = JsonConvert.SerializeObject(enemystatsList, Formatting.Indented);

        File.WriteAllText(path, json);
    }
}