using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ч������
/// </summary>
public class SndPlayer : MonoBehaviour
{
    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="snd">��Ч�ļ���</param>
    public static void Play(string snd)
        => Play(Resources.Load<AudioClip>("SE\\" + snd));
    public static void Play(AudioClip snd)
    {
        GameObject fab = (GameObject)Resources.Load("SndPlayer");    // ����ĸ��
        GameObject box = Instantiate(fab, new Vector3(0, 0, -1), Quaternion.identity);
        AudioSource a = box.GetComponent<AudioSource>();
        a.clip = snd;
        box.SetActive(true);
        a.Play();
        DontDestroyOnLoad(box);
        Destroy(box, a.clip.length);
    }
}
