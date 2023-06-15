using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// �򿪷�о
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class Spool : DeviceBase
{
    /// <summary>
    /// ʱ���ʱ��
    /// </summary>
    public Text timer;
    protected override void OnHandHoverBegin(Hand hand)
    {
        //Debug.Log("�ִ���");

        // ��������
        transform.localPosition = new Vector3(transform.localPosition.x,
                                            transform.localPosition.y,
                                            0.0266f);

        if (isFinish)
            return;
        highlighter.highlighted = false;
        ////inUse = true;
        //Door();
        timer.gameObject.SetActive(true);
    }
    protected override void OnHandHoverEnd(Hand hand)
    {
        //Debug.Log("�ִ���");
        // ������������
        transform.localPosition = new Vector3(transform.localPosition.x,
                                        transform.localPosition.y,
                                        0.03334689f);
    }
    protected override void HandHoverUpdate(Hand hand)
    {
        //Debug.Log("��������");
        //highlighter.highlighted = true;
        //inUse = true;
        Operation();
    }
    /// <summary>
    /// ʣ�����ʱ��
    /// </summary>
    public float residueTime = 3;
    /// <summary>
    /// �Ƿ����
    /// </summary>
    bool isFinish = false;
    public void Operation()
    {
        if (isFinish)
            return;
        residueTime -= Time.deltaTime;
        if (residueTime < 0f)
        {
            isFinish = true;
            timer.gameObject.SetActive(false);
            // �ر� ʱ����ʾ
        }
        else
        {
            timer.text = residueTime.ToString("F1");
        }
    }
}
