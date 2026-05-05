using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovementInput input;
    private MovementController movement;
    private Animator animator;

    // Cache các ID của Parameter để tối ưu hiệu năng thay vì dùng String
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int LastMoveXHash = Animator.StringToHash("LastMoveX");
    bool lastMoveX;

    private void Awake()
    {
        input = GetComponent<IMovementInput>();
        movement = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 move = input.GetMoveInput();
        movement.Move(move);

        UpdateAnimation(move);
    }

    private void UpdateAnimation(Vector2 move)
    {
        if (animator == null) return;

        // Kiểm tra đang di chuyển không
        // Sử dụng ngưỡng nhỏ (0.01f) để tránh nhiễu từ Joystick
        bool isMoving = move.sqrMagnitude > 0.01f;

        animator.SetBool(IsMovingHash, isMoving);
        if (move.x > 0)
        {
            lastMoveX = true;
        }
        else if (move.x < 0)
        {
            lastMoveX = false;
        }    

        if (isMoving)
        {
            // Cập nhật hướng di chuyển
            animator.SetFloat(MoveXHash, move.x);
            animator.SetFloat(MoveYHash, move.y);
            animator.SetBool(LastMoveXHash, lastMoveX);
        }
    }
}