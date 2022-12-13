using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{
    public const string splitStr = "呄";
    public const string splitStr2 = "嘦";
    static TcpClient client;
    public static bool Logined = false;
    public static long QQ = 10000;
    public TextAsset Server;
    public static long Fail = 0;

    private void Start()
    {
        Debug.Log("qq:" + PlayerPrefs.GetString("qq") + "\nlogin qq:" + PlayerPrefs.GetString("login_qq"));
        Debug.Log("token:" + PlayerPrefs.GetString("token") + "\naccounts:" + PlayerPrefs.GetString("accounts"));
        try
        {
            client = new TcpClient();
            client.Connect(Server.text, 1789);
            client.ReceiveBufferSize = 1024000;
            client.SendBufferSize = 1024000;
            new Thread(Receiving).Start();
        }
        catch(Exception err)
        {
            UIDispatcher.Register(() =>
            {
                UIDispatcher.active.TipText.text = err.Message;
                UIDispatcher.active.TipText.gameObject.SetActive(false);
                UIDispatcher.active.TipText.gameObject.SetActive(true);
                UIDispatcher.active.LoginBtn.SetActive(false);
            });

        }
    }

    public static void Receiving()
    {
        NetworkStream stream = client.GetStream();
        Send("dy :version 1.4");
        UIDispatcher.Register(() =>
        {
            if (PlayerPrefs.GetString("token") != "")
            {
                if (PlayerPrefs.GetString("login_qq") != "")
                {
                    QQ = long.Parse(PlayerPrefs.GetString("login_qq"));
                    Send("dy :cache_verify " + QQ + " " + PlayerPrefs.GetString("token_" + QQ));
                }
                else
                {
                    QQ = long.Parse(PlayerPrefs.GetString("qq"));
                    Send("dy :cache_verify " + QQ + " " + PlayerPrefs.GetString("token"));
                }
                UIDispatcher.active.TipText.text = "已自动登录" + QQ + "，欢迎回来~";
                UIDispatcher.active.TipText.gameObject.SetActive(false);
                UIDispatcher.active.TipText.gameObject.SetActive(true);
            }
        });
        while (client.Connected)
        {
            byte[] buffer = new byte[1024000];
            try
            {
                if (stream.Read(buffer, 0, 1024000) > 0)
                foreach (string s in Encoding.UTF8.GetString(buffer).Split(new string[] { splitStr }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] p = s.Split(new string[] { splitStr2 }, StringSplitOptions.RemoveEmptyEntries);
                    if (p.Length < 2) continue;
                    Console.WriteLine("发自" + p[0] + "：" + p[1]);
                    if (p[1].StartsWith("dy :success_authorization") && p[0] == "2487411076")
                    {
                        string[] t = p[1].Split(' ');
                        UIDispatcher.Register(() =>
                        {
                            Logined = true;
                            PlayerPrefs.SetString("token" + (QQ.ToString() != PlayerPrefs.GetString("qq") ? "_" + QQ : ""), t[2]);
                            if (!PlayerPrefs.GetString("accounts").Contains(QQ + ";") && QQ.ToString() != PlayerPrefs.GetString("qq"))
                                PlayerPrefs.SetString("accounts", PlayerPrefs.GetString("accounts") + QQ + ";");
                            UIDispatcher.active.ChatPanel.SetActive(true);
                            PlayerPrefs.Save();
                        });
                    }
                    else if (p[1] == "dy :failure_authorization" && p[0] == "2487411076")
                    {
                        UIDispatcher.Register(() =>
                        {
                            PlayerPrefs.SetString("token" + (QQ.ToString() != PlayerPrefs.GetString("qq") ? "_" + QQ : ""), "");
                            PlayerPrefs.SetString("accounts", PlayerPrefs.GetString("accounts").Replace(QQ + ";", ""));
                            UIDispatcher.active.TipText.text = "验证失败QWQ！！！";
                            UIDispatcher.active.TipText.gameObject.SetActive(false);
                            UIDispatcher.active.TipText.gameObject.SetActive(true);
                            PlayerPrefs.Save();
                        });
                    }
                    else if (p[1].StartsWith("dy :people ") && p[0] == "2487411076")
                    {
                        UIDispatcher.Register(() =>
                        {
                            UIDispatcher.active.PeopleText.text = "DreamY - " + p[1].Split(' ')[2] + "人在线";
                        });
                    }
                    else
                    {
                        UIDispatcher.Register(() =>
                        {
                            MsgCreator.Create(long.Parse(p[0]), p[1], UIDispatcher.active.Container, UIDispatcher.active);
                        });
                    }
                }
            }
            catch
            {
                break;
            }
        }
        client.Close();
        UIDispatcher.Register(() =>
        {
            Fail++;
            if (Fail > 3)
                Application.Quit();
            else
                SceneManager.LoadScene("Startup", LoadSceneMode.Single);
        });
    }

    public static void Send(string s)
    {
        try
        {
            byte[] wr = Encoding.UTF8.GetBytes(QQ + splitStr2 + s + splitStr);
            client.GetStream().Write(wr, 0, wr.Length);
        }catch(Exception err)
        {
            UIDispatcher.Register(() =>
            {
                UIDispatcher.active.TipText.text = "网络异常！！！";
                UIDispatcher.active.TipText.gameObject.SetActive(false);
                UIDispatcher.active.TipText.gameObject.SetActive(true);
            });
        }
    }
}
