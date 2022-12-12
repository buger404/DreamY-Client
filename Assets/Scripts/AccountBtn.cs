using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccountBtn : MonoBehaviour
{
    public Transform Container;
    public GameObject AccountPanel;
    public void Touch()
    {
        for(int i = 0; i < Container.childCount; i++)
        {
            if (Container.GetChild(i).gameObject.activeSelf)
                Destroy(Container.GetChild(i).gameObject);
        }
        GameObject prefab = Container.GetChild(0).gameObject;
        List<string> qq = PlayerPrefs.GetString("accounts").Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
        qq.Add(PlayerPrefs.GetString("qq"));
        for(int i = 0; i < qq.Count; i++)
        {
            GameObject acc = Instantiate(prefab, Container);
            acc.SetActive(true);
            acc.transform.Find("QQ").GetComponent<Text>().text = qq[i].ToString();
            if (MsgCreator.Face.ContainsKey(long.Parse(qq[i])))
                acc.transform.Find("Mask").Find("Face").GetComponent<Image>().sprite = MsgCreator.Face[long.Parse(qq[i])];
        }
        AccountPanel.SetActive(true);
        Network.Logined = false;
    }
}
