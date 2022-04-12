using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _player;
    [SerializeField] private float _lookForward;
    private float _distance;
    private float _minX;
    private float _currentX;


    private void Awake()
    {
        _minX = transform.position.x;
    }
    void Update()
    {
        if (_player.position.x + _distance < _minX) _currentX = _minX;
        else _currentX = _player.position.x + _distance;
        transform.position = new Vector3(_currentX, transform.position.y, transform.position.z);
        _distance = Mathf.Lerp(_distance,_lookForward * _player.localScale.x, _speed * Time.deltaTime);
    }
}
