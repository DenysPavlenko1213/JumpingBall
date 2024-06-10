using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private int speed;
    private void FixedUpdate() => transform.Rotate(0, speed * Time.fixedDeltaTime, 0);
}
