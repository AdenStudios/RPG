using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> warriorSkills = new List<Skill>();
    public List<Skill> mageSkills = new List<Skill>();
    public List<Skill> archerSkills = new List<Skill>();

    public List<Skill> skillsOnCoolDown = new List<Skill>();

    public static SkillManager instance;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadAbilities();
    }

    private void Update() 
    {
        CoolDownHandler();
    }

    public void LoadAbilities()
    {
        Skill[] warrior;
        warrior = Resources.LoadAll<Skill>("Skills/Warrior");
        foreach (var skill in warrior)
        {
            warriorSkills.Add(skill);
        }
    }

    public Skill GetSkill(ClassType classType, int id)
    {
        switch(classType)
        {
            case ClassType.Warrior:
            foreach (var skill in warriorSkills)
            {
                if (skill.id == id)
                {
                    return skill;
                }
            }
            break;

            case ClassType.Mage:
            break;
            
            case ClassType.Archer:
            break;
        }
        return null;
    }

    private void CoolDownHandler()
    {
        for (int i = 0; i < skillsOnCoolDown.Count; i++)
        {
            var skill = skillsOnCoolDown[i];
            skill.coolDownTimer += Time.deltaTime;
            if (skill.coolDownTimer >= skill.coolDown)
            {
                skill.coolDownTimer = 0;
                skillsOnCoolDown.Remove(skill);                
            }
        }
    }

    public void ActivateSkillCooldown(Skill skill)
    {
        skillsOnCoolDown.Add(skill);
    } 
}
