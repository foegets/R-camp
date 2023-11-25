using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_control : MonoBehaviour
{   public bool attacking;
    public bool handing;
    Animator animator;
    public luna_controler luna_Controler;
    public Audiomanager Aduio;
    // Start is called before the first frame update
    void Start()
    {
        handing = false;
        attacking = false;
        animator = GetComponent<Animator>();
        animator.SetBool("is_attacking", attacking);
        animator.SetBool("is_attacking", handing);
        Aduio = GameObject.Find("Audiomanager").GetComponent<Audiomanager>();//��ȡAudio�Ľű�
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&luna_Controler.is_groud )//ת��Ϊ������
        {
            handing=!handing;
            animator.SetBool("is_handing",handing);
        }
        
        if (Input.GetKeyDown(KeyCode.F)&&handing) {   //������
            attacking = true;
            Aduio.Attacking.Play();
            StartCoroutine(change_attacking(1)); //���򽫵ȴ�1�� 
            
        }
        
        animator.SetBool("is_attacking", attacking);

    }
    IEnumerator change_attacking(float seconds)
    {
        float time = 0;
        while (time < seconds)
        {
            time += Time.deltaTime;
            yield return null;
        }
        attacking = false;
    }
}
