using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private RaycastHit hit;

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
        rb.velocity = new Vector2(h * speed, v*speed);
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 30f))
            {
                if (hit.transform)
                {

                }
            }
        }
    }
}