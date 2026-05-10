using UnityEngine;

public class ZigzagMovement : MonoBehaviour, IMovementStrategy
{
    private float zigzagAmplitude = 2f;
    private float zigzagFrequency = 2f;

    public void Move(Transform mover, Transform target, float speed)
    {
        Vector2 toTarget = (target.position - mover.position).normalized;
        Vector2 perpendicular = new Vector2(-toTarget.y, toTarget.x);

        float offset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector2 zigzagDir = (toTarget + perpendicular * offset).normalized;

        mover.position = (Vector2)mover.position + zigzagDir * speed * Time.deltaTime;
    }
}