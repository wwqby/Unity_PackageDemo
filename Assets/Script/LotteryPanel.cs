using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LotteryPanel : BasePanel
{
    private Transform UI_Top_CloseBtn;
    private Transform UI_Center;
    private Transform UI_Bottom_OnceBtn;
    private Transform UI_Bottom_TenceBtn;

    private GameObject Prefab_LotteryItem;
    private void Start()
    {
    }
    protected override void Awake()
    {
        InitUIName();
        InitPrefab();
    }

    private void InitPrefab()
    {
        Prefab_LotteryItem = Resources.Load<GameObject>("Prefab/Panel/Lottery/LotteryItem");
    }

    private void InitUIName()
    {
        UI_Top_CloseBtn = transform.Find("Top/CloseBtn");
        UI_Center = transform.Find("Center");
        UI_Bottom_OnceBtn = transform.Find("Bottom/OnceBtn");
        UI_Bottom_TenceBtn = transform.Find("Bottom/TenceBtn");
        UI_Bottom_OnceBtn.GetComponent<Button>().onClick.AddListener(OnClickLotteryOnce);
        UI_Bottom_TenceBtn.GetComponent<Button>().onClick.AddListener(OnClickLotteryTence);
        UI_Top_CloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()
    {
        Debug.Log(">>>>> OnClickClose");
        UIManager.Instance.OpenPanel(UIConst.MainPanel);
        UIManager.Instance.ClosePanel(UIConst.LotteryPanel);
    }

    private void OnClickLotteryTence()
    {
        Debug.Log(">>>>> OnClickLotteryTence");
        //销毁上一次抽取结果
        for (int i = 0; i < UI_Center.childCount; i++)
        {
            Transform transform = UI_Center.GetChild(i);
            Destroy(transform.gameObject);
        }
        //抽卡十次
        List<PackageLocalItem> list = GameManager.Instance.getLotteryWepeanTence(true);
        foreach (PackageLocalItem item in list)
        {
            LotterCell lotterCell = Instantiate(Prefab_LotteryItem, UI_Center).GetComponent<LotterCell>();
            lotterCell.Refresh(item, this);
        }
    }

    private void OnClickLotteryOnce()
    {
        Debug.Log(">>>>> OnClickLotteryOnce");
         //销毁上一次抽取结果
        for (int i = 0; i < UI_Center.childCount; i++)
        {
            Transform transform = UI_Center.GetChild(i);
            Destroy(transform.gameObject);
        }
        //抽卡一次
        PackageLocalItem localItem = GameManager.Instance.getLotteryRandomOnce();
        LotterCell lotterCell = Instantiate(Prefab_LotteryItem, UI_Center).GetComponent<LotterCell>();
        lotterCell.Refresh(localItem, this);
    }
}
