using UnityEngine;

public class EnemyInputHandler : MonoBehaviour, IMovementInput
{
    private IMovementInput movementStrategy;
    private EnemyType type;
    public Transform playerTarget;

    public void Init(EnemyType type, Transform playerTarget)
    {
        this.type = type;
        this.playerTarget = playerTarget;

        SetupMoveStrategy();
    }

    private void SetupMoveStrategy()
    {
        switch (type)
        {
            case EnemyType.Normal:
            case EnemyType.HighHP:
                movementStrategy = new ChaseMovement(transform, playerTarget);
                break;

            case EnemyType.ShootDistance:
                movementStrategy = new KeepDistanceMovement(transform, playerTarget);
                break;

            case EnemyType.RunCross:
                movementStrategy = new RunCrossMovement(transform, playerTarget);
                break;

            case EnemyType.Suicide:
                movementStrategy = new SuicideMovement(transform, playerTarget);
                break;
        }
    }    

    public Vector2 GetMoveInput()
    {
        return movementStrategy.GetMoveInput();
    }
}
