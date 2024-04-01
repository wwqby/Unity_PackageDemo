using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 游戏界面UI的父类
/// 所有的游戏UI界面都需要挂载本脚本或者继承实现
/// 每个游戏界面需要新建一个canvas组件，然后以canvas组件作为根节点添加界面内容
/// </summary>
public class BasePanel : MonoBehaviour
{
    //界面名称
    protected new string name;
    //界面是否移除
    protected bool isRemove;

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <param name="name">为界面名称赋值</param>
    public virtual void OpenPanel(string name)
    {
        this.name = name;
        gameObject.SetActive(true);
        //添加淡入效果,1s内透明度从0-》1
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 1f);
    }

    /// <summary>
    /// 关闭当前界面
    /// </summary>
    public virtual void ClosePanel()
    {
        //添加淡入效果,1s内透明度从1-》0
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0f, 1f);
        isRemove = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
