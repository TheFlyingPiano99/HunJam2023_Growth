using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rb;
    public LayerMask layerMask;
    public bool grounded;
    public float walkSpeed = 2.0f;
    public float jumpDuration = 1.9f;
    public float jumpPrepareDuration = 0.4f;
    private bool isPreparingToJump = true;
    private float jumpTimer = 0.0f;
    private bool isJumping = false;
    public float animationSpeedMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private float GetImpulseForHeight(float height, float mass, float g)
    {
        return Mathf.Sqrt(2.0f * g * height) * mass;
    }

    private void Jump()
    {
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (isPreparingToJump && jumpPrepareDuration / animationSpeedMultiplier <= jumpTimer)
            {
                var size = gameObject.GetComponent<GrowthScript>().GetInterpolatedSize();
                isPreparingToJump = false;
                var xMax = size * 1.5f;
                var animTime = 0.5f * (jumpDuration - jumpPrepareDuration) / animationSpeedMultiplier;
                var g = 8.0f * xMax / animTime / animTime;
                Physics.gravity = g * new Vector3(0, -1, 0);
                var m = gameObject.GetComponent<Rigidbody>().mass;
                var p = GetImpulseForHeight(xMax, m, g);
                this.rb.AddForce(Vector3.up * p, ForceMode.Impulse);

                this.anim.SetBool("jump", false);   // Control animation (Setting here to prevent multiple jump anims)
            }
            else if (jumpDuration / animationSpeedMultiplier <= jumpTimer)
            {
                jumpTimer = 0.0f;
                isJumping = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && this.grounded)
            {
                isJumping = true;
                isPreparingToJump = true;
                this.anim.SetBool("jump", true);   // Control animation
            }
        }
    }

    private void Grounded()
    {
        var size = gameObject.GetComponent<GrowthScript>().GetInterpolatedSize();
        this.grounded = Physics.CheckSphere(
            this.transform.position + Vector3.down * size,
            size * 0.25f,
            layerMask
        );
    }
    private void Move()
    {
        float veticlaAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 movementDirection = this.transform.forward * veticlaAxis + this.transform.right * horizontalAxis;

        var rigidBody = this.GetComponent<Rigidbody>();
        var size = gameObject.GetComponent<GrowthScript>().GetInterpolatedSize();
        var v = movementDirection * walkSpeed * animationSpeedMultiplier * size;
        if (this.grounded && (!isJumping || isJumping && isPreparingToJump))
        {
            anim.speed = animationSpeedMultiplier * walkSpeed / 2.0f;
            rigidBody.velocity = new Vector3(v.x, rigidBody.velocity.y, v.z);
            this.anim.SetFloat("vertical", veticlaAxis);
            this.anim.SetFloat("horizontal", horizontalAxis);
        }
        else
        {
            rigidBody.velocity = new Vector3(0.5f * v.x, rigidBody.velocity.y, 0.5f * v.z);
            this.anim.SetFloat("vertical", 0.5f * veticlaAxis);
            this.anim.SetFloat("horizontal", 0.5f * horizontalAxis);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var size = gameObject.GetComponent<GrowthScript>().GetInterpolatedSize();
        anim.speed = animationSpeedMultiplier;
        gameObject.GetComponent<Rigidbody>().mass = Mathf.Pow(size, 3);
        Grounded();
        Jump();
        Move();
    }
}
