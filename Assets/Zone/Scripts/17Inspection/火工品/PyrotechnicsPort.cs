using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʒ��
/// </summary>
public class PyrotechnicsPort : MonoBehaviour
{
    /// <summary>
    /// �Ƿ��ѱ�ʹ��
    /// </summary>
    public bool inUsed;
    /// <summary>
    /// �ѷ��ù���
    /// </summary>
    public bool hasTool;
    /// <summary>
    /// ����λ��
    /// </summary>
    public Transform tool_place;

    /// <summary>
    /// ��װ�豸ʱ����λ��
    /// </summary>
    public Vector3 release_in_pos;
    /// <summary>
    /// ����豸ʱ����λ��
    /// </summary>
    public Vector3 tighten_in_pos = Vector3.zero;

    /// <summary>
    /// ����ñλ��
    /// </summary>
    public Transform hat_place;

    public PyrotechnicsSafelyHat Hat;

    private void Start()
    {
        Hat = GetComponentInChildren<PyrotechnicsSafelyHat>();
        inUsed = Hat;
    }

    public bool OnWrenchPlace(PyrotechnicsWrench wrench)
    {
        if (hasTool) { return false; }
        if (inUsed == wrench.inUsed) { return false; }

        wrench.transform.SetParent(tool_place);
        wrench.transform.localPosition = inUsed ? release_in_pos : tighten_in_pos;
        wrench.transform.localEulerAngles = Vector3.zero;

        hasTool = true;
        return true;
    }

    /// <summary>
    /// װ��
    /// </summary>
    /// <param name="hat"></param>
    public void InstallHat(PyrotechnicsSafelyHat hat)
    {
        Hat = hat;

        hat.transform.SetParent(hat_place);
        hat.transform.localPosition = Vector3.zero;
        hat.transform.localEulerAngles = Vector3.zero;
        Hat.OnInstalled();

        inUsed = true;
    }

    /// <summary>
    /// ж��
    /// </summary>
    /// <param name="hat"></param>
    public PyrotechnicsSafelyHat UninstallHat()
    {
        inUsed = false;
        Hat.OnUninstalled();
        return Hat;
    }
}
