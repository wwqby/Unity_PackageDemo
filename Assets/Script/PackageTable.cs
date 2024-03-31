using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="wwq/背包数据",fileName ="PackageTable")]
public class PackageTable : ScriptableObject
{
    public List<PacakageTableItem> dataList = new List<PacakageTableItem>();
}

[System.Serializable]
public class PacakageTableItem{
    public int id;
    public int type;
    public int star;
    public string name;
    public string description;
    public string skillDescription;
    public string imagePath;
}
