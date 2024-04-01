using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    private PackageTable packageTable;

    public static GameManager Instance
    {
        get
        {
            _instance ??= new GameManager();
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanael);
    }

    /// <summary>
    /// 获取本地装备数据
    /// </summary>
    /// <returns></returns>
    public PackageTable GetPackageTable()
    {
        if (packageTable == null)
        {
            packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        }
        return packageTable;
    }

    /// <summary>
    /// 获取本地动态数据
    /// </summary>
    /// <returns></returns>
    public List<PackageLocalItem> GetPackageLocalDatas()
    {
        return PackageLocalData.getInstance.ReadPackageLocalData();
    }

    /// <summary>
    /// 根据id获取本地装备数据
    /// </summary>
    /// <param name="id">装备id</param>
    /// <returns></returns>
    public PacakageTableItem GetPacakageTableItemById(int id)
    {
        List<PacakageTableItem> list = GetPackageTable().dataList;
        foreach (PacakageTableItem item in list)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据uuid获取动态数据
    /// </summary>
    /// <param name="uuid">动态数据uuid</param>
    /// <returns></returns>
    public PackageLocalItem GetPackageLocalItemByUuid(String uuid)
    {
        List<PackageLocalItem> list = PackageLocalData.getInstance.ReadPackageLocalData();
        foreach (PackageLocalItem item in list)
        {
            if (item.uuid.Equals(uuid))
            {
                return item;
            }
        }
        return null;
    }

    public List<PackageLocalItem> getSortedPackageLocalData()
    {
        List<PackageLocalItem> list = PackageLocalData.getInstance.ReadPackageLocalData();
        list.Sort(new PackageLocalItemCompare());
        return list;
    }

}

internal class PackageLocalItemCompare : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem x, PackageLocalItem y)
    {
        PacakageTableItem a = GameManager.Instance.GetPacakageTableItemById(x.id);
        PacakageTableItem b = GameManager.Instance.GetPacakageTableItemById(y.id);
        //首先根据star排列顺序
        int starComparison = b.star.CompareTo(a.star);
        //如果star相同，比较id
        if (starComparison == 0)
        {
            int idComparision = b.id.CompareTo(a.id);
            //如果id相同，比较level大小
            if (idComparision == 0)
            {
                return y.level.CompareTo(x.level);
            }
            return idComparision;
        }
        return starComparison;
    }
}