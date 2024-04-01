using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统一的游戏UI管理类
/// </summary>
public class UIManager
{
    /// <summary>
    /// 单例对象
    /// </summary>
    private static UIManager _instance;
    /// <summary>
    /// 界面配置表
    /// </summary>
    private Dictionary<string, string> pathDicts;
    /// <summary>
    /// 预制体缓存表
    /// </summary>
    private Dictionary<string, GameObject> prefabDicts;
    /// <summary>
    /// 游戏界面缓存表
    /// </summary>
    private Dictionary<string, BasePanel> panelDicts;
    /// <summary>
    /// 根节点
    /// </summary>
    private Transform _uiRoot;

<<<<<<< HEAD
    public static UIManager Instance
=======
    public UIManager Instance
>>>>>>> af57e37d284d72f57dda333b7c112b5bcf15a792
    {
        get
        {
            _instance ??= new UIManager();
            return _instance;
        }
    }

<<<<<<< HEAD
=======
    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                if (GameObject.Find("Canas"))
                {
                    _uiRoot = GameObject.Find("Canas").transform;
                }
                else
                {
                    _uiRoot = new GameObject("Canvas").transform;
                }
            }
            return _uiRoot;
        }
    }
>>>>>>> af57e37d284d72f57dda333b7c112b5bcf15a792

    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        pathDicts = new Dictionary<string, string>(){
<<<<<<< HEAD
            {UIConst.PackagePanael,"Package/PackagePanel"},
=======
            {UIConst.PackagePanael,"Assets/Resources/Prefab/Panel/Package/PackagePanel.prefab"},
>>>>>>> af57e37d284d72f57dda333b7c112b5bcf15a792
        };
        prefabDicts = new Dictionary<string, GameObject>();
        panelDicts = new Dictionary<string, BasePanel>();
    }

    /// <summary>
    /// 打开游戏界面
    /// 首先尝试从缓存中获取,没有缓存再从预制体中获取
    /// </summary>
    /// <param name="name">UIConst 配置表中的name</param>
    /// <returns></returns>
    public BasePanel OpenPanel(string name)
    {
        //检查是否已经打开
        if (panelDicts.TryGetValue(name, out BasePanel panel))
        {
            Debug.LogError("界面已打开:[name]=" + name);
            return null;
        }
        //检查是否有在配置表中
        string path = "";
        if (!pathDicts.TryGetValue(name, out path))
        {
            Debug.LogError("配置表中未查到页面:" + name);
            return null;
        }
        //预制体加载
        GameObject prefab = null;
        if (!prefabDicts.TryGetValue(name, out prefab))
        {
            string realPath = "Prefab/Panel/" + path;
            prefab = Resources.Load<GameObject>(realPath);
            prefabDicts.Add(name, prefab);
        }
        //添加Panel到uiRoot的子节点
        GameObject gameObject = GameObject.Instantiate(prefab);
        //缓存当前页面
        panel = gameObject.GetComponent<BasePanel>();
        panelDicts.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }


    /// <summary>
    /// 根据name关闭界面
    /// </summary>
    /// <param name="name">指定操作界面的name</param>
    /// <returns>true 关闭成功</returns>
    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDicts.TryGetValue(name, out panel))
        {
            Debug.LogError("界面未打开:[name]=" + name);
            return false;
        }
        //移除缓存
        if (panelDicts.ContainsKey(name))
        {
            panelDicts.Remove(name);
        }
        panel.ClosePanel();
        return true;
    }
}

/// <summary>
/// UI界面配置表的key
/// </summary>
public class UIConst
{
    //背包界面
    public const string PackagePanael = "PackagePanel";

}
