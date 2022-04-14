using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色的状态信息
/// </summary>
public class CharacterStates : MonoBehaviour
{
    [SerializeField] CharacterData_SO characterData;
    [SerializeField] AttackData_SO attackData;
    [HideInInspector] public bool isCritical;//是否暴击
    #region  Read from Data_SO
    public int MaxHealth
    {
        get { return characterData != null ? characterData.maxHealth : 0; }
        set { characterData.maxHealth = value; }
    }
    public int CurrentHealth
    {
        get { return characterData != null ? characterData.currentHealth : 0; }
        set { characterData.currentHealth = value; }
    }
    public int BaseDefence
    {
        get { return characterData != null ? characterData.baseDefence : 0; }
        set { characterData.baseDefence = value; }
    }
    public int CurrentDefence
    {
        get { return characterData != null ? characterData.currentDefence : 0; }
        set { characterData.currentDefence = value; }
    }
    public float AttackRange
    {
        get { return attackData != null ? attackData.attackRange : 0; }
        set { attackData.attackRange = value; }
    }
    public float SkillRange
    {
        get { return attackData != null ? attackData.skillRange : 0; }
        set { attackData.skillRange = value; }
    }
    public float CoolDown
    {
        get { return attackData != null ? attackData.coolDown : 0; }
        set { attackData.coolDown = value; }
    }
    public int MinDamage
    {
        get { return attackData != null ? attackData.minDamage : 0; }
        set { attackData.minDamage = value; }
    }
    public int MaxDamage
    {
        get { return attackData != null ? attackData.maxDamage : 0; }
        set { attackData.maxDamage = value; }
    }
    public float CriticalMultiplier
    {
        get { return attackData != null ? attackData.criticalMultiplier : 0; }
        set { attackData.criticalMultiplier = value; }
    }
    public float CriticalChance
    {
        get { return attackData != null ? attackData.criticalChance : 0; }
        set { attackData.criticalChance = value; }
    }
    #endregion
    #region Character Combat
    public void TakeDamage(CharacterStates attacker, CharacterStates defender)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        //TODO:更新UI
        //TODO:更新经验
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            print("暴击");
        }
        return (int)coreDamage;
    }
    #endregion
}
