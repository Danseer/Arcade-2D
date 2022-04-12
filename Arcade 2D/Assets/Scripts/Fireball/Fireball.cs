using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _speed=2;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private bool _hit=false;
    private float _direction;
    private float _lifeTimer;
        
    void Awake()
    {
        InitComponent();
    }

    void Update()
    {
        if (_hit)
            return;

        MoveFireball();
        _lifeTimer += Time.deltaTime;
        if (_lifeTimer > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void MoveFireball()
    {
        transform.Translate(_speed * Time.deltaTime * _direction, 0, 0);
    }

    private void InitComponent()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        _boxCollider.enabled=false;
        _animator.SetTrigger("expos");
    }

    public void Direction(float direction)
    {
        _lifeTimer = 0;
        _direction = direction;
        gameObject.SetActive(true);
        _hit = false;
        _boxCollider.enabled = true;

        float thisDirect = Mathf.Sign(transform.localScale.x);

        if (thisDirect != direction)
        {
            thisDirect = direction;
        }

        transform.localScale = new Vector3(thisDirect, transform.localScale.y, transform.localScale.z);

    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
