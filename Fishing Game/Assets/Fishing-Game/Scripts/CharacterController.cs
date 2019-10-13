using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;
    public float speed = 3.0f;


    public bool canFish = false;
    public bool isFishing = false;

    Rigidbody2D rigidbody2d;
    Collider2D collider;

    public string fishButton = "Jump";

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        //animator = GetComponent<Animator>();

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        //animator.SetFloat("Look X", lookDirection.x);
        //animator.SetFloat("Look Y", lookDirection.y);
        //animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;

        position = position + move * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (Input.GetButtonDown(fishButton) && canFish)
        {
            Debug.Log("Go Fish!");
            GoFish();
        }
    }

    void GoFish()
    {
        isFishing = true;
    }
}
