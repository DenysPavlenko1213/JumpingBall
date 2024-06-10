using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    private void Start() => offset = transform.position - player.position;
    private void Update() => transform.position = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
}
