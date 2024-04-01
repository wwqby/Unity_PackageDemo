using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour
{
    private Transform UI_Icon;
    private Transform UI_New;
    private Transform UI_Head;
    private Transform UI_LevelText;
    private Transform UI_Stars;
    private Transform UI_Select;
    private Transform UI_DeleteSelect;

    private PacakageTableItem tableItem;
    private PackageLocalItem localData;
    private PackagePanel UI_parent;


    private void Awake()
    {
        InitUIName();
    }

    private void InitUIName()
    {
        UI_Icon = transform.Find("Top/Icon");
        UI_New = transform.Find("Top/New");
        UI_Head = transform.Find("Top/Head");
        UI_LevelText = transform.Find("Bottom/Bg/LevelText");
        UI_Stars = transform.Find("Bottom/Stars");
        UI_Select = transform.Find("Select");
        UI_DeleteSelect = transform.Find("DeleteSelect");

        UI_Select.gameObject.SetActive(false);
        UI_DeleteSelect.gameObject.SetActive(false);
    }

    public void RefreshUI(PackageLocalItem localData, PackagePanel parent)
    {
        //数据初始化
        this.localData = localData;
        this.UI_parent = parent;
        this.tableItem = GameManager.Instance.GetPacakageTableItemById(localData.id);

        //等级信息
        UI_LevelText.GetComponent<Text>().text = "LV." + this.localData.level;
        //是否新获得
        UI_New.gameObject.SetActive(localData.isNew);
        //物品图片
        Texture2D texture = Resources.Load<Texture2D>(tableItem.imagePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        UI_Icon.GetComponent<Image>().sprite = sprite;
        //刷新星级
        RefreshStars();
    }

    private void RefreshStars()
    {
        for (int i = 0; i < UI_Stars.childCount; i++)
        {
            Transform transform = UI_Stars.GetChild(i);
            transform.gameObject.SetActive(tableItem.star > i);
        }
    }
}
