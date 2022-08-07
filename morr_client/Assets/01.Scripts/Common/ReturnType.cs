using System.Collections.Generic;

public class GoogleSheet
{
    public string range { get; set; }
    public string majorDimension { get; set; }
    public List<List<string>> values { get; set; }
}
public class RTData
{
    public int m_Index;
}


public class RTSkill : RTData
{
    public int m_GroupIndex;
    public string m_ViewName;
    public string m_ViewDesc;
    public string m_ViewEffect;
    public RTSkillType m_SkillType;
    public RTLikeability m_Likeability;
    public RTUseCondition m_UseCondition;
    public RTDamageType m_DamageType;
    public int m_DamageValue;
    public int m_LimitCount;
    public RTRangePivot m_RangePivot;
    public List<string> m_Range;
    
    //1
    public RTBuffCondition m_BuffCondition_1;
    public RTBuffTimeType m_BuffTimeType_1;
    public int m_BuffTime_1;
    public RTBuffTarget m_BuffTarget_1;
    public RTBuffType m_BuffType_1;
    public string m_BuffValue_1;
    
    //2
    public RTBuffCondition m_BuffCondition_2;
    public RTBuffTimeType m_BuffTimeType_2;
    public int m_BuffTime_2;
    public RTBuffTarget m_BuffTarget_2;
    public RTBuffType m_BuffType_2;
    public string m_BuffValue_2;
}

public class RTLanguage : RTData
{
    public string m_Key;
    public string m_Korean;
    public string m_English;
}

public class RTUnit : RTData
{
    public string m_ViewName;
    public int m_SkillGroupIndex;
    public int m_RequireItemIndex;
    public int m_RequireCount;
    public string m_Resource;
}

public class RTItem : RTData
{
    public string m_ViewName;
    public string m_ViewDesc;
    public string m_IconPath;
}

public class RTGroundProperty : RTData
{
    public int m_AttackerUnitIndex;
    public int m_DefenderUnitIndex;
    public RTMergeResult m_MergeResult;
}

public class RTScenario : RTData
{
    public int m_OwnerUnitIndex;
    public RTLikeability m_Likeability;
    public int m_TargetUnitIndex;
    public List<(int _unitIndex, string _content)> m_Values = new List<(int _unitIndex, string _content)>();
}

public class RTQAConfig : RTData
{
    public string m_Name;
    public List<string> m_Values = new List<string>();
}
public enum RTMergeResult
{
    None=0,
    OnlyAttacker,
    Both,
    WaitDefender,
}
public enum RTSkillType
{
    None=0,
    Common,
    Normal,
    Ultimate,
    Partner,
}

public enum RTLikeability
{
    None=0,
    Low,
    Mid,
    High,
}

public enum RTUseCondition
{
    None=0,
    NoPreDefence,
}

public enum RTDamageType
{
    None=0,
    Fixed,
    PreAttacked,
}

public enum RTRangePivot
{
    None=0,
    Self,
    Enemy,
}

public enum RTBuffCondition
{
    None=0,
    UseSkill,
    AttackSuccess,
    AttackFail,
}

public enum RTBuffTimeType
{
    None=0,
    OneShot,
    Active,
    Passive
}

public enum RTBuffTarget
{
    None=0,
    Self,
    Enemy,
    Ground
}

public enum RTBuffType
{
    None=0,
    DefenceUp,
    DefenceNormal,
    Damage,
    TrapDamage,
    
    Heal,
    Invalid,
    Move,
    
    AddDamage,
    AddDamagePerAttack,
}