
using UnityEngine;


public class SawSideway : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    private float _min, _max;
    private bool _forward=true;

 
    void Start()
    {
        _min = transform.position.x - _distance;
        _max = transform.position.x + _distance;
    }

   
    void Update()
    {
        SwithDirections();

    }

    private void SwithDirections()
    {
        if (_forward)
        {
            if (transform.position.x <= _max)
                transform.position = new Vector3(transform.position.x + _speed * Time.deltaTime, transform.position.y, transform.position.z);
            else _forward = false;
        }
        else
        {
            if (transform.position.x >= _min)
                transform.position = new Vector3(transform.position.x - _speed * Time.deltaTime, transform.position.y, transform.position.z);
            else _forward = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);

        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Health>().Damage(1);
        }
    }
}
