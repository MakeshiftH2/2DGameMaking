using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitHitter : MonoBehaviour
{
    public Animator suitAnimator;


    // Start is called before the first frame update
    void Start()
    {
        suitAnimator = GetComponent<Animator>();
    }


    public void SuitActivator()
    {
        suitAnimator.SetBool("onTriggerHit", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(suitAnimator == null)
        {
            return;
        }
    }
}
