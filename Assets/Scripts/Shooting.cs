using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public float gunDmg = 1f;
    public float rate = .10f;
    public float range = 50f;
    public float hitForce = 100f;

    public float magSize = 12f;

    Transform gunEnd;

    private GameObject player;
    private WaitForSeconds shotDuration = new WaitForSeconds(.7f);
    private TextMeshPro ammo;

    private LineRenderer laserLine;

    private float nextFire;
    
    // Start is called before the first frame update
    void Start()
    {
        gunEnd = this.transform.GetChild(0);
        ammo = gunEnd.GetChild(0).GetComponent<TextMeshPro>();
        laserLine = gunEnd.GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire && magSize>0)
        {
            StartCoroutine(ShotEffect());
            nextFire = Time.time + rate;

            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            Vector3 rayOrigin = player.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

            if(Physics.Raycast(rayOrigin, transform.forward, out hit, range))
            {
                laserLine.SetPosition(1, hit.point);
                EnemyManager enemy = hit.collider.GetComponent<EnemyManager>();

                if(enemy != null)
                {
                    enemy.Damage(gunDmg);
                }
            }
            else
            {
                laserLine.SetPosition(1,rayOrigin + (player.GetComponent<Camera>().transform.forward * range));
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
            magSize -= 1;
            ammo.SetText("" + magSize + "/12");
        }

        if(magSize == 0)
        {

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            magSize = 12;
            ammo.SetText("" + magSize + "/12");
        }
    }

    private IEnumerator ShotEffect()
    { 
        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }



}
