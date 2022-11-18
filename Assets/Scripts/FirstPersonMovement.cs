using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f;

    public Transform ground;
    public float groundDistance = 0.04f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGround;


    // Update is called once per frame
    void Update()
    {

        isGround = Physics.CheckSphere(ground.position, groundDistance, groundMask);

        if(isGround && velocity.y < 0){
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        
    }
}
