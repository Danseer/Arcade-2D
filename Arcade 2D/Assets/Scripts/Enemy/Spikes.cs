using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);

        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Health>().Damage(1);
        }
    }
}
