using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleModeAnimationController : MonoBehaviour
{
    // »ñÈ¡ÉËº¦ÅÐ¶Ï·¶Î§
    public List<GameObject> AttackDetect;

    Animator playeranimator;
    bool isbattlemode;
    float attackweight;

    // ÅÐ¶ÏÊÇ·ñÔÚ½øÐÐ¹¥»÷
    public bool isAttacking;
    // ¹¥»÷¹¥»÷Ç°Ò¡Ê±¼ä
    public float attackWindup = 0.15f;
    // ¹¥»÷Ê±¼ä
    public float onAttacking = 0.4f;
    // ¹¥»÷ºóÒ¡
    public float attackWinddown = 0.15f;
    void Start()
    {
        playeranimator = GetComponent<Animator>();
        isbattlemode = false;
        attackweight = 0f;       
        isAttacking = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isbattlemode = !isbattlemode;
            if (isbattlemode)
            {
                playeranimator.SetLayerWeight(1, 0);
                playeranimator.SetLayerWeight(2, 1);
                //playeranimator.SetLayerWeight(3, 0);
                attackweight = 0f;
            }
            else
            {
                playeranimator.SetLayerWeight(1, 1);
                playeranimator.SetLayerWeight(2, 0);
                //playeranimator.SetLayerWeight(3, 1);
                attackweight = 0f;
            }
        }
        
        if (isbattlemode)
        {
            if (attackweight > 1f)
            {
                attackweight = 0f;
            }
            if (Input.GetMouseButtonDown(0) && !isAttacking)
            {
                playeranimator.SetBool("isAttack", true);
                playeranimator.SetFloat("Attack", attackweight);
                attackweight += 0.25f;
                isAttacking = true;
                StartCoroutine(AttackWind_up());
            }
            else if (Input.GetAxis("Horizontal") != 0 && !isAttacking || Input.GetAxis("Vertical") != 0 && !isAttacking)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playeranimator.SetFloat("Move", 1);
                }
                else
                {
                    playeranimator.SetFloat("Move", 0);
                }
                playeranimator.SetBool("isfrontwalking", true);
            }
            else
            {
                playeranimator.SetBool("isfronting", false);
            }
        }
    
    }

    IEnumerator AttackWind_up()
    {
        yield return new WaitForSeconds(attackWindup);
        AttackDetect[0].SetActive(true);
        StartCoroutine(OnAttacking());
    }
    IEnumerator OnAttacking()
    {
        yield return new WaitForSeconds(onAttacking);
        AttackDetect[0].SetActive(false);
        StartCoroutine(AttackWind_down());
    }
    IEnumerator AttackWind_down()
    {
        yield return new WaitForSeconds(attackWinddown);
        isAttacking = false;
        playeranimator.SetBool("isAttack", false );
    }
}
