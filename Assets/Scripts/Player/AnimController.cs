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
        GameManager.Instance.inputManager.OnRun += HandleRunAnim;
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
        animator.SetBool("isMoving", false);
        animator.SetTrigger("attack");
    }

    private void HandleRunAnim(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    private void HandleParryAnim(bool isBlocking)
    {
        //animator.SetBool("isBlocking", isBlocking);
    }
}
