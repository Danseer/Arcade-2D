using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollect : MonoBehaviour
{
    [SerializeField] private float _health;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);

        if (collision.transform.tag == "Player")
        {
           
            collision.transform.GetComponent<Health>().AddHealth(_health);
            gameObject.SetActive(false);
        }
    }
}
