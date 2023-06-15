using UnityEngine;
using Valve.VR;

public class GridMananger : MonoBehaviour
{
    [Tooltip("ȷ�ϡ���鰴ť")] public SteamVR_Action_Boolean ConfirmBtn;
    [Tooltip("��ʾ��ʾ��Ϣ��ť")] public SteamVR_Action_Boolean PromptsBtn;

    public GameObject panel;

    public virtual void Start()
    {
        ConfirmBtn.onStateDown += Test;
    }

    public void Test(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("����"+fromSource.ToString());

        panel.gameObject.SetActive(!panel.gameObject.activeInHierarchy);
    }
}
