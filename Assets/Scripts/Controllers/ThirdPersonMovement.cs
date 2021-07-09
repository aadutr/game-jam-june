using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    
    CharacterCombat combat;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField]
    private float jumpSpeed, gravity;

    private bool playerGrounded;
    private Vector3 jumpDirection = Vector3.zero;

    int isRunningHash = Animator.StringToHash("isRunning");
    int isJumpingHash = Animator.StringToHash("isJumping");

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        

        if (direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * speed* Time.deltaTime);
        }

        playerGrounded = controller.isGrounded;

        // Jumping
        if(Input.GetButton("Jump") && playerGrounded)
        {
            jumpDirection.y = jumpSpeed;
        }
        jumpDirection.y -= gravity * Time.deltaTime;

        controller.Move(jumpDirection * Time.deltaTime);

        // Attacking, left mouse button
        if(Input.GetMouseButtonDown(0))
        {
            // Get CharacterStats of anything within range
            Collider[] hitEnemies = combat.EnemiesInRange();

            
            // Get enemy stats
            CharacterStats enemyStats = null;
            if (hitEnemies.Length != 0)
            {
                Collider enemy = hitEnemies[0];
                enemyStats = enemy.GetComponent<CharacterStats>();
            }            
            
            // Attack
            combat.Attack(enemyStats);            
        }

        //animations
        animator.SetBool(isRunningHash, direction.magnitude >= 0.1f);
        animator.SetBool(isJumpingHash, !controller.isGrounded);
    }

    
}
