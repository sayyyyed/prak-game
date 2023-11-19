using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundcheck : MonoBehaviour
{
    PlayerLogic movlog;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touch the ground");
        movlog.groundedchanger();
    }

    private void Start()
    {
        movlog = this.GetComponentInParent<PlayerLogic>();
    }


}
