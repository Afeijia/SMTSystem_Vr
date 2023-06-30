using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// 顶部电缆接口/保护帽子
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class TopCable : DeviceBase
{
    public DevType devType;

    public bool inHand = false;

    /// <summary>
    /// 是否已放置于接口
    /// </summary>
    public bool inPlaced = false;

    public bool inRelease = false;

    public bool is_screw_off;

    public Vector3 RotateAxis;

    public TopCablePort Port;

    public SteamVR_Action_Boolean ConfirmBtn;
    public override void Start()
    {
        base.Start();
        Port = GetComponentInParent<TopCablePort>();
        SetInterable(is_screw_off = !Port);
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        if (is_screw_off) return;
        if (ConfirmBtn.GetStateDown(hand.handType))
        {
            inPlaced = false;
            Relax();
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
        TopCablePort port = other.GetComponent<TopCablePort>();
        if (port == null || port.inUsed) return;
        Port = port;
        Port.Cable = this;

        inPlaced = true;
        transform.SetParent(port.cable_place);
        transform.localPosition = Port.tighten_in_pos;
        transform.localEulerAngles = Vector3.zero;

        port.inUsed = true;


        Tighten();
    }

    void InFree()
    {
        inRelease = false;
    }

    /// <summary>
    /// 拧紧
    /// </summary>
    protected virtual void Tighten()
    {
        float time = 1f;

        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalMove(Port.release_in_pos, 2).OnComplete(() =>
            {
                inPlaced = true;
                is_screw_off = false;
                SetInterable(false);
            });
            transform.DOLocalRotate(RotateAxis, 2, RotateMode.FastBeyond360);
        });
    }

    /// <summary>
    /// 拧松
    /// </summary>
    protected virtual void Relax()
    {
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalMove(Port.tighten_in_pos, 2).OnComplete(() =>
            {

                Port.Cable = null;
                Port.inUsed = false;
                Port = null;

                is_screw_off = true;

                transform.parent = null;

                SetInterable(true);
            });
            transform.DOLocalRotate(-RotateAxis, 2, RotateMode.FastBeyond360);
        });
    }

}
