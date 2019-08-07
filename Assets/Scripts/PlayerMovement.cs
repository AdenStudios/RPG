using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Movement Variables
    private float moveSpeed = 5.0f;
    private float normalSpeed = 5.0f;
    private float sprintSpeed = 8.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;

    private CharacterController cController;
    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    public Joystick joystick;
    public Joystick cameraJoyStick;

    private float directionY;
    private float directionX;

    private float desiredRotation = 0f;

    private float sprintStaminaUsage = 6;
    private bool isSprinting = false;

    // Camera Variables
    [SerializeField] GameObject cameraArm;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private float sensitivityX = 0.20f;
    private float sensitivityY = 0.20f;

    private float minimumX = -360.0f;
    private float maximumX = 360.0f;
    private float minimumY = -60.0f;
    private float maximumY = 60.0f;

    private Player player;
    private PlayerCombat playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cController = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    CameraMovement(touch.fingerId);
                }
            }
        }

        //KeyboardMovement();
        TouchMovement();
        SetAnimatorMovementValues();
        if (isSprinting)
        {
            if (player.playerStamina > 0)
            {
                player.UseStamina(sprintStaminaUsage);
            }
            else
            {
                DisableSprint();
            }           
        }
    }

    private void TouchMovement()
    {
        if (cController.isGrounded && !GetComponent<PlayerCombat>().playerAttacking)
        {
            float h = joystick.Horizontal;
            float v = joystick.Vertical;

            if (h != 0 || v != 0)
            {
                float heading = Mathf.Atan2(joystick.Horizontal,joystick.Vertical);
                transform.rotation =  Quaternion.Euler(0f, heading * Mathf.Rad2Deg + cameraArm.transform.eulerAngles.y, 0f);

                moveDirection.x = transform.forward.x * moveSpeed;
                moveDirection.z = transform.forward.z * moveSpeed;           
            }
            else 
            {
                moveDirection.x = 0;
                moveDirection.z = 0;
            }
        }
        else
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
        }

    
        moveDirection.y -= gravity * Time.deltaTime;
        cController.Move(moveDirection * Time.deltaTime);
        // Make the Camera Arm follow the player
        Vector3 myPos = transform.position;
        Vector3 camTargetPos = new Vector3(myPos.x, myPos.y + 1, myPos.z);
        // Smoother follow and camera will lag slightly behind
        cameraArm.transform.position = Vector3.Lerp(cameraArm.transform.position, camTargetPos, 0.2f);

        if (cController.velocity.x != 0 || cController.velocity.z != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    public void TouchJump()
    {
        if (cController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            anim.SetTrigger("Jump");
        }
    }

    public void EnableSprint()
    {
        isSprinting = true;
        anim.speed = 1.2f;
        moveSpeed = sprintSpeed;
    }

    public void DisableSprint()
    {
        isSprinting = false;
        anim.speed = 1.0f;
        moveSpeed = normalSpeed;
    }

    private void KeyboardMovement()
    {
        // If player is grounded and not attacking get rotation values and run MoveWithSmoothRotation method.
        // W key to run forwards will always match the direction the camera is facing,
        // the other keys will rotate the player to run forwards in a different direction to the camera
        if (cController.isGrounded && !GetComponent<PlayerCombat>().playerAttacking) 
        {
           if (Input.GetKey(KeyCode.W))
            {        
                if (Input.GetKey(KeyCode.A))
                {
                    desiredRotation = 315;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    desiredRotation = 45;
                }
                else
                {
                    desiredRotation = 0;
                }
                MoveWithSmoothRotation();
            }

            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    desiredRotation = 225;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    desiredRotation = 135;
                }
                else
                {
                    desiredRotation = 180;
                }
                MoveWithSmoothRotation();
            }
        
            else if (Input.GetKey(KeyCode.A))
            {
                desiredRotation = 270;
                MoveWithSmoothRotation();
            }
        
            else if (Input.GetKey(KeyCode.D))
            {
                desiredRotation = 90;
                MoveWithSmoothRotation();
            }
            else
            {
                moveDirection.z = 0;
                moveDirection.x = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Jump");
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed = normalSpeed;
                anim.speed = 1f;
            }            

        }
        if (Input.GetKey(KeyCode.LeftShift) && cController.isGrounded)
        {
            if (player.playerStamina > 0)
            {
                player.UseStamina(sprintStaminaUsage);
                moveSpeed = sprintSpeed;  
                anim.speed = 1.2f;   
            }
            else
            {
                moveSpeed = normalSpeed;
                anim.speed = 1f;     
            }   
        }
        if (cController.isGrounded && GetComponent<PlayerCombat>().playerAttacking)
        {
            moveDirection.z = 0;
            moveDirection.x = 0;
        }
        if (cController.velocity.x != 0 || cController.velocity.z != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        // Move the player
        cController.Move(moveDirection * Time.deltaTime);
        // Make the Camera Arm follow the player
        Vector3 myPos = transform.position;
        Vector3 camTargetPos = new Vector3(myPos.x, myPos.y + 1, myPos.z);
        // Smoother follow and camera will lag slightly behind
        cameraArm.transform.position = Vector3.Lerp(cameraArm.transform.position, camTargetPos, 0.08f);

    }
    // Smoothly Rotates the Player with local values in comparison to which way the camera is facing
    private void MoveWithSmoothRotation()
    {
        Vector3 myCurrentRot = transform.eulerAngles;
        // add the the desired rotation on top of the cameras rotation
        float finalTargetRotValue = cameraArm.transform.eulerAngles.y + desiredRotation;
        // Gradually increase the rotation towards for a smoother rotation
        float currentRotValue = Mathf.MoveTowardsAngle(transform.eulerAngles.y, finalTargetRotValue, 250f * Time.deltaTime);
        myCurrentRot.y = currentRotValue;
        transform.eulerAngles = myCurrentRot;

        // Set moveDirection values from the Transform.forward vector so player runs forward the ways he's facing
        moveDirection.x = transform.forward.x * moveSpeed;
        moveDirection.z = transform.forward.z * moveSpeed;   
    }
    // Pass Movement Directions to the Animator Floats for the blend tree
    private void SetAnimatorMovementValues()
    {
        float newX = Mathf.MoveTowards(anim.GetFloat("MoveX"), directionX, 3f * Time.deltaTime);
        float newY = Mathf.MoveTowards(anim.GetFloat("MoveY"), directionY, 3f * Time.deltaTime);
        anim.SetFloat("MoveX", newX);
        anim.SetFloat("MoveY", newY);
    }
    // Move the Camera, Camera will aways rotate with the mouse
    private void CameraMovement(int id)
    {
        rotationX += Input.GetTouch(id).deltaPosition.x * sensitivityX;
        rotationY -= Input.GetTouch(id).deltaPosition.y * sensitivityY;

        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);

        cameraArm.transform.eulerAngles = new Vector3(rotationY, rotationX, 0);   
    }

    // Clamp the angles to prevent continuous spin
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

    private bool IsUIActive()
    {
        return (UIManager.isUIActive);
    }
}
