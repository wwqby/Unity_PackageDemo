using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    private Transform UI_Top_TextTitle;
    private Transform UI_Center_Stars;
    private Transform UI_Center_TextDes;
    private Transform UI_Center_ImageIcon;
    private Transform UI_Bottom_TextSkillDes;
    private Transform UI_Bottom_TextLevel;

    private PackageLocalItem packageLocalItem;
    private PacakageTableItem pacakageTableItem;
    private PackagePanel uiParent;


    private void Awake() {
        InitUIName();
        //设置默认数据
        PackageLocalItem item = GameManager.Instance.getSortedPackageLocalData()[0];
        Refresh(item,null);
        
    }

   

    private void InitUIName()
    {
        UI_Top_TextTitle = transform.Find("Top/TextTitle");
        UI_Center_Stars = transform.Find("Center/Stars");
        UI_Center_TextDes = transform.Find("Center/TextDes");
        UI_Center_ImageIcon = transform.Find("Center/ImageIcon");
        UI_Bottom_TextSkillDes = transform.Find("Bottom/TextSkillDes");
        UI_Bottom_TextLevel = transform.Find("Bottom/Level/Text");

    }

    public void Refresh(PackageLocalItem localItem,PackagePanel parent){
        this.packageLocalItem = localItem;
        this.pacakageTableItem = GameManager.Instance.GetPacakageTableItemById(localItem.id);
        this.uiParent = parent;
        UI_Top_TextTitle.GetComponent<Text>().text = pacakageTableItem.name;
        UI_Center_TextDes.GetComponent<Text>().text = pacakageTableItem.description;
        UI_Bottom_TextSkillDes.GetComponent<Text>().text = pacakageTableItem.skillDescription;
        UI_Bottom_TextLevel.GetComponent<Text>().text = String.Format("LV.{0}/40",localItem.level);
        //图标
        Texture2D texture = Resources.Load<Texture2D>(pacakageTableItem.imagePath);
        Sprite sprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0,0));
        UI_Center_ImageIcon.GetComponent<Image>().sprite = sprite;
        //星级
        RefreshStars();
    }


    private void RefreshStars()
    {
        for (int i = 0; i < UI_Center_Stars.childCount; i++)
        {
            Transform transform = UI_Center_Stars.GetChild(i);
            transform.gameObject.SetActive(pacakageTableItem.star > i);
        }
    }
}
