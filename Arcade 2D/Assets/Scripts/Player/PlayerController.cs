using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed=5;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private float _horizontalInput;
    private Animator _animator;

    private bool _jump;
    private bool _go;
    
    private void Awake()
    {
        InitComponents();
    }

    private void Update()
    {
        Move();

        CheckOnTheWall();

        Direction();

        Jump();

        AnimatorLogic();

    }

    private void InitComponents()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Move()
    {
#if UNITY_EDITOR
        //_horizontalInput = Input.GetAxis("Horizontal");
#endif      

        _rigidbody.velocity = new Vector2(_horizontalInput * _moveSpeed, _rigidbody.velocity.y);//move
    }

    private void AnimatorLogic()
    {
        _animator.SetBool("run", _horizontalInput != 0);//switch anim idle<->walk
        _animator.SetBool("grounded", IsGrounded());//on the ground, if ground - jump access
    }

    private void Jump()
    {

        //if (Input.GetKey(KeyCode.Space) && !IsGrounded() && OnWall())//Jump if player on the wall

        if (_jump && !IsGrounded() && OnWall())//Jump if player on the wall
        {
            if (_horizontalInput == 0) //jump from the wall
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                _rigidbody.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x),10), ForceMode2D.Impulse);
            }
            else //jump up for the wall
            {
                _rigidbody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 40, 7);
            }

        }


         //if (Input.GetKey(KeyCode.Space) && IsGrounded())//Jump if player on the ground

            if (_jump && IsGrounded())//Jump if player on the ground
            {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _moveSpeed * 1.5f);
             _animator.SetTrigger("jump");

        }

    }

    private void CheckOnTheWall()
    {
        if (!IsGrounded() && OnWall()) //freezed graxity if on the wall
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
        }
        else _rigidbody.gravityScale = 1.5f;//unfreezed
    }

    private void Direction()
    {
        if (_horizontalInput > 0)//direction face
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool IsGrounded()//on the ground
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, Vector2.down, 0.1f,_groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()//on the wall
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, _wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return (_horizontalInput == 0 && !OnWall() && IsGrounded());
    }

    public void PushRightButton()
    {
        _horizontalInput = 1;
    }

    public void PushLeftButton()
    {
        _horizontalInput = -1;
    }

    public void PushUpLRButton()
    {
        _horizontalInput = 0;
    }

    public void PushJumpButton()
    {
        _jump = true;
    }

    public void PushUpJumpButton()
    {
        _jump = false;
    }








}
