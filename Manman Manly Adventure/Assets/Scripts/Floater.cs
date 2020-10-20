using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    /*
      * The Mathf.Sin() function transforms any number into a number between -1 and 1. 
      * Using it to position an object can produce beautiful results because of 
      * the graceful way it begins and ends. Using Time.time as an argument is easy 
      * and, as long as the game is playing, the number will continue to grow.
      * 
      * The problem is that Lerp requires a number between 0 and 1. We can solve that partly
      * dividing Mathf.Sin(Time.time) by 2. That gives a number between -.5 and .5. If we add
      * .5 to that, we get a constant number between 0 and 1, just what we need. This is what the code
      * looks like:
      * 
      * 			lerp = (Mathf.Sin(Time.time) / 2f) +.5f;
      * 
      * Randomizing the lerp position.
      * If you duplicate an object with this script on it, you'll notice they all move 
      * in step. If that is not what you want, you can add an offset to Time.time. The offset
      * can be any number between 0 and 360. If you make it a random number, 
      * each object will have a unique motion. If that's what you want to happen 
      * you might declare a float called offset. Then, in the Start() function you'd put:
      * 
      * 			offset = Random.Range(0f, 360f);
      * 
      * You'd also need to change the argument to Mathf.Sin() to Time.time + offset):
      * 
      * 			lerp = (Mathf.Sin(Time.time + offset) / 2f) +.5f;
      * 
      *Controlling sepeed.
      *The speed at which the object moves is a function of time.time. You can controllthat by
      * multiplying Time.time by a speed factor. That would look like this:
      * 
      * 			float speed = 10f;
      * 
      * Then the calculation of lerp would change to this:
      * 
      * 			lerp = (Mathf.Sin(Time.time * speed) / 2f) +.5f;
      * 
      * */

    Vector3 startPos, endPos;
    float lerp = .3f;
    public Vector3 direction = new Vector3(0f, 1f, 0f);
    Rigidbody2D rb;

    public float _speed = 1f;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + direction;
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        endPos = startPos + direction;

        lerp = (Mathf.Sin(Time.time * _speed) / 2f) + .5f;
        rb.MovePosition(Vector3.Lerp(startPos, endPos, lerp));
    }
}
