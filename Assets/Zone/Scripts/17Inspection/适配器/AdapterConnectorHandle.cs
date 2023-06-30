using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using Valve.VR;
using Valve.VR.InteractionSystem;


/// <summary>
/// ²âÊÔÌ×¼þ
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class AdapterConnectorHandle : DeviceBase
{
    public bool is_open;
    public bool is_dotweening;
    public Transform child_part;
    public SteamVR_Action_Boolean interact_btn;
    public Vector3 open_rot, open_child_pos;

    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        if (interact_btn.GetStateDown(hand.handType))
        {
            if (is_open)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
    [ContextMenu("open")]
    private void Open()
    {
        is_dotweening = true;
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalRotate(open_rot, 2).OnComplete(() =>
            {
                is_dotweening = false; is_open = true;

            });
            child_part.DOLocalMoveZ(-0.3f, 2);
        });
    }
    [ContextMenu("close")]
    private void Close()
    {
        is_dotweening = true;
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalRotate(Vector3.zero, 2).OnComplete(() =>
            {
                is_dotweening = false; is_open = false;

            });
            child_part.DOLocalMoveZ(0, 2);
        });
    }
}
