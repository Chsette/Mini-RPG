using System;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private Animator animator;

    private bool isMoving =>  GameManager.Instance.inputManager.MoveDirection != Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.inputManager.OnAttack += HandleAttackAnim;
        GameManager.Instance.inputManager.OnParry += HandleParryAnim;
    }

    private void Update()
    {
        UpdateAnimParams();
    }

    private void UpdateAnimParams()
    {
        animator.SetBool("isMoving", isMoving);
    }

    private void HandleAttackAnim()
    {
        animator.SetTrigger("attack");
    }

    private void HandleParryAnim(bool isBlocking)
    {
        animator.SetBool("isBlocking", isBlocking);
    }
}
