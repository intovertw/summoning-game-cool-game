using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        //ery code block for boolet
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);

        Destroy(gameObject, 3f);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //handles collision, ignores player cause that would make the boolet disappear instantly
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Pet"))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}