using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PackageLocalData
{
    private static string KEY = "PackageLocalDate";
    private static PackageLocalData _instance;


    public static PackageLocalData getInstance
    {
        get
        {
            _instance ??= new PackageLocalData();
            return _instance;
        }
    }


    public List<PackageLocalItem> items;

    public void SavePackageLocalData()
    {
        string packageLocalDataString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(KEY, packageLocalDataString);
        PlayerPrefs.Save();
    }


    public List<PackageLocalItem> ReadPackageLocalData()
    {
        if (items != null)
        {
            return items;
        }
        if (!PlayerPrefs.HasKey(KEY))
        {
            items = new List<PackageLocalItem>();
            return items;
        }
        string josnString = PlayerPrefs.GetString(KEY);
        PackageLocalData data = JsonUtility.FromJson<PackageLocalData>(josnString);
        items = data.items;
        return items;
    }


    public void ClearPackageLocalData(){
        if (!PlayerPrefs.HasKey(KEY)){
            return;
        }
        PlayerPrefs.DeleteKey(KEY);
    }
}


[System.Serializable]
public class PackageLocalItem
{

    public string uuid;
    public int id;
    public int num;
    public int level;
    public bool isNew;

    public override string ToString()
    {
        return String.Format("[uuid]:{0},[id]:{1},[num]:{2}", uuid, id, num);
    }
}
