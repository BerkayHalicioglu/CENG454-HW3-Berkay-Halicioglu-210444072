using UnityEngine;

public class DirectMovement : MonoBehaviour, IMovementStrategy
{
    public void Move(Transform mover, Transform target, float speed)
    {
        Vector2 direction = (target.position - mover.position).normalized;
        mover.position = Vector2.MoveTowards(
            mover.position,
            target.position,
            speed * Time.deltaTime
        );
    }
}