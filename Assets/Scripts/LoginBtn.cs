using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBtn : MonoBehaviour
{
    public GameObject VerifyPanel;
    public Text QQText;
    bool clicked = false;
    public void Touch()
    {
        if (QQText.text == "")
        {
            UIDispatcher.active.TipText.text = "ÌîÒ»ÏÂQQºÅQWQ£¡£¡£¡";
            UIDispatcher.active.TipText.gameObject.SetActive(false);
            UIDispatcher.active.TipText.gameObject.SetActive(true);
            return;
        }
        Debug.Log("qq:" + PlayerPrefs.GetString("qq") + "\nlogin qq:" + PlayerPrefs.GetString("login_qq"));
        Debug.Log("token:" + PlayerPrefs.GetString("token") + "\naccounts:" + PlayerPrefs.GetString("accounts"));
        if (PlayerPrefs.GetString("qq") != QQText.text && PlayerPrefs.GetString("qq") != "")
        {
            PlayerPrefs.SetString("login_qq", QQText.text);
        }
        else
        {
            PlayerPrefs.SetString("qq", QQText.text);
            PlayerPrefs.SetString("login_qq", "");
        }
        Network.QQ = long.Parse(QQText.text);
        Network.Send("dy :sendauthorization " + QQText.text);
        VerifyPanel.SetActive(true);
    }
}
