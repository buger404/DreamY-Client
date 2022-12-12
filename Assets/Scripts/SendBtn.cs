using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendBtn : MonoBehaviour
{
    TMP_InputField SendField;
    public GameObject SendBox;
    int cnt;
    DateTime checkTime;
    private void Awake()
    {
        SendField = SendBox.GetComponent<TMP_InputField>();
    }
    public void Touch()
    {
        if (SendField.text == "")
        {
            UIDispatcher.active.TipText.text = "(ノへ￣) 不许发空消息！";
            UIDispatcher.active.TipText.gameObject.SetActive(false);
            UIDispatcher.active.TipText.gameObject.SetActive(true);
            return;
        }
        if (cnt > 5)
        {
            UIDispatcher.active.TipText.text = "(σ｀д′)σ 你操作得太快了...";
            UIDispatcher.active.TipText.gameObject.SetActive(false);
            UIDispatcher.active.TipText.gameObject.SetActive(true);
            return;
        }
        MsgCreator.Create(Network.QQ, SendField.text, UIDispatcher.active.Container, this);
        MsgCreator.newMsg++;
        //SndPlayer.Play("menuback");
        cnt++;
        Network.Send(SendField.text);
        SendField.text = "";
    }
    private void Update()
    {
        if ((DateTime.Now - checkTime).TotalSeconds > 5)
        {
            checkTime = DateTime.Now;
            cnt = 0;
        }
    }
}
