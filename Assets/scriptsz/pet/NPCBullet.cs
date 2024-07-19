using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBullet : MonoBehaviour
{
    public Rigidbody2D rb;

    public float force = 10f;

    Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void FixedUpdate()
    {
        if (!target)
        {
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * force;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //handles collision, ignores player cause that would make the boolet disappear instantly
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Pet") || collision.gameObject.tag.Equals("hiddenGem"))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
