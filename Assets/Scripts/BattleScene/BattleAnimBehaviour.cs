using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimBehaviour : StateMachineBehaviour
{
    IEnumerator DestroyAfterAnimation(AnimatorStateInfo stateInfo, GameObject obj)
    {
        yield return new WaitForSecondsRealtime(stateInfo.length);

        Destroy(obj);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Death"))
        {
            GameManager.instance.StartCoroutine(GameManager.instance.DisplayEndUI(stateInfo));
        }

        if (stateInfo.IsName("MissAttackEffect"))
        {
            GameManager.instance.StartCoroutine(DestroyAfterAnimation(stateInfo, animator.transform.parent.gameObject));
            Debug.Log(animator.transform.parent.gameObject);
        }

        //change character turn icon to enemies
        if (stateInfo.IsName("Attack") && animator.gameObject.tag == "Enemy")
        {
            GameManager.instance.turnIcon.sprite = animator.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (animator.gameObject.tag)
        {
            case "Player":
                if (stateInfo.IsName("Attack"))
                {
                    GameManager.instance.questionUI.SetActive(true);
                }
                break;

            case "Enemy":
                if (stateInfo.IsName("Attack"))
                {
                    GameManager.instance.EnemyAttack();

                    if (GameManager.instance.allPlayers.Count > 0) //if there are still players remaining
                    {
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
                    }
                }
                else if (stateInfo.IsName("Damaged"))
                {
                    if (GameManager.instance.allEnemies.Count > 0) //if there are still enemies remaining
                    {
                        GameManager.instance.NextBattleState();
                    }
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
