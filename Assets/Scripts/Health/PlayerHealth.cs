using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScriptParent
{
    public static PlayerHealth Instance { get; private set; }

    [SerializeField] float armorRegenTime = 1f;
    [SerializeField] int maxArmorHealth = 10;
    int armorHealth;

    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= armorRegenTime && armorHealth < maxArmorHealth)
            RegenArmor();
    }
    private new void Awake()
    {
        base.Awake();
        Instance = this;
        armorHealth = maxArmorHealth;
    }
    private void Start()
    {
        PlayerGuiManager.Instance.ChangeHealth(health, maxHealth);
        PlayerGuiManager.Instance.ChangeArmor(armorHealth, maxArmorHealth);
    }
    public override void TakeDamage(int damage, object sender = null)
    {
        timer = 0;
        if (armorHealth > 0)
        {
            armorHealth = Mathf.Clamp(armorHealth - damage, 0, maxArmorHealth);
            PlayerGuiManager.Instance.ChangeArmor(armorHealth, maxArmorHealth);
        }
        else
        {
            health = Mathf.Clamp(health - damage, 0, maxHealth);
            PlayerGuiManager.Instance.ChangeHealth(health, maxHealth);
        }
        
        hurtEvent?.Invoke();
        if (health <= 0)
            Die();
    }

    protected override void Die()
    {
        dieEvent?.Invoke();
        PlayerGuiManager.Instance.ShowResults(false);
    }

    private void RegenArmor()
    {
        armorHealth = maxArmorHealth;
        PlayerGuiManager.Instance.ChangeArmor(armorHealth, maxArmorHealth);
    }
}
