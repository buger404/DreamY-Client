using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �����̵߳�UI���߳��¼��ַ���
/// </summary>
public class UIDispatcher : MonoBehaviour
{
    public Text TipText, PeopleText;
    public Transform Container;
    public GameObject ChatPanel;
    public GameObject LoginBtn;
    public static UIDispatcher active;
    public static List<Action> actions = new List<Action>();
    /// <summary>
    /// �ַ��µĺ�������
    /// </summary>
    /// <param name="action">��������</param>
    public static void Register(Action action)
    {
        if (active == null) return;
        lock (actions)
        {
            actions.Add(action);
        }
    }
    private void Awake()
    {
        active = this;
        Application.targetFrameRate = 60;
    }
    private void OnDestroy()
    {
        Update();
        active = null;
    }
    private void Update()
    {
        if (MsgCreator.newMsg > 0)
        {
            if (SlideSwitch.Value)
                Container.parent.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
            MsgCreator.newMsg--;
        }
        // ˳���õ������Ч�ģ���
        //if (Input.GetMouseButtonUp(0)) SndPlayer.Play("menuhit");
        lock (actions)
        {
            if (actions.Count == 0) return;
            int i = 0;
            while (i < actions.Count)
            {
                try
                {
                    actions[i]();
                }
                catch(Exception err)
                {
                    Debug.LogError(err.Message + "\n" + err.StackTrace);
                }
                i++;
            }
            actions.Clear();
        }
    }
}
