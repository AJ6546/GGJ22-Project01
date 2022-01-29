using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProjectileInstantiator : MonoBehaviour
{
    [SerializeField] string s;
    Animator anim;
    [SerializeField] CooldownTimer cd;
    [SerializeField] GameObject fill;
    [SerializeField] Projectiles ammo;
    public Transform instantiatorTransform;
    [SerializeField] float fwSpawnPos;
    [SerializeField] float upSpawnPos;
    [SerializeField] float lftSpawnPos;
    [SerializeField] FixedButton attack_defendButton;

    void Start()
    {
        cd = GetComponent<CooldownTimer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Refill();
        Attack();
    }
    #region Refill
    void Refill()
    {
        if (fill.GetComponent<Image>().fillAmount > 0)
        {
            if (s == "w")
            {
                fill.GetComponent<Image>().fillAmount -=
                  (1.0f / cd.cooldownTimer["Attack"]) * Time.deltaTime;
                fill.GetComponentInChildren<Text>().text =
                    (cd.nextAttackTime["Attack"] - (int)Time.time).ToString();
            }
            if (s == "b")
            {
                fill.GetComponent<Image>().fillAmount -=
               (1.0f / cd.cooldownTimer["Defend"]) * Time.deltaTime;
                fill.GetComponentInChildren<Text>().text =
                    (cd.nextAttackTime["Defend"] - (int)Time.time).ToString();
            }
        }
        else { fill.GetComponentInChildren<Text>().text = ""; }
    }
    #endregion

    #region Attack_Defend
    void Attack()
    {
        if (s == "w")
        {
            if (cd.nextAttackTime["Attack"]
            < Time.time && (Input.GetKeyDown("1") || attack_defendButton.Pressed))
            {
                anim.SetTrigger("Attack_Defend");
                cd.nextAttackTime["Attack"] = cd.cooldownTimer["Attack"] + (int)Time.time;
                fill.GetComponent<Image>().fillAmount = 1;
            }
        }
        if (s == "b")
        {
            if (cd.nextAttackTime["Defend"]
                < Time.time && (Input.GetKeyDown("2") || attack_defendButton.Pressed))
            {
                anim.SetTrigger("Attack_Defend");
                cd.nextAttackTime["Defend"] = cd.cooldownTimer["Defend"] + (int)Time.time;
                fill.GetComponent<Image>().fillAmount = 1;
            }
        }
    }
    void Hit()
    {
        if (s == "w")
        {
            Vector3 spawnPos = instantiatorTransform.position + instantiatorTransform.forward * fwSpawnPos +
            instantiatorTransform.up * upSpawnPos + instantiatorTransform.right * lftSpawnPos;
            Projectiles projectileInstance = Instantiate(ammo,
            spawnPos, ammo.transform.rotation);
            projectileInstance.SetInstantiator(transform);
        }
        if (s == "b")
        {
            Vector3 spawnPos = instantiatorTransform.position + instantiatorTransform.forward * fwSpawnPos +
            instantiatorTransform.up * upSpawnPos + instantiatorTransform.right * lftSpawnPos;
            Projectiles projectileInstance = Instantiate(ammo,
            spawnPos, instantiatorTransform.rotation);
            projectileInstance.SetInstantiator(transform);
        }
    }
    #endregion
}
