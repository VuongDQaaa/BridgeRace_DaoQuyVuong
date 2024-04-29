using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Character
{
    [Header("Required components")]
    [SerializeField] private JoyStickController joyStickController;
    [SerializeField] private Animator anim;

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
        HandleInput();
    }
    void FixedUpdate()
    {
        MoveCharacter();
        ClimpStair();
    }
    private void MoveCharacter()
    {
        movingDir.x = joyStickController.horizontalInput;
        movingDir.z = joyStickController.verticalInput;

        rb.velocity = movingDir.normalized * speed;

        //rotate player
        if (joyStickController.horizontalInput != 0 && joyStickController.verticalInput != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
    private void ClimpStair()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.1f))
        {
            if (hit.collider.CompareTag("Step"))
            {
                //Go up
                if (joyStickController.verticalInput > 0)
                {
                    //Debug.Log("hit point y:" + hit.point.y);
                    Vector3 climpPostion = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                    transform.position = Vector3.Lerp(transform.position, climpPostion, speed);
                }
            }
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
