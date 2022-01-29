using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTimer : MonoBehaviour
{
    public Dictionary<string, float> cooldownTimer = new Dictionary<string, float>
    { { "Attack", 5 },
        { "Defend", 7},
        { "Spawn", 7}};
    public Dictionary<string, float> nextAttackTime = new Dictionary<string, float>
    { { "Attack", 0 },
        { "Defend", 0},
        { "Spawn", 0}};
}
