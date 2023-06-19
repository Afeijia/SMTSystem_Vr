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
    public bool inTool = true;

    public bool inRelease = false;

    /// <summary>
    /// �Ƿ�š��
    /// </summary>
    public bool is_screw_off;//š�ɺ����

    public Vector3 off_Pos;
    /// <summary>
    /// ��������λ��
    /// </summary>
    public Transform wrenchPlace;

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
        if (wrench == null || wrench.inUsed)
        {
            return;
        }



        ////��װ
        //if (is_screw_off)
        //{
        //    if (inTool) return;
        //    PyrotechnicsPort port = other.GetComponent<PyrotechnicsPort>();
        //    if (port == null || port.inUsed)
        //    {
        //        return;
        //    }
        //}
        ////ж�� 
        //else
        //{

        //}


    }

    void InFree()
    {
        inRelease = false;
    }

    public void OnWrenchPlace(PyrotechnicsWrench wrench)
    {
        wrenchTool = wrench;
        wrench.transform.SetParent(wrenchPlace);

        wrench.transform.localPosition = Vector3.zero;
        wrench.transform.localEulerAngles = Vector3.zero;
    }
}
