using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGZN.Unity;
using UnityEngine.UI;

public class MainMenuPanel : UIBase
{
    public Button �ṹԭ��, ����ʹ��, ���ά��, ά������;

    string currentSubject="";
    void Start()
    {
        InitObject();

        �ṹԭ��.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("01�ṹԭ��");
        });
        ����ʹ��.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson_�γ�00");
        });
        ���ά��.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson_�γ�00");
        });
        ά������.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson_�γ�00");
        });
    }
}
