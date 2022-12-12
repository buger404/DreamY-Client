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
        UIDispatcher.active.TipText.text = "��" + (Value ? "����" : "�ر�") + "�Զ������ײ�����~";
        UIDispatcher.active.TipText.gameObject.SetActive(false);
        UIDispatcher.active.TipText.gameObject.SetActive(true);
    }
}
