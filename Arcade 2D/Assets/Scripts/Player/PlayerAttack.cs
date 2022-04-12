using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;
    private float _timeForNewAttack=0;
    private float _timerForAttack=0.3f;

    [SerializeField] private Transform _firepoint;
    [SerializeField] private Fireball[] _fireballHolder;

    private bool _attack;


    void Awake()
    {
        InitComponent();
    }

    void Update()
    {
        //if(Input.GetMouseButtonDown(0) && _playerController.CanAttack() && CanNewAttack())
        if (_attack && _playerController.CanAttack() && CanNewAttack())
        {
            Attack();
            _timeForNewAttack = 0;
        }


        UpdateAttackTime(); 
       
    }

    private void InitComponent()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    private bool CanNewAttack()//pause between firebols
    {
        return (_timeForNewAttack > _timerForAttack);
    }

    private void UpdateAttackTime()//pause between firebols
    {
        _timeForNewAttack += Time.deltaTime;
    }

    private void Attack()
    {
        _animator.SetTrigger("attack");
        int number = FindFireInArray();
        _fireballHolder[number].transform.position = _firepoint.position;//set start position fireball
        _fireballHolder[number].GetComponent<Fireball>().Direction(Mathf.Sign(transform.localScale.x));//set direction fireball
    }

    private int FindFireInArray()//find in pool object
    {

        for (int i=0; i<_fireballHolder.Length;i++)
        {
            if(!_fireballHolder[i].isActiveAndEnabled)
            {
                return i;
            }
        }
        return 0;
    }

    public void PushFireButton()
    {
        _attack = true;
    }

    public void PushUpFireButton()
    {
        _attack = false;
    }
}
