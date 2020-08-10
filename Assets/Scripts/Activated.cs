using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activated : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<SuitHitter>().SuitActivator();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<SuitHitter>().suitAnimator.SetBool("OnTriggerHit", false);
    }
}
