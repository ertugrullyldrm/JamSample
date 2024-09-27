using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    public bool isJumping = false;
    public bool canJump = true;
    public bool isAttack1 = false;
    public bool canAttack1 = true;
    public bool isAttack2 = false;
    public bool canAttack2 = true;


    private CharacterController controller;
    private Animator anim;


    public LayerMask enemyLayers;
    public float attackRange = 0.5f;

    public Transform attackPointPlayer;
    public int attackFist = 20;


    //public Transform attackPoint2;
    public int attackKick = 30;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.A) && canAttack1)
        {
            StartCoroutine(FistAttackPlayer());
        }

        if (Input.GetKeyDown(KeyCode.D) && canAttack2)
        {
            StartCoroutine(KickAttackPlayer());
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            StartCoroutine(Jump());
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");


        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("idleWalkRun", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("idleWalkRun", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("idleWalkRun", 1, 0.1f, Time.deltaTime);
    }

    private IEnumerator Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
        anim.SetLayerWeight(anim.GetLayerIndex("Jump Layer"), 1);
        anim.SetTrigger("Jump");

        isJumping = true;
        canJump = false;
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float jumpDuration = stateInfo.length;

        StartCoroutine(EnableJump(jumpDuration));

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Jump Layer"), 0);
    }

    private IEnumerator EnableJump(float duration)
    {
        yield return new WaitForSeconds(duration);

        isJumping = false;
        canJump = true;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPointPlayer == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPointPlayer.position, attackRange);
    }

    private IEnumerator FistAttackPlayer()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("PlayerFistAttackLayer"), 1);
        anim.SetTrigger("PlayerFist1");

        isAttack1 = true;
        canAttack1 = false;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPointPlayer.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().DamageFist(attackFist);
        }

        AnimatorStateInfo stateInfo2 = anim.GetCurrentAnimatorStateInfo(0);
        float attackDuration = stateInfo2.length;

        StartCoroutine(EnableAttack1(attackDuration));

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("PlayerFistAttackLayer"), 0);
    }

    private IEnumerator EnableAttack1(float duration2)
    {
        yield return new WaitForSeconds(duration2);

        isAttack1 = false;
        canAttack1 = true;
    }

    private IEnumerator KickAttackPlayer()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("KickAttackPlayer Layer"), 1);
        anim.SetTrigger("KickAttackPlayer");

        isAttack2 = true;
        canAttack2 = false;

        Collider[] hitEnemies2 = Physics.OverlapSphere(attackPointPlayer.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies2)
        {
            enemy.GetComponent<Enemy>().DamageKick(attackKick);
        }

        AnimatorStateInfo stateInfo3 = anim.GetCurrentAnimatorStateInfo(0);
        float attackDuration2 = stateInfo3.length;

        StartCoroutine(EnableAttack2(attackDuration2));

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("KickAttackPlayer Layer"), 0);
    }

    private IEnumerator EnableAttack2(float duration3)
    {
        yield return new WaitForSeconds(duration3);

        isAttack2 = false;
        canAttack2 = true;
    }

}
