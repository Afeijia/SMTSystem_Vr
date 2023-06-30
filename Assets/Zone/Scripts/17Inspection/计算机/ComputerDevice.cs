using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


/// <summary>
/// ²âÊÔÌ×¼þ
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class ComputerDevice : DeviceBase
{
    public bool is_out;
    private bool is_dotweening;

    public Vector3 open_pos, open_screen_rot;
    public Transform screen;
    public SteamVR_Action_Boolean open_btn;

    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        if (open_btn.GetStateDown(hand.handType))
        {
            if (is_out)
            {
                if (!is_dotweening)
                    CloseDev();
            }
            else
            {
                if (!is_dotweening)
                    OpenDev();
            }
        }
    }

    [ContextMenu("TestOpen")]
    public void OpenDev()
    {
        is_dotweening = true;
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalMove(open_pos, 2).OnComplete(() =>
            {
                screen.DOLocalRotate(open_screen_rot, 2).OnComplete(() => { is_dotweening = false; is_out = true; });
            });
        });
    }
    [ContextMenu("TestClose")]
    public void CloseDev()
    {
        is_dotweening = true;
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            screen.DOLocalRotateQuaternion(Quaternion.identity, 2).OnComplete(() =>
            {
                transform.DOLocalMove(Vector3.zero, 2).OnComplete(() => { is_dotweening = false; is_out = false; });
            });
        });
    }
}
