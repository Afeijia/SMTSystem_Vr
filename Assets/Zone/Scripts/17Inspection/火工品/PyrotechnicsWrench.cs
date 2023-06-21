using DG.Tweening;
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
    public bool inUsed = false;
    /// <summary>
    /// �Ƿ��ѷ����ڻ�Ʒ��
    /// </summary>
    public bool inPlaced = false;

    public bool inRelease = false;

    /// <summary>
    /// ����ñ���빤��λ��
    /// </summary>
    public Transform hat_place;

    /// <summary>
    /// ��Ʒ��
    /// </summary>
    public PyrotechnicsPort Port;

    public PyrotechnicsSafelyHat Hat;

    public Vector3 RotateAxis;

    public override void Start()
    {
        base.Start();
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("��������");
        inHand = true;
        OnDetachedFromPort();
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

    /// <summary>
    /// š��
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
                FinishOP();
                SetInterable(true);
            });
            transform.DOLocalRotate(RotateAxis, 2, RotateMode.FastBeyond360);
        });
    }

    /// <summary>
    /// š��
    /// </summary>
    protected virtual void Relax()
    {
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalMove(Port.tighten_in_pos, 2).OnComplete(() =>
            {
                inPlaced = false;
                Hat = Port.UninstallHat();
                Hat.transform.SetParent(hat_place);
                Hat.transform.localPosition = Vector3.zero;
                Hat.transform.localEulerAngles = Vector3.zero;
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
    /// �ӻ�Ʒ������ʱ �ֶ��ú�š������ɺ��Զ�����
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
