using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 定位卡扣
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class LimitBuckle : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// 是否已放置于接口
    /// </summary>
    public bool inPlaced = false;

    public bool inRelease = false;

    public bool is_screw_off;

    public TopCablePort Port;

    public override void Start()
    {
        base.Start();
        Port = GetComponentInParent<TopCablePort>();
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
        TopCablePort port = other.GetComponent<TopCablePort>();
        if (port == null || !port.inUsed) return;
        Port = port;
        Port.Buckle = this;

        inPlaced = true;
        transform.SetParent(port.buckle_place);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    void InFree()
    {
        inRelease = false;
    }
}
