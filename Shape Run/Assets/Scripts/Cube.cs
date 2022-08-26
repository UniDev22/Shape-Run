using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    public float speed;
    public float jumpVelocity;
    public GameObject deathEffect;
    public AudioClip deathClip;
    public Transform camT;
    public bool isCube;
    public float jpVelocity;
    public Sprite cubeSprite;

    [SerializeField] private LayerMask platformLM;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Ship ship;
    RaycastHit2D raycastHit2D;

    [HideInInspector]
    public bool isDead = false;
    bool neverDone = true;
    
    void Start()
    {
        isCube = true;
        ship = GetComponent<Ship>();
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if(!isDead){
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime, Space.World);
        if(isCube){
            IsGrounded();
            if(Input.GetKey(KeyCode.Space) && IsGrounded()){
                rb.velocity = Vector2.up * jumpVelocity;
            }
            if(raycastHit2D.collider == null){
                transform.Rotate(0, 0, -7.826f);
                }
            if(raycastHit2D.collider != null){
                var vec = transform.eulerAngles;
                vec.z = Mathf.Round(vec.z / 90) * 90;
                transform.eulerAngles = vec;

            }
        }        
        }

    }

    private void Update()
    {

        if(!isDead){
        if(Input.GetKeyDown(KeyCode.R)){
            isDead = true;
            StartCoroutine(Dead());
        }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Collider2D>().tag == "Hit"){
            isDead = true;
            StartCoroutine(Dead());
        }
        if(other.GetComponent<Collider2D>().tag == "JP"){
            rb.velocity = Vector2.up * jpVelocity;

        }
        
        if(other.GetComponent<Collider2D>().tag == "Ring"){
            if(Input.GetKey(KeyCode.Space)){
            rb. velocity = Vector2.up * jumpVelocity;

            }
        }
        if(other.GetComponent<Collider2D>().tag == "Cube Portal"){
            gameObject.GetComponent<SpriteRenderer>().sprite = cubeSprite;
            isCube = true;
            ship.isShip = false;

        }
    }

    IEnumerator Dead(){
        if(isDead){
            if(neverDone){
                GameObject temp = Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
                Destroy(temp, .5f);
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
                AudioSource.PlayClipAtPoint(deathClip, camT.position);
                neverDone = false;
            }
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else{
            yield return null;
        }
    }
    

    private bool IsGrounded(){
        raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, platformLM);
        return raycastHit2D.collider != null;            
    }




}
