using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;

/// <summary>
/// ����֧������
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class BracketWheel : DeviceBase
{
    protected override void OnHandHoverBegin(Hand hand)
    {
        Place();
    }
    /// <summary>
    /// �Ƿ����
    /// </summary>
    bool isPlace = false;
    /// <summary>
    /// ���ڲ���
    /// </summary>
    bool isOperation = false;
    public void Place()
    {
        if (isOperation)
            return;
        isOperation = true;
        float hight = isPlace ? -0.1697885f : -0.3419f;
        transform.DOLocalMoveY(hight, 2).OnComplete(() => {
            isOperation = false;
            isPlace = !isPlace;
        });
    }
}
