using UnityEngine;

public interface IMovementInput
{
    Vector2 GetMoveInput();
    public void RePosition(Transform self, Transform target)
    {
        self.position = CalculateNewPos(target);
    }

    private Vector3 CalculateNewPos(Transform target)
    {
        Vector2 dir = Random.insideUnitCircle.normalized;

        float distance = Random.Range(13, 20);

        Vector2 pos = (Vector2)target.position + dir * distance;

        return pos;
    }
}
