using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed = 7;
    public WeaponScript Weapon;
    public Slider RechargeSlider;

    [HideInInspector] public UnitScript unitScript; 

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D weaponTrigger;
    private GameObject obstacles;

    // Start is called before the first frame update
    public void SetUp()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = GeneralData.HeroSprite;
        Weapon.SetUp(obstacles, RechargeSlider);
        weaponTrigger = Weapon.GetComponent<BoxCollider2D>();
        unitScript = GetComponent<UnitScript>();
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
        if (h < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (h > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        if (weaponTrigger.enabled)
            return;

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        Weapon.transform.localPosition = direction * Weapon.HoldDistance;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Weapon.transform.localRotation = Quaternion.Euler(0f, 0f, rotZ);
        if (Input.GetMouseButtonDown(0))
        {
            if (Weapon.attackReady)
            {
                StartCoroutine(Weapon.Attack(direction));
            }
        }
    }

    public void TakeItem()
    {

    }
}