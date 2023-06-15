using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunctionPanel : MonoBehaviour
{
    public VRUIButon �˳�;
    public VRUIButon ��ɽ���, װ����ж, װ��װ��,�˵�;
    public VRUIButon ��λ1,��λ2, ��λ3, �����, ģ����,͸��,����;
    public VRUIButon ��ж����, ��ж����, ��ж��������,��ж��������;
    public VRUIButon װ�䲽��, װ�䶯��, װ�䶯������, װ�䶯������;

    public AnimManager animManager;
    public FadeManagerTest effectManager;


    public StepTablePanel stepTablePanel;           //����
    public SparePartsTreePanel sparePartsTreePanel; //���


    // devie

    public DeviceTest deviceManager;
    void Start()
    {
        // �Զ���ֵ
        InitObjects();
        �˳�.OnClickDn.AddListener(() => {
            LoadScene.LoadSceneByName(�˳�.name, "MainMenuScene");
        });
        �˵�.OnClickDn.AddListener(() => {
            animManager.BackAnimToTarPos();
            deviceManager.InitToInitPro();
        });
        return;
        //��ɽ���.OnClickDn.AddListener(()=> {
        //    animManager.BackAnimToInitPos();
        //});
        //װ����ж.OnClickDn.AddListener(() => {
        //    animManager.BackAnimToInitPos();

        //    deviceManager.InitToTarPro();
        //});
        //װ��װ��.OnClickDn.AddListener(() => {
        //    animManager.BackAnimToTarPos();
        //    deviceManager.InitToInitPro();
        //});

        // ����
        ��λ1.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            deviceManager.HideAllHightLight();
        });
        ��λ2.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            deviceManager.HideAllHightLight();
        });
        ��λ3.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            deviceManager.HideAllHightLight();
        });

        �����.OnClickDn.AddListener(() => {
            if (stepTablePanel.gameObject.activeInHierarchy)
            {
                stepTablePanel.gameObject.SetActive(false);
            }    
            sparePartsTreePanel.ShowPanel();
        });
        ģ����.OnClickDn.AddListener(() => {

        });
        ͸��.OnClickDn.AddListener(() => {
            Text t = ͸��.GetComponentInChildren<Text>();
            if (t.text == "͸��")
            {
                effectManager.SetFadeMaterial();
                t.text = "ʵ��";
                if (����.GetComponentInChildren<Text>().text == "ʵ��")
                {
                    ����.GetComponentInChildren<Text>().text = "����";
                }
            }
            else
            {
                t.text = "͸��";
                effectManager.SetInitMaterial();
            }
        });
        ����.OnClickDn.AddListener(() => {
            Text t = ����.GetComponentInChildren<Text>();
            if (t.text == "����")
            {
                effectManager.SetGridMaterial();
                t.text = "ʵ��";

                if (͸��.GetComponentInChildren<Text>().text == "ʵ��")
                {
                    ͸��.GetComponentInChildren<Text>().text = "͸��";
                }
            }
            else {
                t.text = "����";
                effectManager.SetInitMaterial();
            }
        });

        // ���
        ��ж����.OnClickDn.AddListener(() => {
            if (sparePartsTreePanel.gameObject.activeInHierarchy)
            {
                sparePartsTreePanel.gameObject.SetActive(false);
            }
            stepTablePanel.SetInitPos();
        });
        ��ж����.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            animManager.StartAnim();
        });
        ��ж��������.OnClickDn.AddListener(() => {
            animManager.AnimSpeedDown();
        });
        ��ж��������.OnClickDn.AddListener(() => {
            animManager.AnimSpeedUp();
        });

        // װ��
        װ�䲽��.OnClickDn.AddListener(() => {
            if (sparePartsTreePanel.gameObject.activeInHierarchy)
            {
                sparePartsTreePanel.gameObject.SetActive(false);
            }
            stepTablePanel.SetInitPos();
        });
        װ�䶯��.OnClickDn.AddListener(() => {
            animManager.BackAnimToTarPos();
            animManager.EndAnim();
        });
        װ�䶯������.OnClickDn.AddListener(() => {
            animManager.AnimSpeedDown();
        });
        װ�䶯������.OnClickDn.AddListener(() => {
            animManager.AnimSpeedUp();
        });
    }

    void InitObjects()
    {
        // ��ȡ���� vrui����
        Type type = typeof(MenuFunctionPanel);
        var filedls = type.GetFields();
        List<FieldInfo> fieldList = new List<FieldInfo>();
        foreach (var item in filedls)
        {
            if (item.FieldType.Equals(typeof(VRUIButon)))
            {
                fieldList.Add(item);
            }
        }
        // ����������  ��ȡ���
        List<VRUIButon> btnList = transform.GetComponentsInChildren<VRUIButon>(true).ToList();
        foreach (var field in fieldList)
        {
            foreach (var btn in btnList)
            {
                if (btn.name.Equals(field.Name))
                {
                    field.SetValue(this, btn);
                    Debug.Log(btn.name);
                    continue;
                }
            }
        }
    }
}
