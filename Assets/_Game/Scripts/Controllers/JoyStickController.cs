using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    public float horizontalInput, verticalInput;
    [SerializeField] private GameObject joystick;
    [SerializeField] private RectTransform bg, knob;
    [SerializeField] private float knobRange;
    private Vector2 startPos, currentPos;
    private Vector2 screen;
    private Vector2 touchPos;

    private void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;
    }
    private void OnEnable()
    {
        horizontalInput = 0f;
        verticalInput = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position - screen / 2;
            //first touch
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touchPos;
                joystick.SetActive(true);
                bg.anchoredPosition = startPos;
            }

            //on move
            if (touch.phase == TouchPhase.Moved)
            {
                currentPos = touchPos;
                //calculate postion of knob
                knob.anchoredPosition = Vector2.ClampMagnitude((currentPos - startPos), knobRange) + startPos;

                Vector3 direction = (currentPos - startPos).normalized;
                direction.z = direction.y;
                direction.y = 0;

                //update input
                horizontalInput = direction.x;
                verticalInput = direction.z;
            }

            //stop
            if (touch.phase == TouchPhase.Ended)
            {
                joystick.SetActive(false);
                horizontalInput = 0f;
                verticalInput = 0f;
            }
        }
    }
}
