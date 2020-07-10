using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<Enemy>().currentTarget = FindObjectOfType<Player>().gameObject;
            FindObjectOfType<Enemy>().moveSpeed = 1f;
            Destroy(gameObject);
        }
    }

}
