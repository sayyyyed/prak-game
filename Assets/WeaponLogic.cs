using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    // [SerializeField] Camera ShootCamera;
    // [SerializeField] float range = 100f;
    public ParticleSystem MuzzleFlash;
    public Camera ShootCamera;
    public float range = 1000f;
    public GameObject HitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Mouse0))
       {
        
        MuzzleFlash.Play();
        Shoot();
       } 
    //    else
    //    {
    //     MuzzleFlash.Stop();
    //    }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(ShootCamera.transform.position, ShootCamera.transform.forward, out hit, range))
        {
            Debug.Log("I hit this mf: " + hit.transform.name);
            CreateHitImpact(hit);
            if (hit.transform.tag.Equals("Enemy"))
            {
                EnemyLogic target = hit.transform.GetComponent<EnemyLogic>();
                target.TakeDamage(50);
            }
        }
        else
        {
            return;
        }



    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = ShootCamera.transform.TransformDirection(Vector3.forward) * range;
        Gizmos.DrawRay(ShootCamera.transform.position, direction);
    }
}
