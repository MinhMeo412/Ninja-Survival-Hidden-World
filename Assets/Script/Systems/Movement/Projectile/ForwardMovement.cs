using UnityEngine;

public class ForwardMovement : IMovementInput
{
    private Transform self;
    private Transform target;
    private Vector2 fixedDirection;

    public ForwardMovement(Transform self, Transform target)
    {
        this.self = self;
        this.target = target;

        fixedDirection = (target.position - self.position).normalized;
    }

    public Vector2 GetMoveInput()
    {
        return fixedDirection;
    }
}
