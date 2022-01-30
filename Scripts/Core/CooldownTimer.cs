using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTimer : MonoBehaviour
{
    public Dictionary<string, float> cooldownTimer = new Dictionary<string, float>
    {
        { "Attack", 3 },
        { "Defend", 5},
        { "Spawn", 7},
        { "EnemyAttack", 7 },
        { "Attack2",2 },
        { "Heal",15 }
    };
    public Dictionary<string, float> nextAttackTime = new Dictionary<string, float>
    {
        { "Attack", 0 },
        { "Defend", 0},
        { "Spawn", 0},
        { "EnemyAttack", 0 },
        { "Attack2",0 },
        { "Heal",0 }
    };
}
