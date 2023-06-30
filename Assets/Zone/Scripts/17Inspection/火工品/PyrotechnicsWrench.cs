using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 火工品扳手
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class PyrotechnicsWrench : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// 是否已附加火工品帽
    /// </summary>
    public bool inUsed = false;
    /// <summary>
    /// 是否已放置于火工品口
    /// </summary>
    public bool inPlaced = false;

    public bool inRelease = false;

    /// <summary>
    /// 保护帽放入工具位置
    /// </summary>
    public Transform hat_place;

    /// <summary>
    /// 火工品口
    /// </summary>
    public PyrotechnicsPort Port;

    public PyrotechnicsSafelyHat Hat;

    public Vector3 RotateAxis;

    public override void Start()
    {
        base.Start();
        ableTake = true;
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("拿在手中");
        inHand = true;
        OnDetachedFromPort();
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
        if (inPlaced) return;

        PyrotechnicsPort port = other.GetComponent<PyrotechnicsPort>();
        if (port == null || port.hasTool)
        {
            return;
        }

        if (port.OnWrenchPlace(this))
        {
            Port = port;
            inPlaced = true;
            SetInterable(false);

            if (inUsed)
            {
                Tighten();
            }
            else
            {
                Relax();
            }
        }
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
                inPlaced = false;
                Port.InstallHat(Hat);
                Hat = null;
                inUsed = false;
                FinishOP();
                SetInterable(true);
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
            Hat = Port.UninstallHat();
            Port.Hat = null;
            Hat.wrenchTool = this;
            Hat.transform.SetParent(hat_place);
            Hat.transform.localPosition = Vector3.zero;
            Hat.transform.localEulerAngles = Vector3.zero;

            transform.DOLocalMove(Port.tighten_in_pos, 2).OnComplete(() =>
            {
                inPlaced = false;

                inUsed = true;

                FinishOP();
                SetInterable(true);
            });
            transform.DOLocalRotate(-RotateAxis, 2, RotateMode.FastBeyond360);
        });
    }

    public void FinishOP()
    {
        OnDetachedFromPort();
        transform.parent = null;
    }

    /// <summary>
    /// 从火工品口拿下时 手动拿和拧动作完成后自动调用
    /// </summary>
    private void OnDetachedFromPort()
    {
        if (Port)
        {
            Port.hasTool = false;
            Port = null;
            inPlaced = false;
        }
    }
}
