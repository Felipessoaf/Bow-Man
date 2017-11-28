using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 Force;
    public Player player;
    public bool Right;
    public float Depth;

    private bool _touch;

    private void Start()
    {
        _touch = false;
    }

    private void Update()
    {
        if(!_touch)
        {
            if (Right)
            {
                Vector2 v = GetComponent<Rigidbody2D>().velocity;
                var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                Vector2 v = GetComponent<Rigidbody2D>().velocity;
                var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    public void Shoot()
    {
        if(!Right)
        {
            transform.Rotate(Vector3.up, 180);
        }
        GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);
        Camera.main.GetComponent<Follow>().SetTarget(transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("Player") && collision.gameObject != player.gameObject) || collision.gameObject.CompareTag("Ground"))
        {
            if(!_touch)
            {
                if(collision.gameObject.CompareTag("Player"))
                {
                    collision.gameObject.GetComponent<Damage>().TakeDamage();
                }
                _touch = true;
                player.EndTurn();
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GetComponent<Rigidbody2D>().freezeRotation = true;
                transform.Translate(Vector3.forward * Depth);
                GetComponentInChildren<BoxCollider2D>().enabled = false;
            }
        }
    }
}
