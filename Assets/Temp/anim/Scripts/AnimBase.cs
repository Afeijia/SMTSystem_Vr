using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimBase : MonoBehaviour
{
    public int sort=0;


    public Vector3 initPos,initRot;
    public Vector3 tarPos,tarRot;
    public float flyTime = 1;
    private float initflyTime = 1;
    void Start()
    {
        initPos = transform.position;
        initRot = transform.localEulerAngles;
        initflyTime = flyTime;
    }
    private void Update()
    {
       
    }
    [ContextMenu("��Ϊ��������")]
    public void StartAnimIte()
    {
        transform.DOMove(tarPos,flyTime).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(tarRot, flyTime).SetEase(Ease.InOutSine);
    }
    [ContextMenu("��λ��������")]
    public void EndAnimItem()
    {
        transform.DOMove(initPos, flyTime).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(initRot, flyTime).SetEase(Ease.InOutSine);
    }
    [ContextMenu("ֱ�ӹ�λ����")]
    public void EndItemToInitPos()
    {
        transform.position = initPos;
        transform.localEulerAngles = initRot;
        if (sing)
        {
            transform.localEulerAngles = new Vector3(0 ,- 92.928f,0);
            transform.position = initPos;
        }
    }
    public bool sing ;
    [ContextMenu("ֱ�Ӿ�λ����")]
    public void EndItemToTarpos()
    {
        transform.position = tarPos;
        transform.localEulerAngles = tarRot;
    }
    public void SetSpeed(float para)
    {
        flyTime = para * initflyTime;
    }


    [ContextMenu("[�༭��]�����ó�ʼ��")]
    void SaveInitInEditor()
    {
        initPos = transform.position;
        initRot = transform.localEulerAngles;
    }
    [ContextMenu("[�༭��]������Ŀ���")]
    void SaveTarInEditor()
    {
        tarPos = transform.position;
        tarRot = transform.localEulerAngles;
    }
 

    [ContextMenu("[�༭��]����λ��Ŀ���")]
    void FlyToTarInEditor()
    {
        transform.position = tarPos;
        transform.localEulerAngles = tarRot;
    }
    [ContextMenu("[�༭��]����λ����ʼ��")]
    void FlyToInitInEditor()
    {
        transform.position = initPos;
        transform.localEulerAngles = initRot;
    }
}
