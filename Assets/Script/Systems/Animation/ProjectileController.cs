using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private IMovementInput input;
    private MovementController movement;
    private Animator animator;
    private Projectile projectile;

    private void Awake()
    {
        input = GetComponent<IMovementInput>();
        movement = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
        projectile = GetComponent<Projectile>();
    }

    private void Update()
    {
        Vector2 move = input.GetMoveInput();
        movement.Move(move);

        UpdateAnimation(move);
    }

    private void UpdateAnimation(Vector2 move)
    {
        //Cho Kunai
        if (projectile != null && projectile.profile != null && projectile.profile.projectileName == "Kunai")
        {
            // Tính góc quay bằng Radian rồi đổi sang độ
            // Mathf.Atan2 nhận (y, x)
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

            // Trừ đi 90 độ vì Sprite gốc của bị lệch
            float finalAngle = angle - 90f;

            // Áp dụng góc xoay vào trục Z
            transform.rotation = Quaternion.Euler(0f, 0f, finalAngle);
        }
    }
}
