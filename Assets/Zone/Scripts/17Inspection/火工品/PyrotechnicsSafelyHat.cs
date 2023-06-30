using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// ��Ʒ��ȫ����ñ
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class PyrotechnicsSafelyHat : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// �Ƿ��Ѹ��Ӳ�������
    /// </summary>
    public bool inTool = false;

    public bool inRelease = false;

    /// <summary>
    /// �Ƿ�š��
    /// </summary>
    public bool is_screw_off = false;//š�ɺ����

    /// <summary>
    /// ����ʹ�õİ��ֹ���
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
        Debug.Log("��������");
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
        Debug.Log("�뿪����");
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
    /// ʹ�ù��߰�װ����Ʒ�����
    /// </summary>
    public void OnInstalled()
    {
        inTool = false;
        is_screw_off = false;
        SetInterable(false);
    }

    /// <summary>
    /// ʹ�ù��ߴӻ�Ʒ�ڲ����ʼ
    /// </summary>
    public void OnUninstalled()
    {
        inTool = true;
        is_screw_off = true;
        SetInterable(true);
    }
}
