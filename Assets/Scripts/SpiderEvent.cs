using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEvent : MonoBehaviour
{
    private Spider _spider;

    void Start()
    {
        _spider = GetComponentInParent<Spider>();
    }

    public void FireAcid()
    {
        _spider.Attack();
    }
}
