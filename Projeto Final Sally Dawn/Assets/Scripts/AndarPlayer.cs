using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndarPlayer : MonoBehaviour
{
    private bool Kfrente, Ktras, Kjump, pular;
    public float moveSpeed = 10, jumpforce = 10;
    [Range(5, 15)]
    public float walkSpeed = 10;
    [Range(5, 15)]
    public float crawlSpeed = 7;
    [Range(15, 30)]
    public float runSpeed;
    [Range(0, 2)]
    public float currentSpeed = .5f;
    [Range(1,7)]
    public float fallMultiplier = 2.5f;
    [Range(1,7)]
    public float lowJumpMultiplier = 2f;
    private Rigidbody rb;
    private Animator Acao;
    private bool Kbaixo;
    private Vector3 Virar;
    private float JumpDelay=-1;
    [Range(0, 2)]
    public float MaxJumpDelay=.2f;
    private Vector3 GroundSize = new Vector3(1.16f, 1.5f,0);
    private Vector3 GroundCenter = new Vector3(0, -0.3f,0);
    public float horizontal = 0;
    public bool isGrounded;
    public LayerMask mask;
    private const int MaxJump = 1;
    private int currentJump = 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Acao = GetComponent<Animator>();
       
    }
    void Update()
    {
        Atualiza();
        Andar();
    }
    void Atualiza()
    {
        if (Input.GetKeyDown(KeyCode.D))
            Kfrente = true;
        if (Input.GetKeyDown(KeyCode.A))
            Ktras = true;
        if (Input.GetKeyDown(KeyCode.S))
            Kbaixo = true;
        if (Input.GetKeyDown(KeyCode.Space))
            Kjump = true;
        else
            Kjump = false;

        if (Input.GetKeyUp(KeyCode.D))
            Kfrente = false;
        if (Input.GetKeyUp(KeyCode.A))
            Ktras = false;
        if (Input.GetKeyUp(KeyCode.S))
            Kbaixo = false;


    }
    void Andar()
    {
        #region andar
        if (Kfrente)
            horizontal = 1;
        else if (Ktras)
            horizontal = -1;
        else
            horizontal = Mathf.MoveTowards(horizontal, 0, 0.15f);

        //if (horizontal == -1)
        //    transform.Rotate(Vector3.up * 18);
        //else if (horizontal == 1)
        //    transform.Rotate(Vector3.zero);


        if (horizontal != 0 && !Kbaixo)
            moveSpeed = Mathf.MoveTowards(moveSpeed, runSpeed, currentSpeed);
        else if (Kbaixo)
            moveSpeed = Mathf.MoveTowards(moveSpeed, crawlSpeed,currentSpeed);
        else
            moveSpeed = Mathf.MoveTowards(moveSpeed, walkSpeed, currentSpeed * 3.5f);

        Vector3 v = Vector3.right * horizontal * moveSpeed;
        v.y = rb.velocity.y;
        rb.velocity = v;


        Vector3 targetOlhar = transform.position;
        targetOlhar.x += horizontal * moveSpeed;
        transform.LookAt(targetOlhar);
        //Debug.Log(Virar == targetOlhar);
        //if (horizontal != 0)
        //    Acao.SetBool("Virar", Virar == targetOlhar);
        //Virar = transform.position;
        //Virar.x += horizontal * moveSpeed;

        #endregion
        #region Pulo
        var vel = rb.velocity;
        //Collider[] Grounds = Physics.OverlapBox(GroundCenter, GroundSize / 2, Quaternion.identity, mask);
        //isGrounded = Grounds != nulll;
        Vector3 origem = transform.position + GroundCenter;
        isGrounded = Physics.Raycast(origem, Vector3.down, GroundSize.y, mask);

        if (isGrounded) currentJump = 0;
        if (Kjump && !Kbaixo && (isGrounded || MaxJump > currentJump))
        {
            pular = true;
            currentJump++;
        }
        else
        {
            pular = false;
        }
        if (pular)    JumpDelay = 0;
        if (JumpDelay >= 0)
        {
            JumpDelay += Time.deltaTime;
            moveSpeed = 0;
        }
            if (JumpDelay > MaxJumpDelay)
        {
            moveSpeed = walkSpeed;
            JumpDelay = -1;
            vel.y = jumpforce;
            rb.velocity = vel;
        }
        Debug.Log(currentJump);
        if (vel.y < 0)                                              vel.y += Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (vel.y > 0 && !Input.GetButton("Jump"))             vel.y += Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
       
        rb.velocity = vel;
        #endregion
        #region Animaçoes
        Acao.SetBool("Agachado", Kbaixo);
        Acao.SetFloat("Queda", rb.velocity.y);
        Acao.SetBool("Pular", pular);
        Acao.SetFloat("Wspeed", horizontal * horizontal * moveSpeed / runSpeed);
        Acao.SetBool("IsGround", isGrounded);
        #endregion

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);

        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
            GroundSize);

        Gizmos.color = new Color(0, 0, 1, .9f);
        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
            new Vector2(0.01f, 1f));
        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
             new Vector2(1f, 0.01f));


    }

}
