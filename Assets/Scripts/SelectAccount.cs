using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAccount : MonoBehaviour
{
    public Text QQText;
    public void Touch()
    {
        string tokenName = QQText.text == PlayerPrefs.GetString("qq") ? "token" : "token_" + QQText.text;
        if (PlayerPrefs.GetString(tokenName) != "")
        {
            if (QQText.text == PlayerPrefs.GetString("qq"))
                PlayerPrefs.SetString("login_qq", "");
            else
                PlayerPrefs.SetString("login_qq", QQText.text);
            Network.QQ = long.Parse(QQText.text);
            Network.Send("dy :cache_verify " + QQText.text + " " + PlayerPrefs.GetString(tokenName));
            UIDispatcher.active.TipText.text = "已自动登录" + QQText.text + "，欢迎回来~";
            UIDispatcher.active.TipText.gameObject.SetActive(false);
            UIDispatcher.active.TipText.gameObject.SetActive(true);
        }
    }
}
