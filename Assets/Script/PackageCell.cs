using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform UI_Icon;
    private Transform UI_New;
    private Transform UI_Head;
    private Transform UI_LevelText;
    private Transform UI_Stars;
    private Transform UI_Select;
    private Transform UI_DeleteSelect;

    private Transform UI_SelectAnimation;
    private Transform UI_MouseOverAnimation;

    private PacakageTableItem tableItem;
    private PackageLocalItem localItem;
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
        UI_SelectAnimation = transform.Find("SelectAnimation");
        UI_MouseOverAnimation = transform.Find("MouseOverAnimation");

        UI_Select.gameObject.SetActive(false);
        UI_DeleteSelect.gameObject.SetActive(false);
        UI_SelectAnimation.gameObject.SetActive(false);
        UI_MouseOverAnimation.gameObject.SetActive(false);
    }

    public void RefreshUI(PackageLocalItem localData, PackagePanel parent)
    {
        //数据初始化
        this.localItem = localData;
        this.UI_parent = parent;
        this.tableItem = GameManager.Instance.GetPacakageTableItemById(localData.id);

        //等级信息
        UI_LevelText.GetComponent<Text>().text = "LV." + this.localItem.level;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick:" + eventData.ToString());
        if (UI_parent.ChooseUuid == this.localItem.uuid)
        {
            return;
        }
        //设置选中数据，触发更新UI
        UI_parent.ChooseUuid = this.localItem.uuid;
        UI_SelectAnimation.gameObject.SetActive(true);
        UI_SelectAnimation.GetComponent<Animator>().SetTrigger("In");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter:" + eventData.ToString());
        UI_MouseOverAnimation.gameObject.SetActive(true);
        UI_MouseOverAnimation.GetComponent<Animator>().SetTrigger("In");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit:" + eventData.ToString());
        UI_MouseOverAnimation.gameObject.SetActive(true);
        UI_MouseOverAnimation.GetComponent<Animator>().SetTrigger("Out");
    }
}
