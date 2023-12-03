using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAnimation : StateMachineBehaviour//状态机的行为（后续做敌人也会用到）
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //即该动画进入的时候启动这个函数（？）
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

     }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //该动画持续执行时执行以下
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //动画退出的时候
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerController>().isHurt = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
