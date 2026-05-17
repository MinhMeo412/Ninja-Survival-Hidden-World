using UnityEngine;

public class MoveStats : MonoBehaviour
{
    public float moveSpeed { get; private set; }

    public void Init(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
