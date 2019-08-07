using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills", fileName="NewSkill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public int id;
    public string description;
    public string animatorName;

    public int damage;
    public float damageDealtDelay;
    public float animationFinishedDelay;

    public float range;
    public bool isProjectile;
    public GameObject projectlePrefab;
    public float castTime;
    public float coolDown;
    public float coolDownTimer;
}
