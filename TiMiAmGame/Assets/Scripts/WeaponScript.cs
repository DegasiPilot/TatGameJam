using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : Music
{
    [HideInInspector] public bool attackReady;

    public float HoldDistance;
    public float AttackDistance;
    public float Damage;
    public float RechargeTime;
    public float AnimSpeed;
    public Slider RechargeSlider;

    private BoxCollider2D trigger;
    private bool blocked;
    private GameObject obstacles;

    public void SetUp(GameObject obstacles, Slider rechergeSlider)
    {
        attackReady = true;
        trigger = GetComponent<BoxCollider2D>();
        trigger.enabled = false;
        blocked = false;
        this.obstacles = obstacles;
        RechargeSlider = rechergeSlider;
        rechergeSlider.maxValue = RechargeTime;
    }

    private void Update()
    {
        if (!trigger.enabled && Time.timeScale > 0)
            RotateByMouse();
    }

    private void RotateByMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
        direction.Normalize();
        transform.localPosition = direction * HoldDistance;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotZ);
        if (Input.GetMouseButtonDown(0))
        {
            if (attackReady)
            {
                StartCoroutine(Attack(direction));
            }
        }
    }

    public IEnumerator Attack(Vector2 direction)
    {
        trigger.enabled = true;
        Vector3 EndPos = direction * AttackDistance;
        attackReady = false;
        StartCoroutine(Recharge());
        while (transform.localPosition != EndPos && !blocked)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, EndPos, AnimSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        blocked = false;
        trigger.enabled = false;
        PlaySound(objsound[0]);
    }

    private IEnumerator Recharge()
    {
        float time = 0;
        while (time <= RechargeTime)
        {
            RechargeSlider.value = time;
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        attackReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent == obstacles)
        {
            blocked = true;
            return;
        }

        UnitScript unit;
        if(collision.TryGetComponent(out unit))
        {
            unit.GetDamage(Damage);
            blocked = true;
        }
    }
}