using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{

    Rigidbody2D rbAnt;
    [SerializeField] float speed = 2f;
    [SerializeField] Transform point1, point2;
    [SerializeField] LayerMask layer;
    [SerializeField] bool isColliding;

    Animator animAnt;
    BoxCollider2D colliderAnt;


    private void Awake(){
        rbAnt = GetComponent<Rigidbody2D>();
        animAnt = GetComponent<Animator>();
        colliderAnt = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rbAnt.velocity = new Vector2(speed, rbAnt.velocity.y);

        isColliding = Physics2D.Linecast(point1.position , point2.position, layer);

        Debug.DrawLine(point1.position , point2.position, Color.blue);

        if(isColliding){
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(transform.position.y + 0.5f < collision.transform.position.y)
            {
                collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                animAnt.SetTrigger("Death");
                speed = 0;
                Destroy(gameObject, 0.3f);
                colliderAnt.enabled = false;
            }
            else
            {
                FindObjectOfType<PlayerMovement>().Death();
                Ant[] ant = FindObjectsOfType<Ant>();

                for (int i = 0; i < ant.Length; i++)
                {
                    ant[i].speed = 0;
                    ant[i].animAnt.speed = 0;
                }
            }
        }
    }
}
