using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{

    /// <summary>
    /// Эͬ������������ַ
    /// </summary>
    public static string Ip => ip;
    /// <summary>
    /// �û���
    /// </summary>
    public static string UserName => userName;

    private static string ip;
    private static string userName;

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
}
