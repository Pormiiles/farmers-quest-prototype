using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;

    private Rigidbody2D rig;
    private Vector2 playerDirection;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));        
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
    }
}
