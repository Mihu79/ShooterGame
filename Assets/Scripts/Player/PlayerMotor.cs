using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool IsGrounded;
    private bool lerpCrouch;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    private float crouchTimer = 0;
    private bool crouching;
    private bool sprinting;

    public Transform gunBarrel;
    public GameObject bulletPrefab;
    private GunRecoil gunRecoil;
    public GameObject Gun;

    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (Gun != null)
        {
            gunRecoil = Gun.GetComponent<GunRecoil>();
        }
        
    }

    void Update()
    {
        IsGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
        if (crouching)
            speed = 3;
        else
            speed = 5;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (IsGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Shoot()
    {
        if (Time.time > nextFireTime)
        {
            
            GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
            Vector3 shootDirection = gunBarrel.forward.normalized;
            bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 40f;

            if (gunRecoil != null)
            {
                gunRecoil.TriggerRecoil();
            }
            nextFireTime = Time.time + fireRate;
        }
    }
}
