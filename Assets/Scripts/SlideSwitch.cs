using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideSwitch : MonoBehaviour
{
    public static bool Value = true;
    public Sprite OnSprite, OffSprite;
    public void Touch()
    {
        Value = !Value;
        GetComponent<Image>().sprite = Value ? OnSprite : OffSprite;
        UIDispatcher.active.TipText.text = "已" + (Value ? "开启" : "关闭") + "自动滑至底部功能~";
        UIDispatcher.active.TipText.gameObject.SetActive(false);
        UIDispatcher.active.TipText.gameObject.SetActive(true);
    }
}
