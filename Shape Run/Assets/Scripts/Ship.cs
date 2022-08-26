using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Sprite shipSprite;
    public bool isShip;
    public float flyvelocity;
    public PolygonCollider2D polygon;

    private Cube cube;
    private Rigidbody2D rb;

    void Start()
    {
        cube = GetComponent<Cube>();
        rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isShip){
            if(Input.GetKey(KeyCode.Space)){
                rb.velocity = Vector2.up * flyvelocity;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Collider2D>().tag == "Ship Portal"){
            gameObject.GetComponent<SpriteRenderer>().sprite = shipSprite;
            cube.isCube = false;
            isShip = true;
            rb.gravityScale = 6.25f;
            transform.rotation = Quaternion.Euler(0, 0, 0);


        }
    }
}
