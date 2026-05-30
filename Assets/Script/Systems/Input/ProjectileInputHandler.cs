using UnityEngine;

public class ProjectileInputHandler : MonoBehaviour, IMovementInput
{
    private IMovementInput movementStrategy;
    private ProjectileProfile profile;
    public Transform target;

    public void Init(ProjectileProfile profile, Transform target)
    {
        this.profile = profile;
        this.target = target;

        SetupMoveStrategy();
    }

    private void SetupMoveStrategy()
    {
        switch(profile.projectileName)
        {
            case "EnergyBall":
                movementStrategy = new ForwardMovement(gameObject.transform, target);
                break;
            case "Kunai":
                movementStrategy = new ForwardMovement(gameObject.transform, target);
                break;
        }
    }

    public Vector2 GetMoveInput()
    {
        return movementStrategy.GetMoveInput();
    }
}
