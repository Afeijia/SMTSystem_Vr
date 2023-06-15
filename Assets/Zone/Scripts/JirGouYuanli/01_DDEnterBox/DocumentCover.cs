using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;

/// <summary>
/// ���ļ���
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class DocumentCover : DeviceBase
{
    bool inUse = false;
    protected override void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("�ִ���");
        highlighter.highlighted = false;
        //inUse = true;
        Door();
    }
    protected override void OnAttachedToHand(Hand hand)
    {
        Debug.Log("��������");
        highlighter.highlighted = true;
        inUse = true;
    }
    /// <summary>
    /// �Ƿ�ر�
    /// </summary>
    bool isOpen = false;
    /// <summary>
    /// ���ڲ���
    /// </summary>
    bool isOperation = false;
    public void Door()
    {
        if (isOperation)
            return;
        isOperation = true;
        float angle = isOpen ? -90 : 20;
        transform.DOLocalRotate(new Vector3(0, angle, 0),2).OnComplete(()=> {
            isOperation = false;
            isOpen = !isOpen;
        });
    }
}
