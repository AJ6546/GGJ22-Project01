using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Health health1, health2;
    [SerializeField] Image foreground1, foreground2;
    [SerializeField] Text t1, t2;
    [SerializeField] float healthFactor1, healthFactor2;
    void Start()
    {
        
    }
    void Update()
    {
        healthFactor1 = health1.GetHealthFactor();
        healthFactor2 = health2.GetHealthFactor();
        foreground1.fillAmount = Mathf.Max(healthFactor1/2, 0);
        foreground2.fillAmount = Mathf.Max(healthFactor2/2, 0);
        t1.text = (health1.GetHealthFactor()*100).ToString();
        t2.text = (health2.GetHealthFactor()*100).ToString();
    }
}
