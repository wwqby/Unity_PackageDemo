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
        // UIManager.Instance.OpenPanel(UIConst.PackagePanel);
        // UIManager.Instance.OpenPanel(UIConst.LotteryPanel);
        UIManager.Instance.OpenPanel(UIConst.MainPanel);
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
    public PackageTableItem GetPacakageTableItemById(int id)
    {
        List<PackageTableItem> list = GetPackageTable().dataList;
        foreach (PackageTableItem item in list)
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


    public List<PackageTableItem> GetPacakageTableItemsByType(int type)
    {
        List<PackageTableItem> list = new List<PackageTableItem>();
        foreach (PackageTableItem item in GameManager.Instance.GetPackageTable().dataList)
        {
            if (item.type == type)
            {
                list.Add(item);
            }
        }
        return list;
    }

    /// <summary>
    /// TODO 增加不同星级的概率
    /// </summary>
    /// <returns>新武器</returns>
    public PackageLocalItem getLotteryRandomOnce()
    {
        List<PackageTableItem> tableList = GameManager.Instance.GetPacakageTableItemsByType(PackageType.WEPEAN);
        int index = UnityEngine.Random.Range(0, tableList.Count-1);
        PackageTableItem item = tableList[index];
        PackageLocalItem localItem = new PackageLocalItem
        {
            uuid = System.Guid.NewGuid().ToString(),
            id = item.id,
            num = 1,
            level = UnityEngine.Random.Range(0, 40),
            isNew = CheckWepeanNew(item.id),
        };
        PackageLocalData.getInstance.ReadPackageLocalData().Add(localItem);
        PackageLocalData.getInstance.SavePackageLocalData();
        return localItem;
    }

    /// <summary>
    /// 检查是否是新获得的武器类型
    /// </summary>
    /// <param name="id">武器型号id</param>
    /// <returns>true 新武器</returns>
    private bool CheckWepeanNew(int id)
    {
        List<PackageLocalItem> list = GameManager.Instance.GetPackageLocalDatas();
        foreach (PackageLocalItem item in list){
            if (item.id == id)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 抽取10次武器
    /// </summary>
    /// <param name="sort">抽取结果是否排序</param>
    /// <returns>10次抽取结果</returns>
    public List<PackageLocalItem> getLotteryWepeanTence(bool sort){
        List<PackageLocalItem> list = new List<PackageLocalItem>();
        for(int i = 0; i < 10;i++){
            PackageLocalItem item = GameManager.Instance.getLotteryRandomOnce();
            list.Add(item);
        }
        if(sort){
            list.Sort(new PackageLocalItemCompare());
        }
        return list;
    }


    public void DeletePackageItems(List<string> list){
        foreach(string item in list){
            DeletePackageItems(item,false);
        }
        PackageLocalData.getInstance.SavePackageLocalData();
    }

    private void DeletePackageItems(string uuid, bool needSave)
    {
        List<PackageLocalItem> list = PackageLocalData.getInstance.items;
        if(list==null){
            return;
        }
        list.RemoveAll(x => x.uuid.Equals(uuid));
        if(needSave){
            PackageLocalData.getInstance.SavePackageLocalData();
        }
    }
}

internal class PackageLocalItemCompare : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem x, PackageLocalItem y)
    {
        PackageTableItem a = GameManager.Instance.GetPacakageTableItemById(x.id);
        PackageTableItem b = GameManager.Instance.GetPacakageTableItemById(y.id);
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

/// <summary>
/// 背包数据类型
/// 0 武器
/// 1 食物
/// </summary>
public class PackageType
{
    public const int WEPEAN = 0;
    public const int FOOD = 1;
}