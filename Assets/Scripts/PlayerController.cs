using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public Rigidbody _rb;
    public float _speed;
    public float _turnSpeed;
    //Vector2 moveVal2;
    Vector3 moveVal3;
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    void Awake()
    {
        //actions = new PlayerMovement();
    }
    void Update()
    {
        if (!isDashing)
        {
            Look();
        }        
    }
    void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }        
    }
    void OnMove(InputValue value)
    {
        Vector2 moveVal2 = value.Get<Vector2>();
        moveVal3 = new Vector3(moveVal2.x, 0, moveVal2.y);
        print("moveVal3: " + moveVal3);
    }
    void OnDash()
    {
        if(!isDashing && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void Look()
    {
        if (moveVal3 == Vector3.zero) return;

        var rot = Quaternion.LookRotation(moveVal3.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }
    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * moveVal3.normalized.magnitude * _speed * Time.deltaTime);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        //_rb.gravityScale = 0f;
        _rb.useGravity = false;

        //_rb.velocity = transform.forward * dashingPower;

        _rb.AddForce(transform.forward * dashingPower, ForceMode.Impulse);

        /*Vector3 targetPosition = transform.position + transform.forward * dashingPower;
        //_rb.MovePosition(targetPosition);

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < dashingTime)
        {
            _rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashingTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //_rb.MovePosition(targetPosition);
        */
        yield return new WaitForSeconds(dashingTime);
        _rb.velocity = Vector3.zero;
        _rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
