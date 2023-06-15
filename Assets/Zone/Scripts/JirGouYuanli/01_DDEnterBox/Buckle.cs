using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ������
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class Buckle : DeviceBase
{
    /// <summary>
    /// �Ƿ��
    /// </summary>
    public bool isOpen=false;
    /// <summary>
    /// ���ڲ���
    /// </summary>
    bool isOperation = false;
    public Vector3 tarAngle;
    protected override void OnHandHoverBegin(Hand hand)
    {
        Operation();
    }
    public void Operation()
    {
        if (isOperation)
            return;
        isOperation = true;
        Vector3 angle = isOpen ?  new Vector3(0,0,0): tarAngle;
        transform.DOLocalRotate(angle, 2).OnComplete(() => {
            isOperation = false;
            isOpen = !isOpen;
            // ��������״̬
            transform.GetComponentInParent<BuckleParent>().UpdateDoorState();
        });
    }
}
