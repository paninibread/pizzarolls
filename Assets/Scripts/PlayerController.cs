using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    bool isWeaponAnimationPlaying;
    private Animator animator;

    private List<GameObject> recordedHits = new List<GameObject>();
    private GameObject capturedHit;
    private GameObject hitEnemy;

    public Rigidbody _rb;

    public float _speed;
    public float _turnSpeed;

    Vector3 moveVal3;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private void OnEnable()
    {
        CombatSystemEventHandler.HitEvent += OnHitEvent;
        CombatSystemEventHandler.BasicAttackAnimationState += OnBasicAttackAnimation;
    }
    private void OnDisable()
    {
        CombatSystemEventHandler.HitEvent -= OnHitEvent;
        CombatSystemEventHandler.BasicAttackAnimationState -= OnBasicAttackAnimation;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (!isDashing)
        {
            Look();
        }
        if (isWeaponAnimationPlaying)
        {
            RecordWeaponSensorHits();
        }
    }
    void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }
    }
    private void RecordWeaponSensorHits()
    {
        hitEnemy = recordedHits.Find(x => x.gameObject.GetComponent<EnemyManager>());
        if (hitEnemy)
        {
            hitEnemy.GetComponent<EnemyManager>().Damage(1);
            isWeaponAnimationPlaying = false;
            recordedHits.Clear();
        }
    }
    private void OnHitEvent(GameObject sensorInfo)
    {
        capturedHit = sensorInfo.GetComponent<Sensors>().hit.collider.gameObject;
        if (capturedHit.GetComponent<EnemyManager>() != null)
        {
            recordedHits.Add(capturedHit);
        }
    }
    public void OnBasicAttack(InputValue value)
    {
        if(value.isPressed)
        {
            animator.SetTrigger("Attacking");
        }
    }
    private void OnBasicAttackAnimation(bool state)
    {
        if (state)
        {
            CombatSystemEventHandler.TriggerStartSensors(true);
            isWeaponAnimationPlaying = true;
        }
        else if (state == false)
        {
            CombatSystemEventHandler.TriggerStartSensors(false);
            isWeaponAnimationPlaying = false;
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
        if (!isDashing && canDash)
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
        _rb.MovePosition(transform.position + moveVal3.ToIso() * moveVal3.normalized.magnitude * _speed * Time.deltaTime);
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
