using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    public MoveStats stats;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<MoveStats>();   
    }

    public void Move(Vector2 input)
    {
        rb.linearVelocity = input * stats.moveSpeed;
    }
}