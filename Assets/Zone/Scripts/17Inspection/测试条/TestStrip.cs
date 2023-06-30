using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 测试条
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class TestStrip : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// 是否已放置于接口
    /// </summary>
    public bool inPlaced = false;

    public bool inRelease = false;

    public override void Start()
    {
        base.Start();
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("拿在手中");
        inHand = true;
        inPlaced = false;
    }

    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        Debug.Log("离开手里");
        inHand = false;
        inRelease = true;
        Invoke("InFree", 0.2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!inRelease) return;
        if (inPlaced) return;
        StripPort port = other.GetComponent<StripPort>();
        if (port == null) return;

        inPlaced = true;
        transform.SetParent(port.strip_place);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    void InFree()
    {
        inRelease = false;
    }
}
