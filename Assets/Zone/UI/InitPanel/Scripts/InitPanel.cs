using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGZN.Unity;
using UnityEngine.UI;

public class InitPanel : UIBase
{
    public Button ��ʼѵ��, ����, �˳�, ѵ��ȷ��,����ȷ��, PL1, PL2;
    public Transform ѵ������, ���ý���;
    public Color color;
    string currentSubject="";
    void Start()
    {
        InitObject();
        ��ʼѵ��.onClick.AddListener(() => {
            ��ʼѵ��.GetComponent<Image>().color = color;
            ����.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            �˳�.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            ѵ������.gameObject.SetActive(true);
            ���ý���.gameObject.SetActive(false);
            Debug.Log("��ʼ");

        });
   
        ����.onClick.AddListener(() => {
            ��ʼѵ��.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            ����.GetComponent<Image>().color = color;
            �˳�.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            ѵ������.gameObject.SetActive(false);
            ���ý���.gameObject.SetActive(true);


        });
        �˳�.onClick.AddListener(() => {
            ��ʼѵ��.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            ����.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            �˳�.GetComponent<Image>().color = color;
            ѵ������.gameObject.SetActive(false);
            ���ý���.gameObject.SetActive(false);

            Application.Quit();

        });

        PL1.onClick.AddListener(() => {
            PL1.GetComponent<Image>().color = color;
            PL2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            currentSubject = "PL1";
        });
        PL2.onClick.AddListener(() => {
            PL1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            PL2.GetComponent<Image>().color = color;
            currentSubject = "PL2";
        });

        ѵ��ȷ��.onClick.AddListener(() => {
            if (!string.IsNullOrEmpty( currentSubject))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                
            }
        });
        ����ȷ��.onClick.AddListener(() => {
            ����.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            ���ý���.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
