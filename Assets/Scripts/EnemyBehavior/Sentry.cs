using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentry : MonoBehaviour
{
    [SerializeField] AudioClip shotAudio;
    [SerializeField] OwnerType sentryType = OwnerType.Player;

    [SerializeField] Transform bulletSpawn;
    [SerializeField] Transform sentryHead;
    [SerializeField] ParticleSystem particles;
    ParticleSystem.EmissionModule emissionModule;

    [SerializeField] int maxAmmo = 100;
    [SerializeField] float distance = 10;
    [SerializeField] int damage = 1;
    [SerializeField] float cooldown = 1;
    [SerializeField] float spread = 0;

    int ammo = 0;
    float timer = 0;
    bool exploded = false;

    Quaternion headRotation = Quaternion.identity;

    private void Start()
    {
        emissionModule = particles.emission;
        ammo = maxAmmo;
        Color c = Color.white;
        if (sentryType == OwnerType.Player)
            c = Color.blue;
        else
            c = Color.red;
        
        foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>())
        {
            if (s.gameObject.name != "Fill")
                s.color = c;
        }
    }

    private void Update()
    {
        if (exploded)
        {
            return;
        }
        GameObject go = FindTarget();
        if (go != null)
        {
            Debug.DrawLine(bulletSpawn.position, go.transform.position, new(1, 1, 1, 0.5f));

            Vector3 targetPos = go.transform.position;
            if (sentryType == OwnerType.Player)
                targetPos = go.transform.position + go.GetComponent<NavMeshAgent>().velocity * ((go.transform.position - sentryHead.position).magnitude / 34);
            else if (sentryType == OwnerType.Enemy)
                targetPos = go.transform.position + go.GetComponent<Rigidbody>().velocity * ((go.transform.position - sentryHead.position).magnitude / 34);

            Debug.DrawLine(bulletSpawn.position, targetPos,new(1,1,1,1));

            sentryHead.rotation = Quaternion.LookRotation(targetPos - sentryHead.position) * Quaternion.Euler(0, -90, 0);
            headRotation = Quaternion.Euler(0, sentryHead.rotation.eulerAngles.y, 0);

            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;
                if (ammo <= 0)
                {
                    SentryHealth s = GetComponent<SentryHealth>();
                    if (s != null)
                        s.Explode();
                    Shutdown();
                }
                else
                {
                    emissionModule.rateOverTime = (maxAmmo - ammo) / 10;
                    Shoot();
                }
            }
        }
        else
        {
            headRotation = headRotation * Quaternion.Euler(0,0.5f,0);
            sentryHead.rotation = headRotation;
        }
    }

    private void Shoot()
    {
        ammo--;
        AudioManager.Play(gameObject, shotAudio, 20f, 1f, AudioManager.RandomPitch(0.1f));
        GameObject bullet = Instantiate(GlobalVals.Instance.Bullet, bulletSpawn.position, bulletSpawn.rotation * Quaternion.Euler(0, 90, 0), GlobalVals.Instance.BulletParent.transform);
        bullet.GetComponent<BulletScript>().Constructor(damage, sentryType);

        Rigidbody brb = bullet.GetComponent<Rigidbody>();
        Vector3 rndVector = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 1);
        brb.AddRelativeForce(rndVector * 30, ForceMode.VelocityChange);
    }

    private GameObject FindTarget()
    {
        if (sentryType == OwnerType.Player)
        {
            EnemyBehavior closest = null;
            float currentDistance = distance;
            foreach (EnemyBehavior e in EnemyBehavior.ActiveEnemies)
            {
                if (e == null)
                    continue;
                if (Physics.Raycast(bulletSpawn.position, e.transform.position - sentryHead.position,out RaycastHit hit,currentDistance,GlobalVals.Instance.RaycastLayermask) && hit.collider.gameObject == e.gameObject)
                {
                    if ((e.transform.position - sentryHead.position).magnitude <  currentDistance)
                        closest = e;
                }
            }
            return closest?.gameObject;
        }
        else if (sentryType == OwnerType.Enemy)
        {
            GameObject e = PlayerHealth.Instance.gameObject;
            if (Physics.Raycast(bulletSpawn.position, e.transform.position - sentryHead.position, out RaycastHit hit, distance, GlobalVals.Instance.RaycastLayermask) && hit.collider.gameObject == e.gameObject)
            {
                return e;
            }
            else
                return null;
        }
        return null;
    }

    public void Shutdown()
    {
        exploded = true;
        emissionModule.enabled = false;
        this.enabled = false;
    }
}
