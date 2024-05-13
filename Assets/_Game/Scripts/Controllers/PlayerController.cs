using UnityEngine;

public class PlayerController : Character
{
    [Header("Required components")]
    [SerializeField] private Animator anim;

    [Header("Attributes")]
    [SerializeField] private float speed;
    private Vector3 currentRotation;
    private AnimationState currentAnimationState;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = Vector3.forward;
        currentAnimationState = AnimationState.idle;
    }

    void Update()
    {
        if (JoyStickController.direction != Vector3.zero)
        {
            currentRotation = JoyStickController.direction;
        }
        MoveCharacter();
        CheckStepColor();

        if(isFinished == true)
        {
            ChangeAnim(AnimationState.win);
        }
    }

    private void MoveCharacter()
    {
        if (Input.GetMouseButton(0))
        {
            if (JoyStickController.direction != Vector3.zero)
            {
                Vector3 newPostion = transform.position + JoyStickController.direction * speed * Time.deltaTime;
                if (isOnstair == true && JoyStickController.verticalInput > 0)
                {
                    if (isMoveable == true)
                    {
                        transform.position = CheckGrounded(newPostion);
                    }
                }
                else
                {
                    transform.position = CheckGrounded(newPostion);
                }
                ChangeAnim(AnimationState.runing);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim(AnimationState.idle);
        }

        //rotate player
        transform.rotation = Quaternion.LookRotation(currentRotation);
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
