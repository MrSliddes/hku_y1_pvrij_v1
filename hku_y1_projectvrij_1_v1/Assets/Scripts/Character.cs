using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterType characterType;
    public bool isAlive = true;
    public GameObject UIHead;
    public float characterSpeed = 5f;
    public float gravity = 3;
    public bool isControlled = false;
    public CharacterState state;
    private bool hasEnteredNewState = false;

    public bool isGrounded = true;
    public float distanceToGround;
    public float slopeSnapSmoothness = 1f;
    public bool canMove = true;

    public int trowableAmount;
    public GameObject trowable;
    public Transform trowOrgin;

    public SpriteRenderer characterSprite;

    public bool isHidden = false;

    [Header("Arch")]
    private LineRenderer lineRenderer;
    public int lineSegment = 10;
    private Camera cam;
    public LayerMask layer;
    public GameObject cursor;

    private Rigidbody rb;
    private Vector3 moveDir;
    private CharacterController controller;
    private ManagerUI managerUI;

    [Header("Monster Bite")]
    public Animator monsterBiteAnim;

    [Header("Animation")]
    public string animNameIdle;
    public string animNameWalking;
    public string animNameDead;
    private Animator characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();
        characterAnimator = GetComponent<Animator>();
        managerUI = FindObjectOfType<ManagerUI>();

        cam = Camera.main;
        lineRenderer.positionCount = lineSegment;

        EnterNewState(CharacterState.idle);

        if(Application.isEditor == false)
        {
            trowableAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update character state
        switch(state)
        {
            case CharacterState.idle:
                if(!hasEnteredNewState)
                {
                    hasEnteredNewState = true;
                    characterAnimator.Play(animNameIdle);
                }

                // Exit
                if(PlayerPressedInput())
                {
                    EnterNewState(CharacterState.walking);
                }
                break;
            case CharacterState.walking:
                if(!hasEnteredNewState)
                {
                    hasEnteredNewState = true;
                    if(isControlled) characterAnimator.Play(animNameWalking);
                }

                // Exit
                if(!PlayerPressedInput())
                {
                    EnterNewState(CharacterState.idle);
                }
                break;
            case CharacterState.death:
                if(!hasEnteredNewState && isAlive)
                {
                    hasEnteredNewState = true;
                    monsterBiteAnim.Play("MonsterBite");
                    characterAnimator.Play(animNameIdle);
                    StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<Player>().ShakeScreen(0.3f));
                    isAlive = false;
                    lineRenderer.enabled = false;
                    cursor.SetActive(false);
                    characterSprite.flipX = false;
                    managerUI.Pickup(false);
                    UIHead.SetActive(false);
                }

                // Check anim finished
                if(monsterBiteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && monsterBiteAnim.GetCurrentAnimatorStateInfo(0).IsName("MonsterBite"))
                {
                    // Finished
                    characterAnimator.enabled = false;
                    characterSprite.gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
                    characterSprite.gameObject.transform.localPosition = new Vector3(0, 1.73f, 0);
                }
                break;
            default:
                break;
        }

        if(!isControlled)
        {
            lineRenderer.enabled = false;
        }

        if(!isControlled) return;
        if(!isAlive) return;
        if(!canMove) return;

        // Movement
        moveDir = Vector3.zero;
        if(controller.isGrounded)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            // Flip sprite
            if(h == 1)
            {
                characterSprite.flipX = false;
            }
            else if(h == -1)
            {
                characterSprite.flipX = true;
            }
            moveDir = (v * Vector3.forward + h * Vector3.right).normalized;
            moveDir *= characterSpeed;
        }
        moveDir.y -= gravity;
        CheckGround();

        ControllCharacter();

        // Trowing
        if(!isHidden) LaunchProjectile();
        if(isHidden)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            if(Input.GetMouseButtonDown(1))
            {
                if(trowableAmount > 0)
                    lineRenderer.enabled = true;
            }
        }

        // Target
        Target.show = lineRenderer.enabled;

        // Enable/Disable cursor
        cursor.SetActive(lineRenderer.enabled);
    }

    private void FixedUpdate()
    {
        if(!isControlled) return;        
    }

    private void EnterNewState(CharacterState newState)
    {
        state = newState;
        hasEnteredNewState = false;
    }

    private void ControllCharacter()
    {
        //rb.MovePosition(transform.position + moveDir * characterSpeed * Time.fixedDeltaTime);
        controller.Move(moveDir * Time.deltaTime);
    }

    private void CheckGround()
    {
        //isGrounded = Physics.Raycast(transform.position + Vector3.up, -Vector3.up, 1.1f);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            distanceToGround = hit.distance;
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
        }
        if(distanceToGround > 0.2f + 0.5f)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, hit.point.y + 0.001f, transform.position.z), slopeSnapSmoothness * Time.deltaTime);
    }

    #region Trow

    private void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(camRay, out hit, 100f, layer))
        {
            //lineRenderer.enabled = true;
            if(cursor != null)
            {
                cursor.transform.position = hit.point + Vector3.up * 0.1f;
                cursor.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }

            Vector3 vo = CalculateVelocity(hit.point, trowOrgin.position, 1f);

            Visualize(vo);

            if(Input.GetMouseButtonUp(1) && trowableAmount > 0)
            {
                // Trow it
                trowableAmount--;
                GameObject a = Instantiate(trowable, trowOrgin.position, Quaternion.identity);
                a.GetComponent<Rigidbody>().velocity = vo;
                lineRenderer.enabled = false;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private Vector3 CalculateVelocity(Vector3 target, Vector3 orgin, float time)
    {
        Vector3 distance = target - orgin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz * time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }

    private Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0;

        Vector3 result = trowOrgin.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + trowOrgin.position.y;

        result.y = sY;
        return result;
    }

    private void Visualize(Vector3 vo)
    {
        for(int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineRenderer.SetPosition(i, pos);
        }
    }

    #endregion

    public void HideCharacter(bool hide)
    {
        isHidden = hide;

        if(hide)
        {
            rb.useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;
            characterSprite.enabled = false;
            GetComponent<LineRenderer>().enabled = false;
            canMove = false;
            cursor.SetActive(false);
        }
        else
        {
            rb.useGravity = true;
            GetComponent<CapsuleCollider>().enabled = true;
            characterSprite.enabled = true;
            //GetComponent<LineRenderer>().enabled = true;
            canMove = true;
        }
    }

    public void TakeDamage()
    {
        if(!isHidden)
        {
            EnterNewState(CharacterState.death);

            switch(characterType)
            {
                case CharacterType.vader:
                    Player.charVaderIsAlive = false;
                    break;
                case CharacterType.zus:
                    Player.charBroerIsAlive = false;
                    break;
                case CharacterType.broer:
                    Player.charZusIsAlive = false;
                    break;
                default:
                    break;
            }
        }
    }

    private bool PlayerPressedInput()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }
}

public enum CharacterState
{
    idle,
    walking,
    death
}

public enum CharacterType
{
    vader,
    zus,
    broer
}
