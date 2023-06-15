using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BuckleParent : MonoBehaviour
{
    /// <summary>
    /// ����
    /// </summary>
   public CircularDrive door;
    /// <summary>
    /// ����s
    /// </summary>
    Buckle[] buckles;
    void Start()
    {
        buckles = transform.GetComponentsInChildren<Buckle>();
    }
    
    /// <summary>
    /// ��������״̬
    /// </summary>
    public void UpdateDoorState()
    {
        bool isOpen = door.GetComponent<BoxCollider>().enabled;
        if (isOpen)
        {

        }
        else
        {
            // ȫ����  �ſ��Կ�����
            bool allOpen = true;
            foreach (var item in buckles)
            {
                if (!item.isOpen)
                {
                    allOpen = false;
                    return;
                }
            }
            if (allOpen)
            {
                door.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

}
