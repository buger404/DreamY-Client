using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音效播放器
/// </summary>
public class SndPlayer : MonoBehaviour
{
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="snd">音效文件名</param>
    public static void Play(string snd)
        => Play(Resources.Load<AudioClip>("SE\\" + snd));
    public static void Play(AudioClip snd)
    {
        GameObject fab = (GameObject)Resources.Load("SndPlayer");    // 载入母体
        GameObject box = Instantiate(fab, new Vector3(0, 0, -1), Quaternion.identity);
        AudioSource a = box.GetComponent<AudioSource>();
        a.clip = snd;
        box.SetActive(true);
        a.Play();
        DontDestroyOnLoad(box);
        Destroy(box, a.clip.length);
    }
}
