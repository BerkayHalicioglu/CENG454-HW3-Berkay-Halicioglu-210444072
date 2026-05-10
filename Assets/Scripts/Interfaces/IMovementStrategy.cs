using UnityEngine;

public interface IMovementStrategy
{
    void Move(Transform mover, Transform target, float speed);
}