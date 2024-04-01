using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GMCmd
{

    [MenuItem("GMCmd/读取装备")]
    public static void readPackageTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        foreach (PacakageTableItem item in packageTable.dataList)
        {
            Debug.Log(string.Format("[id]:{0},[name]:{1}", item.id, item.name));
        }
    }


    [MenuItem("GMCmd/创建背包测试数据")]
    public static void CreatePackageLocalData()
    {
        //保存数据
        PackageLocalData.getInstance.items = new List<PackageLocalItem>();
        for (int i = 0; i < 8; i++)
        {
            PackageLocalItem item = new()
            {
                uuid = Guid.NewGuid().ToString(),
                id = i,
                num = i,
                level = (i + 1) % 5,
                isNew = i % 2 == 1
            };
            PackageLocalData.getInstance.items.Add(item);
        }
        PackageLocalData.getInstance.SavePackageLocalData();
    }


    [MenuItem("GMCmd/读取背包测试数据")]
    public static void readPackageLocalData()
    {
        //读取数据
        List<PackageLocalItem> localItems = PackageLocalData.getInstance.ReadPackageLocalData();
        foreach (var item in localItems)
        {
            Debug.Log(item);
        }
    }

    [MenuItem("GMCmd/清除背包测试数据")]
    public static void ClearPackageLocalData()
    {
        PackageLocalData.getInstance.ClearPackageLocalData();
    }

    [MenuItem("GMCmd/打开背包界面")]
    public static void OpenPanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanael);
    }
}
