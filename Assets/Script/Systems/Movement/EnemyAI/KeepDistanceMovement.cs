using UnityEngine;

public class KeepDistanceMovement : IMovementInput
{
    private Transform self;
    private Transform target;
    IMovementInput inputInterface;

    private float minDistance = 6f;
    private float maxDistance = 9f;

    private float desiredDistance;

    private float nextChangeTime = 0f;
    private float changeDelay = 5f;

    private bool isMoving = false;
    private Vector2 destinationPoint;
    private Vector2 moveDirection;

    public KeepDistanceMovement(Transform self, Transform target)
    {
        this.self = self;
        this.target = target;
        inputInterface = this;

        desiredDistance = Random.Range(minDistance, maxDistance);
    }
    public Vector2 GetMoveInput()
    {
        float currentDistanceToTarget = Vector2.Distance(self.position, target.position);

        if (currentDistanceToTarget > 22)
        {
            inputInterface.RePosition(self, target);
            isMoving = false;
            moveDirection = Vector2.zero;
        }

        if (isMoving)
        {
            if (Vector2.Distance(self.position, destinationPoint) <= 0.1f)
            {
                isMoving = false;
                nextChangeTime = GameplayTimer.Instance.currentTime + changeDelay;
                return Vector2.zero;
            }

            return moveDirection;
        }

        if (GameplayTimer.Instance.currentTime >= nextChangeTime)
        {
            //Target quá gần -> Chạy ra xa
            if (currentDistanceToTarget < minDistance)
            {
                desiredDistance = Random.Range(minDistance, maxDistance);
                moveDirection = (self.position - target.position).normalized;
                destinationPoint = (Vector2)target.position + (moveDirection * desiredDistance);
                isMoving = true;
                return moveDirection;
            }

            //Target quá xa -> Lại gần
            if (currentDistanceToTarget > desiredDistance)
            {
                desiredDistance = Random.Range(minDistance, maxDistance);
                moveDirection = (target.position - self.position).normalized;
                destinationPoint = (Vector2)target.position - (moveDirection * desiredDistance);
                isMoving = true;
                return moveDirection;
            }
        }

        // Đúng khoảng cách
        return Vector2.zero;
    }
}
