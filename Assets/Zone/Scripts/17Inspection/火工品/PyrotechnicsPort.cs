using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火工品口
/// </summary>
public class PyrotechnicsPort : MonoBehaviour
{
    /// <summary>
    /// 是否已被使用
    /// </summary>
    public bool inUsed;
    /// <summary>
    /// 已放置工具
    /// </summary>
    public bool hasTool;
    /// <summary>
    /// 工具位置
    /// </summary>
    public Transform tool_place;

    /// <summary>
    /// 安装设备时吸附位置
    /// </summary>
    public Vector3 release_in_pos;
    /// <summary>
    /// 拆除设备时吸附位置
    /// </summary>
    public Vector3 tighten_in_pos = Vector3.zero;

    /// <summary>
    /// 保护帽位置
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
    /// 装上
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
    /// 卸下
    /// </summary>
    /// <param name="hat"></param>
    public PyrotechnicsSafelyHat UninstallHat()
    {
        inUsed = false;
        Hat.OnUninstalled();
        return Hat;
    }
}
