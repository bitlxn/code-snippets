using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //tapegun variables
    [SerializeField] float offset;
    [SerializeField] private SpriteRenderer tapeSpriteRender;
    //projectile timing
    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Start()
    {
        tapeSpriteRender = GetComponentInChildren<SpriteRenderer>();
    }


    // Update is called once per frame
    private void Update()
    {
        //weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        //weapon flip
        if (difference.x > 0)
        {
            tapeSpriteRender.flipY = false;
        }
        else if (difference.x < 0)
        {
            tapeSpriteRender.flipY = true;
        }


        //projectile spawn thing
        if (timeBtwShots <= 0)
        {
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
