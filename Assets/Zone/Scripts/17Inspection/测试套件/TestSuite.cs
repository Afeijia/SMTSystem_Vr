using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// 测试套件
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class TestSuite : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// 是否已放置于接口
    /// </summary>
    public bool inPlaced = false;

    public bool inRelease = false;

    public bool is_open;

    public Transform a, b;

    public TestSuitePort Port;

    public SteamVR_Action_Boolean interact_btn;

    public override void Start()
    {
        base.Start();
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        if (interact_btn.GetStateDown(hand.handType))
        {
            if (is_open)
            {
                CloseSuite();
            }
            else
            {
                OpenSuite();
            }

            if (inPlaced)
            {
                SetInterable(is_open);
            }
        }
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
        if (!is_open) return;
        TestSuitePort port = other.GetComponent<TestSuitePort>();
        if (port == null || port.inUsed) return;
        Port = port;
        Port.Suite = this;

        inPlaced = true;
        transform.SetParent(port.suite_place);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    void InFree()
    {
        inRelease = false;
    }

    public void OpenSuite()
    {
        a.localEulerAngles = new Vector3(-25, 0, 0);
        b.localEulerAngles = new Vector3(25, 0, 0);
        is_open = true;
    }

    public void CloseSuite()
    {
        a.localEulerAngles = b.localEulerAngles = Vector3.zero;
        is_open = false;
    }
}
