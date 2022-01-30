using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject obj,otherPlayer;
    [SerializeField] float min, max, height=1;
    [SerializeField] bool spawnOnStart;
    [SerializeField] FixedButton spawnButton;
    [SerializeField] GameObject fill;
    [SerializeField] CooldownTimer cd=null;
    void Start()
    {
        if (spawnOnStart)
        {
            Vector3 spawnPos = new Vector3(Random.Range(min, max), height, Random.Range(min, max));
            Instantiate(obj, spawnPos, obj.transform.rotation);
        }
        else
        {
            cd = GetComponent<CooldownTimer>();
        }
    }

    void Update()

    {
        if (!spawnOnStart)
        {
            Refill();
            Spawn();
        }
      
    }
    #region Refill
    void Refill()
    {
        if (fill.GetComponent<Image>().fillAmount > 0)
        {
           fill.GetComponent<Image>().fillAmount -=
           (1.0f / cd.cooldownTimer["Spawn"]) * Time.deltaTime;
           fill.GetComponentInChildren<Text>().text =
           (cd.nextAttackTime["Spawn"] - (int)Time.time).ToString();
        }
        else { fill.GetComponentInChildren<Text>().text = ""; }
    }
    #endregion

    #region Attack_Defend
    void Spawn()
    {
            if (cd.nextAttackTime["Spawn"]
            < Time.time && (Input.GetKeyDown("1") || spawnButton.Pressed))
            {
                Vector3 spawnPos = otherPlayer.transform.position + new Vector3(Random.Range(min, max),0,Random.Range(min, max));
                Instantiate(obj, spawnPos, obj.transform.rotation);
                cd.nextAttackTime["Spawn"] = cd.cooldownTimer["Spawn"] + (int)Time.time;
                fill.GetComponent<Image>().fillAmount = 1;
            }
    }
    #endregion
}
