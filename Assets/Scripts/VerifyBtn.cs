using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifyBtn : MonoBehaviour
{
    public Text VerifyCode;
    int cnt;
    DateTime checkTime;
    public void Touch()
    {
        if (VerifyCode.text == "")
        {
            UIDispatcher.active.TipText.text = "������֤�룬С�ĺ���ҧ��Ŷ��";
            UIDispatcher.active.TipText.gameObject.SetActive(false);
            UIDispatcher.active.TipText.gameObject.SetActive(true);
            return;
        }
        if (cnt > 5)
        {
            UIDispatcher.active.TipText.text = "(�ң�ա�)�� �������̫����...";
            UIDispatcher.active.TipText.gameObject.SetActive(false);
            UIDispatcher.active.TipText.gameObject.SetActive(true);
            return;
        }
        cnt++;
        Network.Send("dy :verify " + VerifyCode.text);
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
