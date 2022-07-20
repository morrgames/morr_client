using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public partial class DataManager
{
    private Dictionary<int, RTSkill> dicSkill = new Dictionary<int, RTSkill>();
    private Dictionary<int, RTLanguage> dicLanguage = new Dictionary<int, RTLanguage>();
    private Dictionary<int, RTUnit> dicUnit = new Dictionary<int, RTUnit>();
    private Dictionary<int, RTItem> dicItem = new Dictionary<int, RTItem>();
    private Dictionary<int, RTGroundProperty> dicGroundProperty = new Dictionary<int, RTGroundProperty>();
    private Dictionary<int, RTScenario> dicScenario = new Dictionary<int, RTScenario>();


    private List<(int itemIndex, int itemCount)> cfg_startItem = new List<(int itemIndex, int itemCount)>();
    private List<(int unitIndex, int likeable)> cfg_Likeability = new List<(int unitIndex, int likeable)>();
    private int cfg_WhoFirstLikeability = 0;
    private List<int> cfg_LikeabilityInterval = new List<int>();
    private List<int> cfg_SinglePlay_MoreThan2Win_Free = new List<int>();//경험치,
    private List<int> cfg_SinglePlay_MoreThan2Win_FreeAD = new List<int>();//경험치,상대캐릭터호감도
    private List<int> cfg_SinglePlay_MoreThan2Win_Paid = new List<int>();//경험치,상대캐릭터호감도
    private List<int> cfg_SinglePlay_LessThan2Win_Free = new List<int>();//경험치
    private List<int> cfg_SinglePlay_LessThan2Win_FreeAD = new List<int>();//경험치,상대캐릭터호감도
    private List<int> cfg_SinglePlay_LessThan2Win_Paid = new List<int>();//경험치,상대캐릭터호감도
    private Dictionary<int, List<int>> cfg_Match_Story = new Dictionary<int, List<int>>();//unitIndex, targetIndexs...

    async UniTask InitData_File()
    {
        await GetExcel_Skill();
        await GetExcel_Language();
        await GetExcel_Unit();
        await GetExcel_Item();
        await GetExcel_GroundProperty();
        await GetExcel_Scenario();
        await GetExcel_QAConfig();
    }

    async UniTask GetExcel_Skill()
    {
        string jsonData = await GetExcel(Constants.EXCEL.SKILL);
        if (string.IsNullOrEmpty(jsonData)) return;

        dicSkill.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;

        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var _data = new RTSkill();
            for (int j = 0; j < rowData.Count; j++)
            {
                switch (values[0][j])
                {
                    case "Index":
                        _data.m_Index = AsInt(rowData[j]);
                        break;
                    case "GroupIndex":
                        _data.m_GroupIndex = AsInt(rowData[j]);
                        break;
                    case "ViewName":
                        _data.m_ViewName = AsString(rowData[j]);
                        break;
                    case "ViewDesc":
                        _data.m_ViewDesc = AsString(rowData[j]);
                        break;
                    case "ViewEffect":
                        _data.m_ViewEffect = AsString(rowData[j]);
                        break;
                    case "SkillType":
                        _data.m_SkillType = AsEnum<RTSkillType>(rowData[j]);
                        break;
                    case "Likeability":
                        _data.m_Likeability = AsEnum<RTLikeability>(rowData[j]);
                        break;
                    case "UseCondition":
                        _data.m_UseCondition = AsEnum<RTUseCondition>(rowData[j]);
                        break;
                    case "DamageType":
                        _data.m_DamageType = AsEnum<RTDamageType>(rowData[j]);
                        break;
                    case "DamageValue":
                        _data.m_DamageValue = AsInt(rowData[j]);
                        break;
                    case "LimitCount":
                        _data.m_LimitCount = AsInt(rowData[j]);
                        break;
                    case "RangePivot":
                        _data.m_RangePivot = AsEnum<RTRangePivot>(rowData[j]);
                        break;
                    case "Range":
                        _data.m_Range = AsListString(rowData[j]);
                        break;
                    case "BuffCondition_1":
                        _data.m_BuffCondition_1 = AsEnum<RTBuffCondition>(rowData[j]);
                        break;
                    case "BuffTimeType_1":
                        _data.m_BuffTimeType_1 = AsEnum<RTBuffTimeType>(rowData[j]);
                        break;
                    case "BuffTime_1":
                        _data.m_BuffTime_1 = AsInt(rowData[j]);
                        break;
                    case "BuffTarget_1":
                        _data.m_BuffTarget_1 = AsEnum<RTBuffTarget>(rowData[j]);
                        break;
                    case "BuffType_1":
                        _data.m_BuffType_1 = AsEnum<RTBuffType>(rowData[j]);
                        break;
                    case "BuffValue_1":
                        _data.m_BuffValue_1 = AsString(rowData[j]);
                        break;
                    case "BuffCondition_2":
                        _data.m_BuffCondition_2 = AsEnum<RTBuffCondition>(rowData[j]);
                        break;
                    case "BuffTimeType_2":
                        _data.m_BuffTimeType_2 = AsEnum<RTBuffTimeType>(rowData[j]);
                        break;
                    case "BuffTime_2":
                        _data.m_BuffTime_2 = AsInt(rowData[j]);
                        break;
                    case "BuffTarget_2":
                        _data.m_BuffTarget_2 = AsEnum<RTBuffTarget>(rowData[j]);
                        break;
                    case "BuffType_2":
                        _data.m_BuffType_2 = AsEnum<RTBuffType>(rowData[j]);
                        break;
                    case "BuffValue_2":
                        _data.m_BuffValue_2 = AsString(rowData[j]);
                        break;
                }
            }

            dicSkill.Add(_data.m_Index, _data);
        }

        Debug.Log("load success , skill");
    }

    async UniTask GetExcel_Language()
    {
        string jsonData = await GetExcel(Constants.EXCEL.LANGUAGE);
        if (string.IsNullOrEmpty(jsonData)) return;

        dicLanguage.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var _data = new RTLanguage();
            for (int j = 0; j < rowData.Count; j++)
            {
                switch (values[0][j])
                {
                    case "Index":
                        _data.m_Index = AsInt(rowData[j]);
                        break;
                    case "Key":
                        _data.m_Key = AsString(rowData[j]);
                        break;
                    case "Korean":
                        _data.m_Korean = AsString(rowData[j]);
                        break;
                    case "English":
                        _data.m_English = AsString(rowData[j]);
                        break;
                }
            }

            dicLanguage.Add(_data.m_Index, _data);
        }

        Debug.Log("success , language");
    }

    async UniTask GetExcel_Unit()
    {
        string jsonData = await GetExcel(Constants.EXCEL.UNIT);
        if (string.IsNullOrEmpty(jsonData)) return;

        dicLanguage.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var _data = new RTUnit();
            for (int j = 0; j < rowData.Count; j++)
            {
                switch (values[0][j])
                {
                    case "Index":
                        _data.m_Index = AsInt(rowData[j]);
                        break;
                    case "ViewName":
                        _data.m_ViewName = AsString(rowData[j]);
                        break;
                    case "SkillGroupIndex":
                        _data.m_SkillGroupIndex = AsInt(rowData[j]);
                        break;
                    case "RequireItemIndex":
                        _data.m_RequireItemIndex = AsInt(rowData[j]);
                        break;
                    case "RequireCount":
                        _data.m_RequireCount = AsInt(rowData[j]);
                        break;
                    case "Resource":
                        _data.m_Resource = AsString(rowData[j]);
                        break;
                }
            }

            dicUnit.Add(_data.m_Index, _data);
        }

        Debug.Log("success , Unit");
    }

    async UniTask GetExcel_Item()
    {
        string jsonData = await GetExcel(Constants.EXCEL.ITEM);
        if (string.IsNullOrEmpty(jsonData)) return;

        dicLanguage.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var _data = new RTItem();
            for (int j = 0; j < rowData.Count; j++)
            {
                switch (values[0][j])
                {
                    case "Index":
                        _data.m_Index = AsInt(rowData[j]);
                        break;
                    case "ViewName":
                        _data.m_ViewName = AsString(rowData[j]);
                        break;
                    case "ViewDesc":
                        _data.m_ViewDesc = AsString(rowData[j]);
                        break;
                    case "IconPath":
                        _data.m_IconPath = AsString(rowData[j]);
                        break;
                }
            }

            dicItem.Add(_data.m_Index, _data);
        }

        Debug.Log("success , Item");
    }

    async UniTask GetExcel_GroundProperty()
    {
        string jsonData = await GetExcel(Constants.EXCEL.GROUND_PROPERTY);
        if (string.IsNullOrEmpty(jsonData)) return;

        dicLanguage.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var _data = new RTGroundProperty();
            for (int j = 0; j < rowData.Count; j++)
            {
                switch (values[0][j])
                {
                    case "Index":
                        _data.m_Index = AsInt(rowData[j]);
                        break;
                    case "AttackerUnitIndex":
                        _data.m_AttackerUnitIndex = AsInt(rowData[j]);
                        break;
                    case "DefenderUnitIndex":
                        _data.m_DefenderUnitIndex = AsInt(rowData[j]);
                        break;
                    case "MergeResult":
                        _data.m_MergeResult = AsEnum<RTMergeResult>(rowData[j]);
                        break;
                }
            }

            dicGroundProperty.Add(_data.m_Index, _data);
        }

        Debug.Log("success , GroundProperty");
    }

    async UniTask GetExcel_Scenario()
    {
        string jsonData = await GetExcel(Constants.EXCEL.SCENARIO);
        if (string.IsNullOrEmpty(jsonData)) return;

        dicScenario.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var _data = new RTScenario();
            for (int j = 0; j < rowData.Count; j++)
            {
                switch (values[0][j])
                {
                    case "Index":
                        _data.m_Index = AsInt(rowData[j]);
                        break;
                    case "OwnerUnitIndex":
                        _data.m_OwnerUnitIndex = AsInt(rowData[j]);
                        break;
                    case "Likeability":
                        _data.m_Likeability = AsEnum<RTLikeability>(rowData[j]);
                        break;
                    case "TargetUnitIndex":
                        _data.m_TargetUnitIndex = AsInt(rowData[j]);
                        break;
                    case "speakingUnitIndex_1":
                    {
                        int unitIndex = AsInt(rowData[j]);
                        string content = AsString(rowData[j + 1]);
                        if (unitIndex == 0) break;
                        _data.m_Values.Add((unitIndex, content));
                        break;
                    }
                    case "speakingUnitIndex_2":
                    {
                        int unitIndex = AsInt(rowData[j]);
                        string content = AsString(rowData[j + 1]);
                        if (unitIndex == 0) break;
                        _data.m_Values.Add((unitIndex, content));
                        break;
                    }
                    case "speakingUnitIndex_3":
                    {
                        int unitIndex = AsInt(rowData[j]);
                        string content = AsString(rowData[j + 1]);
                        if (unitIndex == 0) break;
                        _data.m_Values.Add((unitIndex, content));
                        break;
                    }
                    case "speakingUnitIndex_4":
                    {
                        int unitIndex = AsInt(rowData[j]);
                        string content = AsString(rowData[j + 1]);
                        if (unitIndex == 0) break;
                        _data.m_Values.Add((unitIndex, content));
                        break;
                    }
                    case "speakingUnitIndex_5":
                    {
                        int unitIndex = AsInt(rowData[j]);
                        string content = AsString(rowData[j + 1]);
                        if (unitIndex == 0) break;
                        _data.m_Values.Add((unitIndex, content));
                        break;
                    }
                }
            }

            dicScenario.Add(_data.m_Index, _data);
        }

        Debug.Log("success , Scenario");
    }

    async UniTask GetExcel_QAConfig()
    {
        string jsonData = await GetExcel(Constants.EXCEL.QACONFIG);
        if (string.IsNullOrEmpty(jsonData)) return;

        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            for (int j = 3; j < rowData.Count; j++)
            {
                switch (values[i][2])
                {
                    case "StartItem":
                    {
                        var temp = AsListInt(rowData[j]);
                        if (temp[0] == 0) break;
                        cfg_startItem.Add((temp[0], temp[1]));
                        break;
                    }
                    case "WhoFirstLikeability":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_WhoFirstLikeability = temp;
                        break;
                    }
                    case "LikeabilityInterval":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_LikeabilityInterval.Add(temp);
                        break;
                    }
                    case "SinglePlay_MoreThan2Win_Free":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_SinglePlay_MoreThan2Win_Free.Add(temp);
                        break;
                    }
                    case "SinglePlay_MoreThan2Win_FreeAD":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_SinglePlay_MoreThan2Win_FreeAD.Add(temp);
                        break;
                    }
                    case "SinglePlay_MoreThan2Win_Paid":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_SinglePlay_MoreThan2Win_Paid.Add(temp);
                        break;
                    }
                    case "SinglePlay_LessThan2Win_Free":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_SinglePlay_LessThan2Win_Free.Add(temp);
                        break;
                    }
                    case "SinglePlay_LessThan2Win_FreeAD":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_SinglePlay_LessThan2Win_FreeAD.Add(temp);
                        break;
                    }
                    case "SinglePlay_LessThan2Win_Paid":
                    {
                        var temp = AsInt(rowData[j]);
                        if (temp == 0) break;
                        cfg_SinglePlay_LessThan2Win_Paid.Add(temp);
                        break;
                    }
                    case "Match_Story":
                    {
                        var temp = AsListInt(rowData[j]);
                        if (temp.Count == 0) break;
                        var selfIndex = temp[0];
                        temp.RemoveAt(0);
                        cfg_Match_Story.Add(selfIndex, new List<int>(temp));
                        break;
                    }
                }
            }
        }

        Debug.Log("success , QAConfig");
    }

    async UniTask<string> GetExcel(string _sheetName, string _range = null)
    {
        string url = GetURL(_sheetName, _range);
        UnityWebRequest request = UnityWebRequest.Get(url);
        await request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("구글엑셀 데이터읽기 실패.");
            return null;
        }
        else
        {
            return request.downloadHandler.text;
        }

        string GetURL(string _sheetName, string _range)
        {
            string _sheetId = Constants.EXCEL.SHEET_ID;
            string _apiKey = Constants.EXCEL.API_KEY;
            string s = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{_sheetName}?key={_apiKey}";
            if (_range != null)
            {
                s += $"&range={_range}";
            }

            return s;
        }
    }

    #region HelperMethods

    T AsEnum<T>(string str)
    {
        foreach (var temp in Enum.GetValues(typeof(T)))
        {
            if (str == temp.ToString())
                return (T) temp;
        }

        return default(T);
    }

    int AsInt(string str)
    {
        if (string.IsNullOrEmpty(str) == true)
        {
            return 0;
        }

        return Convert.ToInt32(str);
    }

    float AsFloat(string str)
    {
        if (string.IsNullOrEmpty(str) == true)
        {
            return 0.0f;
        }

        return Convert.ToSingle(str);
    }

    string AsString(string str)
    {
        if (true == string.IsNullOrEmpty(str))
        {
            return "";
        }

        return str;
    }

    List<int> AsListInt(string str)
    {
        List<int> result = new List<int>();

        if (string.IsNullOrEmpty(str) == true)
        {
            return result;
        }

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ')).Split(',');
        foreach (var s in temp)
        {
            var asValue = AsInt(s);
            result.Add(asValue);
        }

        return result;
    }

    List<string> AsListString(string str)
    {
        List<string> result = new List<string>();

        if (string.IsNullOrEmpty(str) == true)
        {
            return result;
        }

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ')).Split(',');
        foreach (var s in temp)
        {
            var asValue = AsString(s);
            result.Add(asValue);
        }

        return result;
    }

    List<float> AsListFloat(string str)
    {
        List<float> result = new List<float>();

        if (string.IsNullOrEmpty(str) == true)
        {
            return result;
        }

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ')).Split(',');
        foreach (var s in temp)
        {
            var asValue = AsFloat(s);
            result.Add(asValue);
        }

        return result;
    }

    List<T> AsListEnum<T>(string str)
    {
        List<T> result = new List<T>();

        if (string.IsNullOrEmpty(str) == true)
        {
            return result;
        }

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ')).Split(',');
        foreach (var s in temp)
        {
            var asValue = AsEnum<T>(s);
            result.Add(asValue);
        }

        return result;
    }

    #endregion
}