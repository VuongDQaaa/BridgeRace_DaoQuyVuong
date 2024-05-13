using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : Character
{
    private IState currentState;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Animator anim;
    private AnimationState currentAnimationState;
    public List<GameObject> targetBricks;
    private int currentTargetIndex;
    [SerializeField] private Transform finishTarget;
    void Awake()
    {
        finishTarget = GameObject.Find("Finish").transform;
        UpdateCharacterColor(myColor);
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        if (currentStage != null)
        {
            targetBricks = currentStage.spawnedBricks.FindAll(brick => brick.GetComponent<Brick>().brickColor == myColor);
        }
        CheckStepColor();
    }

    public void OnIdle()
    {
        ChangeAnim(AnimationState.idle);
    }

    public void OnCollecting()
    {
        ChangeAnim(AnimationState.runing);
        agent.destination = GetNearestBrick().position;
    }

    public void OnClimping()
    {
        ChangeAnim(AnimationState.runing);
        agent.destination = finishTarget.position;
    }

    public void OnWining()
    {
        ChangeAnim(AnimationState.win);
    }

    private Transform GetNearestBrick()
    {
        GameObject closestBrick = null;
        float nearestTarget = Mathf.Infinity;
        foreach (GameObject brick in targetBricks)
        {
            float distance = Vector3.Distance(transform.position, brick.transform.position);
            if (distance < nearestTarget)
            {
                nearestTarget = distance;
                closestBrick = brick;
            }
        }
        return closestBrick.transform;
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    private void ChangeAnim(AnimationState animationState)
    {
        if (currentAnimationState != animationState)
        {
            anim.ResetTrigger(animationState.ToString());
            currentAnimationState = animationState;
            anim.SetTrigger(currentAnimationState.ToString());
        }
    }
}
