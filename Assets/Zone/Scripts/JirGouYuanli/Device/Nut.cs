using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
/// <summary>
/// ��ĸ
/// </summary>
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class Nut : DeviceBase
{
    public bool inPlace = false;    // �Ƿ�����ڹ�����
    public bool inHand = false;     // �Ƿ�������
    public bool isTighted = false;  // �Ƿ�š��
    NutBox nb;                      // ��ĸ�� �����ϲ���
    Wrench wrench;                  // ����
    public Vector3  wrenchPos;  //����λ��
    public Vector3  wrenchRot;  // ���ֽǶ�
    public Vector3  initPos;    // ��ĸ��ʼλ��
    public Vector3  initRot;    // ��ĸ��ʼ�Ƕ�
    public Vector3 tarPos;      // ��ĸ��ת��Ƕ�
    public Vector3 tarRot;      // ��ĸ��ת��Ƕ�

    public float rotTime =2;       // š��ʱ��

    public override void Start()
    {
        base.Start();
        if (isTighted)
        {
            SetInterable(false);
        }
        initPos = transform.localPosition;
        initRot = transform.localEulerAngles;

        //nb = transform.parent.GetComponentInChildren<NutBox>() ;
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        Debug.Log("��������");
        //highlighter.highlighted = true;
        inHand = true;
    }
    protected override void HandHoverUpdate(Hand hand)
    {
    }
    protected override void OnDetachedFromHand(Hand hand)
    {
        Debug.Log("�뿪����");
        inHand = false;
        //if (inPlace)
        //    return;
        //else
        //{
        //    Remove();
        //}
    }

    /// <summary>
    /// š��
    /// </summary>
    public void OnWrenchPlace(Wrench wre)
    {
        wrench = wre;
        wre.transform.SetParent(transform);
        wre.transform.localPosition = wrenchPos;
        wre.transform.localEulerAngles = wrenchRot;
        //highlighter.highlighted = false ;
        if (isTighted)
        {
            Relax();
        }
        else
        {
            Tighten();
        }
    }

    /// <summary>
    /// š��
    /// </summary>
    protected virtual void Tighten()
    {
        float time = 1f;
        DOTween.To(() => time,x => time = x,1,.5f).OnComplete(()=> {
            transform.DOLocalMove(initPos, rotTime).OnComplete(() => {
                isTighted = true;
                wrench.FinishRot();
                SetInterable(true);
                FinishEvent(true);
            });
            transform.DOLocalRotate(initRot, rotTime);
        });
    }

    /// <summary>
    /// š��
    /// </summary>
    protected virtual void Relax()
    {
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1,.5f).OnComplete(() =>
        {
            transform.DOLocalMove(tarPos, rotTime).OnComplete(() =>
            {
                isTighted = false;
                wrench.FinishRot();
                FinishEvent(false);
            });
            transform.DOLocalRotate(tarRot, rotTime);
        });
    }
    /// <summary>
    /// ��ɺ��¼�
    /// </summary>
    protected virtual void FinishEvent(bool isTughtOrRelax)
    {
        
    }
    /// <summary>
    /// ������
    /// </summary>
    public void Place(NutBox nutBox)
    {
        //return
        //nb = nutBox;
        //inPlace = true;
    }
   

    /// <summary>
    /// ȡ��
    /// </summary>
    public void Remove()
    {
        //nb.inUse = false;
        //isPlace = false;
    }
}


