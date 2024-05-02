using UnityEngine;

public class PlayerController : Character
{
    [Header("Required components")]
    [SerializeField] private JoyStickController joyStickController;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform checkMovable;

    [Header("Attributes")]
    [SerializeField] private float speed;

    private Rigidbody rb;
    private string currentAnimName;
    private Vector3 movingDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentAnimName = "idle";
    }

    // Update is called once per frame
    void Update()
    {
        movingDir.x = joyStickController.horizontalInput;
        movingDir.z = joyStickController.verticalInput;
        HandleInput();
        InteractWithStep();
    }
    void FixedUpdate()
    {
        MoveCharacter(movingDir);
    }
    private void MoveCharacter(Vector3 direction)
    {
        Ray ray = new Ray(checkMovable.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //moving vector
            Vector3 moveAmount = direction.normalized * speed * Time.deltaTime;

            if (hit.collider.CompareTag("Stair"))
            {
                rb.isKinematic = true;
                //go up stair
                if (joyStickController.verticalInput > 0 && isValidStep == true)
                {
                    moveAmount.y = moveAmount.y + 1;
                }
            }
            if (hit.collider.CompareTag("Ground"))
            {
                rb.isKinematic = false;
            }

            rb.MovePosition(rb.position + moveAmount);
        }
        //rotate player
        if (joyStickController.horizontalInput != 0 && joyStickController.verticalInput != 0)
        {
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                ChangeAnim("runing");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                ChangeAnim("idle");
            }
        }
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

}
