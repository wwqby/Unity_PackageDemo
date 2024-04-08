using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotterCell : MonoBehaviour
{
    private Transform UI_Top_MarkNewIcon;
    private Transform UI_Center_WepeanIcon;
    private Transform UI_Bottom_Stars;

    private PackageTableItem TableItem;
    private PackageLocalItem LocalItem;
    private LotteryPanel UI_parent;


    private void Awake() {
        InitUIName();
    }

    private void InitUIName()
    {
        UI_Top_MarkNewIcon = transform.Find("Top/MarkNewIcon");
        UI_Center_WepeanIcon = transform.Find("Center/WepeanIcon");
        UI_Bottom_Stars = transform.Find("Bottom/Stars");
        UI_Top_MarkNewIcon.gameObject.SetActive(false);
    }


    public void Refresh(PackageLocalItem localItem, LotteryPanel parent){
        this.LocalItem = localItem;
        this.TableItem = GameManager.Instance.GetPacakageTableItemById(localItem.id);
        this.UI_parent = parent;
        RefreshImage();
        RefreshStars();
        RefreshMarkNew();
    }

    private void RefreshMarkNew()
    {
        List<PackageLocalItem> list = GameManager.Instance.GetPackageLocalDatas();
        bool isNew = true;
        foreach (PackageLocalItem item in list){
            if (item.id == LocalItem.id)
            {
                isNew = false;
                break;
            }
        }
        UI_Top_MarkNewIcon.gameObject.SetActive(isNew);
    }

    private void RefreshStars()
    {
        for (int i = 0; i < UI_Bottom_Stars.childCount; i++)
        {
            Transform transform = UI_Bottom_Stars.GetChild(i);
            transform.gameObject.SetActive(TableItem.star > i);
        }
    }

    private void RefreshImage()
    {
        Texture2D texture2D= Resources.Load<Texture2D>(TableItem.imagePath);
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0,0));
        UI_Center_WepeanIcon.GetComponent<Image>().sprite = sprite;
    }
}
