using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private Transform UI_Center_PackageBtn;
    private Transform UI_Center_LotteryBtn;
    private Transform UI_Bottom_ExitBtn;

    protected override void Awake() {
        InitUIName();
    }

    private void InitUIName()
    {
       UI_Center_PackageBtn = transform.Find("Center/PackageBtn");
       UI_Center_LotteryBtn = transform.Find("Center/LotteryBtn");
       UI_Bottom_ExitBtn = transform.Find("Bottom/ExitBtn");

       UI_Center_PackageBtn.GetComponent<Button>().onClick.AddListener(OnClickPackageBtn);
       UI_Center_LotteryBtn.GetComponent<Button>().onClick.AddListener(OnClickLotteryBtn);
       UI_Bottom_ExitBtn.GetComponent<Button>().onClick.AddListener(OnClickExitBtn);
    }

    private void OnClickExitBtn()
    {
        Debug.Log(">>>>> OnClickExitBtn");
        ClosePanel();
    }

    private void OnClickLotteryBtn()
    {
        Debug.Log(">>>>> OnClickLotteryBtn");
        UIManager.Instance.OpenPanel(UIConst.LotteryPanel);
        UIManager.Instance.ClosePanel(UIConst.MainPanel);
    }

    private void OnClickPackageBtn()
    {
        Debug.Log(">>>>> OnClickPackageBtn");
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
        UIManager.Instance.ClosePanel(UIConst.MainPanel);
    }
}
