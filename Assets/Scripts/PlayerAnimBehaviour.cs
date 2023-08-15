using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (animator.gameObject.tag)
        {
            case "Player1": //MC
                GameManager.instance.UpdateBattleState(GameManager.BattleState.KnightTurn);
                break;

            case "Player2": //Knight
                GameManager.instance.UpdateBattleState(GameManager.BattleState.MageTurn);
                break;

            case "Player3": //Mage
                GameManager.instance.UpdateBattleState(GameManager.BattleState.PriestTurn);
                break;

            case "Player4": //Priest
                GameManager.instance.UpdateBattleState(GameManager.BattleState.EnemyTurn);
                break;

            case "Enemy":
                GameManager.instance.EnemyAttack();
                
                GameObject enemy = animator.gameObject;
                int enemyIndex = GameManager.instance.allEnemies.IndexOf(enemy);

                if (enemyIndex < GameManager.instance.allEnemies.Count - 1)
                {
                    GameManager.instance.allEnemies[enemyIndex + 1].GetComponent<Animator>().SetTrigger("isAttacking");
                }
                else
                {
                    GameManager.instance.UpdateBattleState(GameManager.BattleState.MCTurn);
                    GameManager.instance.playerTurn.SetActive(true);
                }
                break;
        }
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
