using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 _offset;
    
    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + _offset;
    }
}
