using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MsgCreator : MonoBehaviour
{
    public static Dictionary<long, Sprite> Face = new Dictionary<long, Sprite>();
    public static GameObject Prefab = null;
    public static int newMsg = 0;
    static IEnumerator DownloadQQFace(long qq, string content, Transform container)
    {
        UnityWebRequest request = UnityWebRequest.Get("http://q.qlogo.cn/headimg_dl?dst_uin=" + qq + "&spec=640");
        yield return request.SendWebRequest();
        File.WriteAllBytes(Application.persistentDataPath + "\\" + qq + ".jpg", request.downloadHandler.data);
        Texture2D face = new Texture2D(640, 640);
        face.LoadImage(request.downloadHandler.data);
        if (!Face.ContainsKey(qq))
            Face.Add(qq, Sprite.Create(face, new Rect(0, 0, face.width, face.height), new Vector2(0, 0)));
        InnerCreate(qq, content, container);
        yield break;
    }
    public static void Create(long qq, string content, Transform container, MonoBehaviour mono)
    {
        if (Prefab == null)
            Prefab = Resources.Load<GameObject>("MsgPrefab");
        if (!Face.ContainsKey(qq))
        {
            if (!File.Exists(Application.persistentDataPath + "\\" + qq + ".jpg"))
            {
                mono.StartCoroutine(DownloadQQFace(qq, content, container));
                return;
            }
            else
            {
                Texture2D face = new Texture2D(640, 640);
                face.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "\\" + qq + ".jpg"));
                Face.Add(qq, Sprite.Create(face, new Rect(0, 0, face.width, face.height), new Vector2(0, 0)));
            }
        }
        InnerCreate(qq, content, container);
    }
    private static void InnerCreate(long qq, string content, Transform container)
    {
        if (!Network.Logined) return;
        GameObject msg = GameObject.Instantiate(Prefab, container);
        msg.transform.Find("Msg").GetComponent<TextMeshProUGUI>().SetText(content);
        msg.transform.Find("QQ").GetComponent<Text>().text = qq.ToString();
        msg.transform.Find("Mask").Find("Face").GetComponent<Image>().sprite = Face[qq];
        RectTransform pannel = msg.transform.Find("Panel").GetComponent<RectTransform>();
        RectTransform text = msg.transform.Find("Msg").GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(text);
        if (text.sizeDelta.x > 1950)
        {
            text.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            text.sizeDelta = new Vector2(1950, text.sizeDelta.y);
            LayoutRebuilder.ForceRebuildLayoutImmediate(text);
        }
        pannel.sizeDelta = (text.sizeDelta * 0.6f) + new Vector2(260, 230);
        RectTransform rect = msg.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, pannel.sizeDelta.y + 150);
        msg.SetActive(true);
        newMsg += 1000;
        //Debug.Log("before:" + container.parent.parent.GetComponent<ScrollRect>().normalizedPosition.y);
        //Debug.Log("after:" + container.parent.parent.GetComponent<ScrollRect>().normalizedPosition.y);
    }
}
