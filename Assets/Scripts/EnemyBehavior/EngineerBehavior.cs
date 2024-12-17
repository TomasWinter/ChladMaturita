using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerBehavior : BasicEnemyBehavior
{
    [SerializeField] AudioClip whoosh;
    [SerializeField] GameObject turretPrefab;
    [SerializeField] int maxTurrets = 3;
    int turrets = 0;

    [SerializeField] float cooldown = 30;
    [SerializeField] float randomDelay = 5;
    float timer = 0;

    bool deploying = false;

    private void Awake()
    {
        turrets = maxTurrets;
    }
    protected override void Update()
    {
        if (turrets > 0 && timer > cooldown && !deploying)
        {
            if ((target.transform.position - transform.position).magnitude < 20)
            timer = 0;
            deploying = true;
            StartCoroutine(DeployTurret());
        }
        else if (!deploying)
        {
            timer += Time.deltaTime;
            base.Update();
        }
    }

    IEnumerator DeployTurret()
    {
        turrets--;
        walkAnimator.SetBool("Walking", false);
        agent.ResetPath();

        yield return new WaitForSeconds(1f);

        GameObject x = Instantiate(turretPrefab,transform.position+transform.forward,Quaternion.identity);
        x.GetComponent<Rigidbody>().AddForce((transform.forward + x.transform.up/2)*5,ForceMode.Impulse);
        AudioManager.Play(x, whoosh, 15);

        yield return new WaitForSeconds(1f);

        timer = 0 + Random.Range(-randomDelay, randomDelay);
        deploying = false;
    }
}
