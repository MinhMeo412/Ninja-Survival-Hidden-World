using Unity.VisualScripting;
using UnityEngine;

public class ChaseMovement : IMovementInput
{
    private Transform self;
    private Transform target;
    IMovementInput inputInterface;

    public ChaseMovement(Transform self, Transform target)
    {
        this.self = self;
        this.target = target;
        inputInterface = this;
    }

    public Vector2 GetMoveInput()
    {
        float distance = Vector2.Distance(self.position, target.position);
        if (distance > 22)
        {
            inputInterface.RePosition(self, target);
        }

        Vector2 dir = (target.position - self.position).normalized;
        return dir;
    }
}
