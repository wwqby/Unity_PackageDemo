using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    private Transform UI_Top_PackageIconInfo;
    private Transform UI_Top_MenuWepean;
    private Transform UI_Top_MenuFood;
    private Transform UI_Top_PackageInfo;
    private Transform UI_Top_CloseBtn;
    private Transform UI_Center_ScrollView;
    private Transform UI_Center_DetailPanel;
    private Transform UI_Center_PreviousBtn;
    private Transform UI_Center_NextBtn;
    private Transform UI_Bottom_DeleteBtn;
    private Transform UI_Bottom_DetailBtn;
    private Transform UI_Bottom_DeleteBackBtn;
    private Transform UI_Bottom_InfoText;
    private Transform UI_Bottom_ConfirmText;
    private Transform UI_Bottom_BottomMenus;
    private Transform UI_Bottom_DeletePanel;


    public GameObject PackagePanelItemPrefab;
    //当前界面工作模式
    public int curMode;
    //多选时选中物品的uuid记录
    public List<string> DeleteChoosenUuids;

    //当前查看的物品uuid
    private string _chooseUuid;

    public string ChooseUuid
    {
        get
        {
            return _chooseUuid;
        }
        set
        {
            this._chooseUuid = value;
            RefreshDetail();
        }
    }

    private void RefreshDetail()
    {
        PackageLocalItem localItem = GameManager.Instance.GetPackageLocalItemByUuid(ChooseUuid);
        UI_Center_DetailPanel.GetComponent<PackageDetail>().Refresh(localItem, this);
    }

    override protected void Awake()
    {
        base.Awake();
        InitUIName();
        InitClick();
    }

    private void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        RefreshScrollContent();
    }

    private void RefreshScrollContent()
    {
        //删除content中原有GameObject
        RectTransform content = UI_Center_ScrollView.GetComponent<ScrollRect>().content;
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        //重新添加GameObject
        foreach (PackageLocalItem item in GameManager.Instance.getSortedPackageLocalData())
        {
            Transform transform = Instantiate<Transform>(PackagePanelItemPrefab.transform, content);
            PackageCell packageCell = transform.GetComponent<PackageCell>();
            packageCell.RefreshUI(item, this);
        }
    }

    private void InitUIName()
    {
        UI_Top_PackageIconInfo = transform.Find("Top/PackageIcon/Info");
        UI_Top_MenuWepean = transform.Find("Top/Menus/Wepean");
        UI_Top_MenuFood = transform.Find("Top/Menus/Food");
        UI_Top_CloseBtn = transform.Find("Top/CloseBtn");
        UI_Top_PackageInfo = transform.Find("Top/BackBtn/Info");
        UI_Center_ScrollView = transform.Find("Center/ScrollView");
        UI_Center_DetailPanel = transform.Find("Center/DetailPanel");
        UI_Center_PreviousBtn = transform.Find("Center/PreviousBtn");
        UI_Center_NextBtn = transform.Find("Center/NextBtn");
        UI_Bottom_DeleteBtn = transform.Find("Bottom/BottomMenus/DeleteBtn");
        UI_Bottom_DetailBtn = transform.Find("Bottom/BottomMenus/DetailBtn");
        UI_Bottom_DeleteBackBtn = transform.Find("Bottom/DeletePanel/BackBtn");
        UI_Bottom_InfoText = transform.Find("Bottom/DeletePanel/InfoIcon/InfoText");
        UI_Bottom_ConfirmText = transform.Find("Bottom/DeletePanel/ConfirmBtn");
        UI_Bottom_BottomMenus = transform.Find("Bottom/BottomMenus");
        UI_Bottom_DeletePanel = transform.Find("Bottom/DeletePanel");

        UI_Bottom_BottomMenus.gameObject.SetActive(true);
        UI_Bottom_DeletePanel.gameObject.SetActive(false);
    }


    private void InitClick()
    {
        UI_Top_MenuWepean.GetComponent<Button>().onClick.AddListener(OnClickWepean);
        UI_Top_MenuFood.GetComponent<Button>().onClick.AddListener(OnClickFood);
        UI_Top_CloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        UI_Center_PreviousBtn.GetComponent<Button>().onClick.AddListener(OnClickPrecious);
        UI_Center_NextBtn.GetComponent<Button>().onClick.AddListener(OnClickNext);
        UI_Bottom_DeleteBtn.GetComponent<Button>().onClick.AddListener(OnClickDelete);
        UI_Bottom_ConfirmText.GetComponent<Button>().onClick.AddListener(OnClickDeleteConfirm);
        UI_Bottom_DeleteBackBtn.GetComponent<Button>().onClick.AddListener(OnClickDeleteBack);
        UI_Bottom_DetailBtn.GetComponent<Button>().onClick.AddListener(OnClickDetail);
    }

    private void OnClickDetail()
    {
        print(">>>>>OnClickDetail");
    }

    private void OnClickDeleteBack()
    {
        print(">>>>>OnClickDeleteBack");
        curMode = PackageMode.NORMAL;
        UI_Bottom_DeletePanel.gameObject.SetActive(false);
        UI_Bottom_BottomMenus.gameObject.SetActive(true);
        DeleteChoosenUuids = new List<string>();
        RefreshDeletePanel();
    }

    private void OnClickDeleteConfirm()
    {
        print(">>>>>OnClickDeleteConfirm");
        curMode = PackageMode.NORMAL;
        UI_Bottom_DeletePanel.gameObject.SetActive(false);
        UI_Bottom_BottomMenus.gameObject.SetActive(true);
        //判断是否需要删除操作
        if(DeleteChoosenUuids==null||DeleteChoosenUuids.Count==0){
            return;
        }
        //删除数据
        GameManager.Instance.DeletePackageItems(DeleteChoosenUuids);
        //刷新UI
        RefreshUI();
    }

    private void OnClickDelete()
    {
        print(">>>>>OnClickDelete");
        curMode = PackageMode.DELETE;
        UI_Bottom_DeletePanel.gameObject.SetActive(true);
        UI_Bottom_BottomMenus.gameObject.SetActive(false);
    }

    private void OnClickNext()
    {
        print(">>>>>OnClickNext");
    }

    private void OnClickPrecious()
    {
        print(">>>>>OnClickPrecious");
    }

    private void OnClickClose()
    {
        print(">>>>>OnClickClose");
        UIManager.Instance.OpenPanel(UIConst.MainPanel);
        UIManager.Instance.ClosePanel(UIConst.PackagePanel);
    }

    private void OnClickFood()
    {
        print(">>>>>OnClickFood");
    }

    private void OnClickWepean()
    {
        print(">>>>>OnClickWepean");
    }


    /// <summary>
    /// 添加和取消删除的物品uuid
    /// </summary>
    /// <param name="uuid">选中的物品uuid</param>
    public void AddChooseDeleteUuid(string uuid)
    {
        DeleteChoosenUuids ??= new List<string>();
        if (DeleteChoosenUuids.Contains(uuid))
        {
            DeleteChoosenUuids.Remove(uuid);
            return;
        }
        DeleteChoosenUuids.Add(uuid);
    } 


    public void RefreshDeletePanel(){
        RectTransform rect = UI_Center_ScrollView.GetComponent<ScrollRect>().content;
        foreach(Transform itemTransform in rect){
            PackageCell cell = itemTransform.GetComponent<PackageCell>();
            // 刷新删除的选中状态
            cell.RefreshDeleteState();
        }
    }
}

/// <summary>
/// 背包界面的状态
/// </summary>
class PackageMode{
    //正常模式
    public const int NORMAL = 0;
    //删除模式
    public const int DELETE = 1;
}