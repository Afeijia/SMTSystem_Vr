using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SparePartsTreePanel : MonoBehaviour
{
    public VRUIButon close;

    void Start()
    {
        InitObjects();
        close.OnClickDn.AddListener(() => {
            gameObject.gameObject.SetActive(false);
        });
        gameObject.SetActive(false);
    }

    public void ShowPanel ()
    {
        gameObject.SetActive(true);
    }
    void InitObjects()
    {
        // ��ȡ���� vrui����
        Type type = typeof(SparePartsTreePanel);
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
