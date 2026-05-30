using UnityEngine;
using UnityEngine.UIElements;

public class SuicideMovement : IMovementInput
{
    private Transform self;
    private Transform target;
    IMovementInput inputInterface;

    private float stopDistance = 1f;

    private bool stopped;

    public SuicideMovement(Transform self, Transform target)
    {
        this.self = self;
        this.target = target;
        inputInterface = this;
    }

    public Vector2 GetMoveInput()
    {
        if (stopped)
            return Vector2.zero;

        float distance = Vector2.Distance(self.position, target.position);

        if (distance <= stopDistance)
        {
            stopped = true;
            return Vector2.zero;
        }

        if (distance > 22)
        {
            inputInterface.RePosition(self,target);
        }

        return (target.position - self.position).normalized;
    }
}
