using System;
using System.Collections;
using System.Collections.Generic;
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


    private string _chooseUuid;

    public string ChooseUuid{
        get{
            return _chooseUuid;
        }
        set{
            this._chooseUuid = value;
            RefreshDetail();
        }
    }

    private void RefreshDetail()
    {
        PackageLocalItem localItem = GameManager.Instance.GetPackageLocalItemByUuid(ChooseUuid);
        UI_Center_DetailPanel.GetComponent<PackageDetail>().Refresh(localItem,this);
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
            Transform transform=Instantiate<Transform>(PackagePanelItemPrefab.transform, content);
            PackageCell packageCell =transform.GetComponent<PackageCell>();
            packageCell.RefreshUI(item,this);
        }
    }

    private void InitUIName()
    {
        Transform panel = transform.Find("PackageMainPanel");
        UI_Top_PackageIconInfo = panel.Find("Top/PackageIcon/Info");
        UI_Top_MenuWepean = panel.Find("Top/Menus/Wepean");
        UI_Top_MenuFood = panel.Find("Top/Menus/Food");
        UI_Top_CloseBtn = panel.Find("Top/CloseBtn");
        UI_Top_PackageInfo = panel.Find("Top/BackBtn/Info");
        UI_Center_ScrollView = panel.Find("Center/ScrollView");
        UI_Center_DetailPanel = panel.Find("Center/DetailPanel");
        UI_Center_PreviousBtn = panel.Find("Center/PreviousBtn");
        UI_Center_NextBtn = panel.Find("Center/NextBtn");
        UI_Bottom_DeleteBtn = panel.Find("Bottom/BottomMenus/DeleteBtn");
        UI_Bottom_DetailBtn = panel.Find("Bottom/BottomMenus/DetailBtn");
        UI_Bottom_DeleteBackBtn = panel.Find("Bottom/DeletePanel/BackBtn");
        UI_Bottom_InfoText = panel.Find("Bottom/DeletePanel/InfoIcon/InfoText");
        UI_Bottom_ConfirmText = panel.Find("Bottom/DeletePanel/ConfirmBtn");
        UI_Bottom_BottomMenus = panel.Find("Bottom/BottomMenus");
        UI_Bottom_DeletePanel = panel.Find("Bottom/DeletePanel");

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
    }

    private void OnClickDeleteConfirm()
    {
        print(">>>>>OnClickDeleteConfirm");
    }

    private void OnClickDelete()
    {
        print(">>>>>OnClickDelete");
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
        ClosePanel();
    }

    private void OnClickFood()
    {
        print(">>>>>OnClickFood");
    }

    private void OnClickWepean()
    {
        print(">>>>>OnClickWepean");
    }
}
