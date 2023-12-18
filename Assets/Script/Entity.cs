using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region 变量
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallcheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    #endregion

    #region 组件
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public EntityFX fx { get; private set; }

    #endregion

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDiraction;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;







    #region 朝向数据
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        //获取组件
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Damage()
    {
        fx.StartCoroutine(fx.FlashFX());
        StartCoroutine(HitKnockback());
    }

    #region 受击
    protected virtual IEnumerator HitKnockback()
    {
        //受击后退停滞
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDiraction.x * -facingDir, knockbackDiraction.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
    #endregion


    #region 速度
    public void ZeroVelocity()
    {
        if (isKnocked)
            return;
        rb.velocity = Vector2.zero;

    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion


    #region 地面检测、墙面检测
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallcheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    
    protected virtual void OnDrawGizmos() //检测用画线
    {
        Gizmos.DrawLine(groundCheck.position,new Vector2(groundCheck.position.x,groundCheck.position.y - groundCheckDistance)); 
        Gizmos.DrawLine(wallcheck.position,new Vector2(wallcheck.position.x + wallCheckDistance,wallcheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region 翻转
    public virtual void Flip()//翻转朝向
    {
        facingRight = !facingRight;
        facingDir *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    public virtual void FlipController(float _x)
    {
        if (facingRight && _x < 0)
            Flip();
        else if (!facingRight && _x > 0)
            Flip();
    }
    #endregion


}
