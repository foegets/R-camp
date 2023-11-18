using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleModeAnimationController : MonoBehaviour
{
    Animator playeranimator;
    bool isbattlemode;
    float attackweight;
    // Start is called before the first frame update
    void Start()
    {
        playeranimator = GetComponent<Animator>();
        isbattlemode = false;
        attackweight = 0f;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isbattlemode = !isbattlemode;
            if (isbattlemode)
            {
                playeranimator.SetLayerWeight(1, 0);
                playeranimator.SetLayerWeight(2, 1);
                playeranimator.SetLayerWeight(3, 0);
                attackweight = 0f;
            }
            else
            {
                playeranimator.SetLayerWeight(1, 1);
                playeranimator.SetLayerWeight(2, 0);
                playeranimator.SetLayerWeight(3, 1);
                attackweight = 0f;
            }
        }
        
        if (isbattlemode)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
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
            if (Input.GetMouseButtonDown(0))
            {
                playeranimator.SetTrigger("isattack");
                playeranimator.SetFloat("Attack", attackweight);
                attackweight += 0.25f;
            }
        }
        if (attackweight > 1)
        {
            attackweight = 0f;
        }
    }
}
