using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTestMove : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 5f;

    [SerializeField] float activeMoveSpeed;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;

    Vector2 Movement;
    Vector2 MousePos;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activeMoveSpeed = MoveSpeed;
        //Cursor.visible = false;       
    }

    private void Update()
    {

        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + Movement * activeMoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = MousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
