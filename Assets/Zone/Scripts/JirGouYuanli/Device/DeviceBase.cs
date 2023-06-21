using UnityEngine;
using Valve.VR.InteractionSystem;
using HighlightPlus;
public enum DeviceState
{
    InHand,
    Release,
    Free
}
/// <summary>
/// �豸����
/// </summary>
public class DeviceBase : MonoBehaviour
{
    protected HighlightEffect highlighter;
    /// <summary>
    /// �豸״̬
    /// </summary>
    public DeviceState DeviceState;
    /// <summary>
    /// �Ƿ������
    /// </summary>
    public bool ableTake;
    /// <summary>
    /// �豸����
    /// </summary>
    public string deviceName;
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public GameObject tipsObj;
    public virtual void Start()
    {
        highlighter = this.GetComponent<HighlightEffect>();
    }
    protected virtual void OnHandHoverBegin(Hand hand)
    {
        //Debug.Log("��ʼ����");
    }
    protected virtual void HandHoverUpdate(Hand hand)
    {
        //Debug.Log("��������");
    }
    protected virtual void OnHandHoverEnd(Hand hand)
    {
        //Debug.Log("��������");
    }
    protected virtual void OnAttachedToHand(Hand hand)
    {
        //Debug.Log("��������");
        if (ableTake && DeviceState == DeviceState.Free)
        {
            DeviceState = DeviceState.InHand;
        }
    }
    protected virtual void HandAttachedUpdate(Hand hand)
    {
        //Debug.Log("������������");
    }
    protected virtual void OnDetachedFromHand(Hand hand)
    {
        //Debug.Log("�������뿪");
        if (DeviceState == DeviceState.InHand)
        {
            DeviceState = DeviceState.Release;
            Invoke("ResetFree", 0.5f);
        }
    }
    /// <summary>
    /// �ع�����״̬
    /// </summary>
    /// <param name="deviceOP"></param>
    public virtual void ResetFree()
    {
        if (DeviceState == DeviceState.Release)
            DeviceState = DeviceState.Free;
    }
    //protected virtual void OnTriggerEnter(Collider col)
    //{
    //}
    //protected virtual void OnTriggerStay(Collider col)
    //{
    //}
    //protected virtual void OnTriggerExit(Collider col)
    //{
    //}
    public virtual void ProcedureStart()
    {

    }
    public virtual void ProcedureEnd()
    {

    }

    protected virtual void SetInterable(bool canMove)
    {
        transform.GetComponent<Interactable>().enabled = canMove;
        if (transform.GetComponent<Throwable>())
            transform.GetComponent<Throwable>().able_throw = canMove;
    }
}
