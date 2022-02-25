using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Mummy : MonoBehaviour
{
    public static event Action<Mummy>  Died;

    [SerializeField] float _attackRange = 1f;
    [SerializeField] int _health = 2;
    [SerializeField] public GameObject DieEffect;
    [SerializeField] public GameObject DamageEffect;
   

    int _currentHealth;

    NavMeshAgent _navmeshAgent;
    Animator _animator;

    bool Alive => _currentHealth > 0;

    private void Awake()
    {
        _currentHealth = _health;
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navmeshAgent.enabled = false;

    }

    void OnCollisionEnter(Collision collision)
    {
        var blasterShot = collision.collider.GetComponent<BlasterShot>();
        if (blasterShot != null)
        {
            _currentHealth--;
            if (_currentHealth <= 0)
            {
                Die();
                Instantiate(DieEffect, transform.position, Quaternion.identity);                
            }

            else
            {
                TakeHit();
                Instantiate(DamageEffect, transform.position, Quaternion.identity);
            }
        }
    }

    public void StartWalking()
    {
        _navmeshAgent.enabled = true;
        _animator.SetBool("Moving", true);
    }

    void TakeHit()
    {
        _navmeshAgent.enabled = false;
        _animator.SetTrigger("Hit"); 
    }

    void Die()
    {
        GetComponent<Collider>().enabled = false;
        _navmeshAgent.enabled = false;
        _animator.SetTrigger("Died");
        Died?.Invoke(this);
        Destroy(gameObject, 5f); 
    }
    
    private void Update()
    {
        if (!Alive)
            return;

        var player = FindObjectOfType<PlayerMovement>();
        if (_navmeshAgent.enabled)        
        _navmeshAgent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
            Attack();
    }
    void Attack()
    {
        _animator.SetTrigger("Attack");
        _navmeshAgent.enabled = false;
    }
    #region Animation Callbacks
    void AttackComplete()
    {
        if(Alive)
        _navmeshAgent.enabled = true;
    }
    void AttackHit()
    {
        Debug.Log("Killed Player");
        SceneManager.LoadScene(0);
    }

    void HitComplete()
    {
        if (Alive)
            _navmeshAgent.enabled = true;
    }
    #endregion
}
