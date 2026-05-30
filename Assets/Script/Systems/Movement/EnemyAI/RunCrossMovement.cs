using UnityEngine;

public class RunCrossMovement : IMovementInput
{
    private Transform self;
    private Transform target;
    IMovementInput inputInterface;

    private Vector2 moveDirection = Vector2.zero;
    private bool passedTarget = false;

    public RunCrossMovement(Transform self, Transform target)
    {
        this.self = self;
        this.target = target;
        inputInterface = this;
    }

    public Vector2 GetMoveInput()
    {
        float distance = Vector2.Distance(self.position, target.position);

        if (moveDirection == Vector2.zero && !passedTarget)
        {
            moveDirection = (target.position - self.position).normalized;
        } 
            
        if (!passedTarget && distance > 20f)
        {
            passedTarget = true;
        }

        if (passedTarget && distance > 20f)
        {
            inputInterface.RePosition(self, target);
            moveDirection = Vector2.zero;
            passedTarget = false;
        }

        return moveDirection;
    }
}
