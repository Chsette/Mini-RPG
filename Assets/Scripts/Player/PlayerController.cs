using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity;

    private Vector2 moveDirection;
    
    private void Start()
    {
        GameManager.Instance.inputManager.OnJump += HandleOnJump;
    }

    private void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        moveDirection = GameManager.Instance.inputManager.MoveDirection;
        print(moveDirection);
    }

    private void HandleOnJump()
    {
        print("Estou pulando!!");
    }
}
