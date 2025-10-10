using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Animator animator;
    public Rigidbody rb;

    private Vector2 velocity;
    private Vector2 smoothDeltaPosition;
    private Vector3 lastPosition;
    private Vector3 rootMotionDelta;
  


    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        rb = GetComponent<Rigidbody>();
        animator = enemyManager.animator;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rootMotionDelta != Vector3.zero && !enemyManager.enemyInteractionManager.inKnockUpAnimation)
        {
            rb.MovePosition(rb.position + rootMotionDelta * Time.fixedDeltaTime);

            enemyManager.agent.nextPosition = rb.position;

            // Clear after applying
            rootMotionDelta = Vector3.zero;
        }

    }

    private void OnAnimatorMove()
    {
        if (animator == null)
            return;

        rootMotionDelta = animator.deltaPosition / Time.deltaTime;

    }

    public void SetMovementParameters(float horizontal, float vertical)
    {
        enemyManager.animator.SetFloat("horizontal", horizontal, 0.6f, Time.deltaTime * 9f);
        enemyManager.animator.SetFloat("vertical", vertical, 0.6f, Time.deltaTime * 9f);

    }

    public void PlayActionAnimation(string animationName, bool canMove = true, bool rootMotion = true, bool isPerformingAction = true, float normalizedTime = 0.05f)
    {
        enemyManager.canMove = canMove;
        animator.applyRootMotion = rootMotion;
        enemyManager.isPerformingAction = isPerformingAction;

        if (normalizedTime > 0f)
        {
            animator.CrossFade(animationName, normalizedTime);
        }
        else
        {
            animator.Play(animationName, 0, 0);
        }
    }
}
