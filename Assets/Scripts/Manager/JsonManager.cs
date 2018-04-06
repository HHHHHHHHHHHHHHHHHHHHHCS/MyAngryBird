using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

public class JsonManager
{
    private static JsonManager _instance;
    public static JsonManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new JsonManager();
                _instance.CreateDefaultJson();
            }
            return _instance;
        }
    }

    #region Json Path
    private const string _fileDir = "Save";
    private const string _fileName = "GameData.json";
    private readonly string fileDir;
    private readonly string filePath;

    public JsonManager()
    {
#if UNITY_ANDROID
        fileDir = string.Format("{0}/{1}", Application.persistentDataPath,fileDir);
        filePath = string.Format("{0}/{1}", fileDir, fileName);
#elif UNITY_IPHONE
        fileDir = string.Format("{0}/{1}", Application.persistentDataPath,fileDir);
        filePath = string.Format("{0}/{1}", fileDir, fileName);
#else
        fileDir = string.Format("{0}/{1}", Application.dataPath, _fileDir);
        filePath = string.Format("{0}/{1}", fileDir, _fileName);
#endif
    }
    #endregion

    #region Json Property
    private const string _needStar = @"needStar";
    private const string _levelStar = @"levelStar";
    private const int maxMapCount = 7;
    private const int maxLevelCount = 10;
    #endregion

    #region Base Function
    private string Read()
    {
        string result = "";
        if (CheckGameData())
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                result = sr.ReadToEnd();
            }
        }
        return result;
    }


    private T GetData<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    private string MakeJson(object o)
    {
        return JsonConvert.SerializeObject(o);
    }

    private string SaveData(object gameData)
    {
        if (!Directory.Exists(fileDir))
        {
            Directory.CreateDirectory(fileDir);
        }
        string jsonStr = MakeJson(gameData);
        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.Write(jsonStr);
        }
        return jsonStr;
    }

    private string UpdateData(object gameData)
    {
        return SaveData(gameData);
    }

    private bool CheckGameData()
    {
        if (File.Exists(filePath))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region High Function


    public bool CreateDefaultJson()
    {
        if (CheckGameData())
        {
            return false;
        }
        int[] needStar = new int[maxMapCount];
        int[,] levelStar = new int[maxMapCount, maxLevelCount];
        for (int i = 0; i < needStar.Length; i++)
        {
            needStar[i] = i * maxLevelCount / 2 * 3;
        }
        for (int y = 0; y < levelStar.GetLength(0); y++)
        {
            for (int x = 0; x < levelStar.GetLength(1); x++)
            {
                levelStar[y, x] = x == 0 ? 0 : -1;
            }
        }
        var needStarStr = JsonConvert.SerializeObject(needStar);
        var levelStarStr = JsonConvert.SerializeObject(levelStar);
        JObject jo = new JObject();
        jo.Add(_needStar, needStarStr);
        jo.Add(_levelStar, levelStarStr);


        SaveData(jo);
        return true;
    }

    public int[] ReadNeedStar()
    {

        JObject jo = JObject.Parse(Read());
        return GetData<int[]>(jo[_needStar].ToString());
    }

    //public bool UnlockAllNeedStar()
    //{
    //
    //    int[] needStar = new int[maxMapCount];
    //    JObject jo = JObject.Parse(Read());
    //    jo[_needStar] = JToken.Parse(MakeJson(needStar));
    //    return !string.IsNullOrEmpty(UpdateData(jo));
    //}

    public int[,] ReadLevelStar()
    {
        JObject jo = JObject.Parse(Read());
        return GetData<int[,]>(jo[_levelStar].ToString());
    }

    public bool UpdateLevelStar(int nowMap, int nowLevel, int star, bool autoUnlock = true)
    {
        JObject jo = JObject.Parse(Read());
        var array = ReadLevelStar();
        if (star > array[nowMap, nowLevel])
        {
            array[nowMap, nowLevel] = star;
            if (autoUnlock && nowLevel + 1 < maxLevelCount
                && star > 0 && array[nowMap, nowLevel + 1] < 0)
            {
                array[nowMap, nowLevel + 1] = 0;
            }
            jo[_levelStar] = JToken.Parse(MakeJson(array));
            return !string.IsNullOrEmpty(UpdateData(jo));
        }
        return false;
    }


    public int ReadMaxLevelCount()
    {
        JObject jo = JObject.Parse(Read());
        var array = ReadLevelStar();
        return array.GetLength(1);
    }

    #endregion

}
