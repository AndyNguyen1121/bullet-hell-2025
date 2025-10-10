using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3 : PlayerCombatBaseState
{
    public PlayerAttack3(PlayerCombatStateMachine stateMachine, PlayerCombatStateFactory factory) : base(stateMachine, factory) { }
    public override void EnterState()
    {
        playerManager.playerAnimationManager.PlayActionAnimation(
            animationName: "Attack3",
            isPerformingAction: true,
            applyRootMotion: !playerManager.playerCameraManager.isLockedOn,
            rotateTowardsPlayerInput: !playerManager.playerCameraManager.isLockedOn, // do not follow input rotation when locked on
            canRotate: playerManager.playerCameraManager.isLockedOn, // allow lock on rotations to occur during attack
            canMove: false,
            useGravity: true);

        if (playerManager.playerCameraManager.isLockedOn)
        {
            Vector3 enemyPosition = playerManager.playerCameraManager.currentLockOnTarget.position;
            enemyPosition = new Vector3(enemyPosition.x, playerManager.transform.position.y, enemyPosition.z);

            stateMachine.GravitateTowardsTransform(
                objectTransform: playerManager.playerCameraManager.currentLockOnTarget,
                distanceToStop: 1.5f,
                minimumDistance: 1.5f,
                maximumDistance: 5f,
                duration: 0.1f,
                canMoveOnComplete: false);
        }
    }

    public override void UpdateState()
    {
        foreach (CombatScriptableObj criteria in stateMachine.currentStateObj.nextStates)
        {
            if (stateMachine.ValidateCombatStateCriteria(criteria))
            {
                stateMachine.SwitchState(criteria.stateID);
            }
        }
    }

    public override void ExitState()
    {

    }
}
