using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class GridMananger : MonoBehaviour
{
    [Tooltip("ȷ�ϡ���鰴ť")] public SteamVR_Action_Boolean ConfirmBtn;
    [Tooltip("��ʾ��ʾ��Ϣ��ť")] public SteamVR_Action_Boolean PromptsBtn;
    public SteamVR_LaserPointer steamVR_Laser;
    public GameObject panel;

    public virtual void Start()
    {
        ConfirmBtn.onStateDown += Test;
    }

    public void Test(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("����"+fromAction.activeDevice);
        if (fromAction.activeDevice.ToString().Equals("LeftHand"))
        {
        panel.gameObject.SetActive(!panel.gameObject.activeInHierarchy);

        }
        else if (fromAction.activeDevice.ToString().Equals("RightHand"))
        {
            steamVR_Laser.SetLinerActive();
        }
    }
}
