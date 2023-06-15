using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel1 : MonoBehaviour
{
    public VRUIButon �ṹԭ��, ʵ��ѵ��;
    public JumpEffect �ṹԭ��_Jump;
    void Start()
    {
        �ṹԭ��.OnClickDn.AddListener(() => {

            StartCoroutine(DelayEnterScene()); 
        });
        �ṹԭ��.OnPointEnterEvent.AddListener(() => {
            // ֹͣ�˶�
            ParticleSystem ps = �ṹԭ��.GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emis = ps.emission;
            emis.enabled = false;

            ParticleSystem pf = �ṹԭ��.transform.Find("Fire").GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emisf = pf.emission;
            emisf.enabled = false;

            �ṹԭ��_Jump.Stand();
        });
        �ṹԭ��.OnPointExitEvent.AddListener(() => {
            // ֹͣ�˶�
            ParticleSystem ps = �ṹԭ��.GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emis = ps.emission;
            emis.enabled = true;

            ParticleSystem pf = �ṹԭ��.transform.Find("Fire").GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emisf = pf.emission;
            emisf.enabled = true;

            �ṹԭ��_Jump.Jump();
        });
    }
    IEnumerator DelayEnterScene()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("0_�ṹԭ��_�γ�ѡ��");
    }
}
