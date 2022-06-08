using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StarInfo : EventArgs
    {
        public int value;

        public StarInfo(int value)
        {
            this.value = value;
        }
    }
public class Star : MonoBehaviour
{
    //Custom delegate with no parameters
    public delegate void MyEventHandlerEmpty();

    //The event belonging to the custom delegate
    public static event MyEventHandlerEmpty StarMissed;
    //C# built-in EventHandler
    //without parameter
    //public static event EventHandler StarMissed;
    //with custom parameter
    public static event EventHandler<StarInfo> StarCollectedWithValue;
    float colliderHalfHeight;
    private int value;
    private float scale;
    // Start is called before the first frame update
    void Awake(){

    }
    void Start()
    {
        scale = UnityEngine.Random.Range(0.1f, 0.3f);
        GetComponent<Rigidbody2D>().AddForce(1 * new Vector2(0, -1),
            ForceMode2D.Impulse
        );
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        gameObject.transform.localScale = new Vector3(scale, scale, 1);
        colliderHalfHeight = collider.radius * scale;
        
        value = UnityEngine.Random.Range(10, 20);
    }

    //Update is called once per frame
    void Update()
    {
        if (transform.position.y <= (ScreenUtils.ScreenBottom - colliderHalfHeight))
        {
            StarMissed?.Invoke();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StarCollectedWithValue?.Invoke(this, new StarInfo(value));
            Destroy(gameObject);
        }
    }
}
