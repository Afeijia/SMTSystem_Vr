using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// ��Ʒ����
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class PyrotechnicsWrench : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// �Ƿ��Ѹ��ӻ�Ʒñ
    /// </summary>
    public bool inUsed = true;

    public bool inRelease = false;

    public override void Start()
    {
        base.Start();
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("��������");
        inHand = true;
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
        if (inUsed) return;

        PyrotechnicsSafelyHat hat = other.GetComponent<PyrotechnicsSafelyHat>();
        if (hat == null || hat.inTool)
        {
            return;
        }

        inUsed = true;
        hat.OnWrenchPlace(this);
    }
}
