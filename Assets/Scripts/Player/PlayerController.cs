using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity = 10;
    [SerializeField] private float rotationvelocity = 10;

    private CharacterController characterController;

    private Vector3 moveDirection;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        GameManager.Instance.inputManager.OnParry += HandleParry;
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        Vector2 inputData = GameManager.Instance.inputManager.MoveDirection;
        moveDirection.x = inputData.x;
        moveDirection.z = inputData.y;
        Vector3 cameraRelativeMovement =
            ConvertMoveDirectionToCameraSpace(moveDirection);

        characterController.SimpleMove(cameraRelativeMovement *
                                 velocity);
        RotatePlayerAccordingToInput(cameraRelativeMovement);
    }

    private void RotatePlayerAccordingToInput(Vector3 cameraRelativeMovement)
    {
        Vector3 pointToLookAt;
        pointToLookAt.x = cameraRelativeMovement.x;
        pointToLookAt.y = 0;
        pointToLookAt.z = cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(pointToLookAt);

            transform.rotation =
                Quaternion.Slerp(currentRotation,
                                 targetRotation,
                                 rotationvelocity * Time.deltaTime);
        }

    }

    private Vector3 ConvertMoveDirectionToCameraSpace(Vector3 moveDirection)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 cameraForwardZProduct = cameraForward * moveDirection.z;
        Vector3 cameraRightXProduct = cameraRight * moveDirection.x;

        Vector3 directionToMovePlayer =
            cameraForwardZProduct + cameraRightXProduct;

        return directionToMovePlayer;
    }

    private void HandleParry(bool isBlocking)
    {
        print("Estou pulando!!");
    }
}
