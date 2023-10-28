using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rb;
    public LayerMask layerMask;
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& this.grounded)
        {
            this.rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            this.anim.SetBool("jump", true);
        }
    }

    private void Grounded()
    {
        if(Physics.CheckSphere(this.transform.position + Vector3.down,0.2f, layerMask))
        {
            this.grounded = true;
        }
        else
        {
            this.grounded = false;
        }
        
    }
    private void Move()
    {
        float veticlaAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 movment = this.transform.forward * veticlaAxis + this.transform.right * horizontalAxis;
        movment.Normalize();

        this.transform.position += movment * 0.04f;
        this.anim.SetFloat("vertical", veticlaAxis);
        this.anim.SetFloat("horizontal", horizontalAxis);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Grounded();
        Jump();
        Move();
    }
}
