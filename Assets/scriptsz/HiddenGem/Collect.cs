using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    //public GameObject buffCircle;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            //buffCircle.SetActive(false);
            this.gameObject.SetActive(false);
            //score++ or something
        }
    }
}
