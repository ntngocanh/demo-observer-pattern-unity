using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static event EventHandler StarCollected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Input.mousePosition;
        position.z = -Camera.main.transform.position.z;
        position = Camera.main.ScreenToWorldPoint(position);

        // move to position and clamp in screen
        float step = 10 * Time.deltaTime;
        transform.position = position;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
		// if colliding with star, destroy star and play sound
		if (coll.gameObject.CompareTag("Star"))
        {
			//Destroy(coll.gameObject);
            StarCollected?.Invoke(this, EventArgs.Empty);
		}
	}
}
