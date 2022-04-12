using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _startingHealth;
    private Animator _animator;
    private PlayerController _playerController;
    private bool _isDead;
    private float _currentHealth;

    private SpriteRenderer _renderer;


    internal float GetCurrentHealth()
    {
        return _currentHealth;
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _renderer = GetComponent<SpriteRenderer>();
        _currentHealth = _startingHealth;
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth > 0)
        {
            _animator.SetTrigger("pain");
            StartCoroutine(Invurn());
        }
        else if (!_isDead)
        {
            _isDead = true;
            _animator.SetTrigger("die");
        }
    }

    public void AddHealth(float health)
    {
         _currentHealth = Mathf.Clamp(_currentHealth + health, 0, _startingHealth);
    }

    IEnumerator Invurn()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < 7; i++)
        {
            _renderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.2f);
            _renderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);

        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
    
}
