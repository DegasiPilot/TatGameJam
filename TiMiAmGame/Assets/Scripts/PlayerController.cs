using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 7;
    public GameObject Weapon;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public void SetUp()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(h * Speed, v * Speed);
        flip(h);
    }

    private void flip(float h)
    {
        if (h > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (h < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        Weapon.transform.localPosition = direction;
    }
}