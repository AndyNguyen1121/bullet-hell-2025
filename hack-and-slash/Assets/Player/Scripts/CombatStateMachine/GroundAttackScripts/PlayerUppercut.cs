using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUppercut : PlayerCombatBaseState
{
    public PlayerUppercut(PlayerCombatStateMachine stateMachine, PlayerCombatStateFactory factory) : base(stateMachine, factory) { }
    public override void EnterState()
    {
        playerManager.playerAnimationManager.ChangeRootMotionMultiplier(1, 1.5f, 1);
        playerManager.playerAnimationManager.PlayActionAnimation(
            animationName: "Uppercut",
            isPerformingAction: true,
            applyRootMotion: true,
            rotateTowardsPlayerInput: !playerManager.playerCameraManager.isLockedOn, // do not follow input rotation when locked on
            canRotate: playerManager.playerCameraManager.isLockedOn, // allow lock on rotations to occur during attack
            canMove: false,
            useGravity: false);

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

        stateMachine.playerManager.playerMovementManager.isJumping = true;
        PlayerManager.instance.EnableEnemyLayerCollision();
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
