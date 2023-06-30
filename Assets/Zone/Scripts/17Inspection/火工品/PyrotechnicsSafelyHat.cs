using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 火工品安全保护帽
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class PyrotechnicsSafelyHat : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// 是否已附加操作工具
    /// </summary>
    public bool inTool = false;

    public bool inRelease = false;

    /// <summary>
    /// 是否拧松
    /// </summary>
    public bool is_screw_off = false;//拧松后可拿

    /// <summary>
    /// 正在使用的扳手工具
    /// </summary>
    public PyrotechnicsWrench wrenchTool;

    public override void Start()
    {
        base.Start();
        SetInterable(is_screw_off);
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("拿在手中");
        inHand = true;

        if (wrenchTool)
        {
            wrenchTool.inUsed = false;
            wrenchTool.Hat = null;
            wrenchTool = null;
        }
    }

    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        Debug.Log("离开手里");
        inHand = false;
        inRelease = true;
        Invoke("InFree", 0.2f);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!inRelease) return;
        PyrotechnicsWrench wrench = other.GetComponent<PyrotechnicsWrench>();
        if (wrench == null || wrench.inUsed || !wrench.inHand)
        {
            return;
        }
        wrenchTool = wrench;
        wrenchTool.Hat = this;
        wrenchTool.inUsed = true;

        inTool = true;
        transform.SetParent(wrench.hat_place);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    void InFree()
    {
        inRelease = false;
    }

    /// <summary>
    /// 使用工具安装至火工品口完成
    /// </summary>
    public void OnInstalled()
    {
        inTool = false;
        is_screw_off = false;
        SetInterable(false);
    }

    /// <summary>
    /// 使用工具从火工品口拆除开始
    /// </summary>
    public void OnUninstalled()
    {
        inTool = true;
        is_screw_off = true;
        SetInterable(true);
    }
}
