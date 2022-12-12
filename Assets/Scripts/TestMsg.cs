using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMsg : MonoBehaviour
{
    public Transform Container;
    [TextArea]
    public string content;
    public void Test()
    {
        MsgCreator.Create(1361778219, content, Container, this);
    }
}
