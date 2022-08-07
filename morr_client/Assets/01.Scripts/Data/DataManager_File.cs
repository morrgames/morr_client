using System;
using System.Collections.Generic;
using System.Linq;
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

    private List<(int unitIndex, int likeable)> cfg_Likeability = new List<(int unitIndex, int likeable)>();
    private Dictionary<int, List<int>> cfg_Match_Story = new Dictionary<int, List<int>>(); //unitIndex, targetIndexs...
    private int cfg_WhoFirstLikeability = 0;

    private List<int> cfg_LikeabilityInterval = new List<int>();

    //Free
    private List<int> cfg_SinglePlay_Free_LessThan2Win_NoAD = new List<int>(); //경험치,
    private List<int> cfg_SinglePlay_Free_LessThan2Win_YesAD = new List<int>(); //경험치,상대캐릭터호감도
    private List<int> cfg_SinglePlay_Free_MoreThan2Win_NoAD = new List<int>(); //경험치,상대캐릭터호감도
    private List<int> cfg_SinglePlay_Free_MoreThan2Win_YesAD = new List<int>(); //경험치

    //Paid
    private List<int> cfg_SinglePlay_Paid_LessThan2Win = new List<int>(); //경험치,상대캐릭터호감도
    private List<int> cfg_SinglePlay_Paid_MoreThan2Win = new List<int>(); //경험치,상대캐릭터호감도
    //


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
        string jsonData = await GetExcel(Constants.EXCEL.SKILL,"A1:Z100");
        if (string.IsNullOrEmpty(jsonData)) return;

        dicSkill.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;

        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var rowDic = new Dictionary<string, string>();
            values[0].ForEach(x => rowDic.Add(x, ""));
            for (int j = 0; j < rowData.Count; j++)
            {
                rowDic[values[0][j]] = rowData[j];
            }

            var rtData = new RTSkill();
            rtData.m_Index = AsInt(rowDic["Index"]);
            rtData.m_GroupIndex = AsInt(rowDic["GroupIndex"]);
            rtData.m_ViewName = AsString(rowDic["ViewName"]);
            rtData.m_ViewDesc = AsString(rowDic["ViewDesc"]);
            rtData.m_ViewEffect = AsString(rowDic["ViewEffect"]);
            rtData.m_SkillType = AsEnum<RTSkillType>(rowDic["SkillType"]);
            rtData.m_Likeability = AsEnum<RTLikeability>(rowDic["Likeability"]);
            rtData.m_UseCondition = AsEnum<RTUseCondition>(rowDic["UseCondition"]);
            rtData.m_DamageType = AsEnum<RTDamageType>(rowDic["DamageType"]);
            rtData.m_DamageValue = AsInt(rowDic["DamageValue"]);
            rtData.m_LimitCount = AsInt(rowDic["LimitCount"]);
            rtData.m_RangePivot = AsEnum<RTRangePivot>(rowDic["RangePivot"]);
            rtData.m_Range = AsListString(rowDic["Range"]);
            rtData.m_BuffCondition_1 = AsEnum<RTBuffCondition>(rowDic["BuffCondition_1"]);
            rtData.m_BuffTimeType_1 = AsEnum<RTBuffTimeType>(rowDic["BuffTimeType_1"]);
            rtData.m_BuffTime_1 = AsInt(rowDic["BuffTime_1"]);
            rtData.m_BuffTarget_1 = AsEnum<RTBuffTarget>(rowDic["BuffTarget_1"]);
            rtData.m_BuffType_1 = AsEnum<RTBuffType>(rowDic["BuffType_1"]);
            rtData.m_BuffValue_1 = AsString(rowDic["BuffValue_1"]);
            rtData.m_BuffCondition_2 = AsEnum<RTBuffCondition>(rowDic["BuffCondition_2"]);
            rtData.m_BuffTimeType_2 = AsEnum<RTBuffTimeType>(rowDic["BuffTimeType_2"]);
            rtData.m_BuffTime_2 = AsInt(rowDic["BuffTime_2"]);
            rtData.m_BuffTarget_2 = AsEnum<RTBuffTarget>(rowDic["BuffTarget_2"]);
            rtData.m_BuffType_2 = AsEnum<RTBuffType>(rowDic["BuffType_2"]);
            rtData.m_BuffValue_2 = AsString(rowDic["BuffValue_2"]);

            dicSkill.Add(rtData.m_Index, rtData);
        }

        Debug.Log("load success , skill");
    }

    async UniTask GetExcel_Language()
    {
        string jsonData = await GetExcel(Constants.EXCEL.LANGUAGE,"A1:E8");
        if (string.IsNullOrEmpty(jsonData)) return;

        dicLanguage.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var rowDic = new Dictionary<string, string>();
            values[0].ForEach(x => rowDic.Add(x, ""));
            for (int j = 0; j < rowData.Count; j++)
            {
                rowDic[values[0][j]] = rowData[j];
            }

            var rtData = new RTLanguage();

            rtData.m_Index = AsInt(rowDic["Index"]);
            rtData.m_Key = AsString(rowDic["Key"]);
            rtData.m_Korean = AsString(rowDic["Korean"]);
            rtData.m_English = AsString(rowDic["English"]);

            dicLanguage.Add(rtData.m_Index, rtData);
        }


        Debug.Log("success , language");
    }

    async UniTask GetExcel_Unit()
    {
        string jsonData = await GetExcel(Constants.EXCEL.UNIT,"A1:G14");
        if (string.IsNullOrEmpty(jsonData)) return;

        dicLanguage.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            //
            var rowDic = new Dictionary<string, string>();
            values[0].ForEach(x => rowDic.Add(x, ""));
            for (int j = 0; j < rowData.Count; j++)
            {
                rowDic[values[0][j]] = rowData[j];
            }

            var rtData = new RTUnit();
            //
            rtData.m_Index = AsInt(rowDic["Index"]);
            rtData.m_ViewName = AsString(rowDic["ViewName"]);
            rtData.m_SkillGroupIndex = AsInt(rowDic["SkillGroupIndex"]);
            rtData.m_RequireItemIndex = AsInt(rowDic["RequireItemIndex"]);
            rtData.m_RequireCount = AsInt(rowDic["RequireCount"]);
            rtData.m_Resource = AsString(rowDic["Resource"]);

            dicUnit.Add(rtData.m_Index, rtData);
        }

        Debug.Log("success , Unit");
    }

    async UniTask GetExcel_Item()
    {
        string jsonData = await GetExcel(Constants.EXCEL.ITEM,"A1:E15");
        if (string.IsNullOrEmpty(jsonData)) return;

        dicItem.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            //
            var rowDic = new Dictionary<string, string>();
            values[0].ForEach(x => rowDic.Add(x, ""));
            for (int j = 0; j < rowData.Count; j++)
            {
                rowDic[values[0][j]] = rowData[j];
            }

            var rtData = new RTItem();
            rtData.m_Index = AsInt(rowDic["Index"]);
            rtData.m_ViewName = AsString(rowDic["ViewName"]);
            rtData.m_ViewDesc = AsString(rowDic["ViewDesc"]);
            rtData.m_IconPath = AsString(rowDic["IconPath"]);

            dicItem.Add(rtData.m_Index, rtData);
        }

        Debug.Log("success , Item");
    }

    async UniTask GetExcel_GroundProperty()
    {
        string jsonData = await GetExcel(Constants.EXCEL.GROUND_PROPERTY,"A1:E94");
        if (string.IsNullOrEmpty(jsonData)) return;

        dicLanguage.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.
            var rowDic = new Dictionary<string, string>();
            values[0].ForEach(x => rowDic.Add(x, ""));
            for (int j = 0; j < rowData.Count; j++)
            {
                rowDic[values[0][j]] = rowData[j];
            }

            var rtData = new RTGroundProperty();

            rtData.m_Index = AsInt(rowDic["Index"]);
            rtData.m_AttackerUnitIndex = AsInt(rowDic["AttackerUnitIndex"]);
            rtData.m_DefenderUnitIndex = AsInt(rowDic["DefenderUnitIndex"]);
            rtData.m_MergeResult = AsEnum<RTMergeResult>(rowDic["MergeResult"]);

            dicGroundProperty.Add(rtData.m_Index, rtData);
        }

        Debug.Log("success , GroundProperty");
    }

    async UniTask GetExcel_Scenario()
    {
        string jsonData = await GetExcel(Constants.EXCEL.SCENARIO,"A1:J8");
        if (string.IsNullOrEmpty(jsonData)) return;

        dicScenario.Clear();
        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var rowDic = new Dictionary<string, string>();
            values[0].ForEach(x => rowDic.Add(x, ""));
            for (int j = 0; j < rowData.Count; j++)
            {
                rowDic[values[0][j]] = rowData[j];
            }

            var rtData = new RTScenario();

            rtData.m_Index = AsInt(rowDic["Index"]);
            rtData.m_OwnerUnitIndex = AsInt(rowDic["OwnerUnitIndex"]);
            rtData.m_Likeability = AsEnum<RTLikeability>(rowDic["Likeability"]);
            rtData.m_TargetUnitIndex = AsInt(rowDic["TargetUnitIndex"]);
            rtData.m_TargetUnitIndex = AsInt(rowDic["TargetUnitIndex"]);
            for (int j = 1; j <= 5; j++)
            {
                var temp = AsListString(rowDic[$"Message_{j}"]);
                if (temp == null || temp.Count == 0) continue;
                var unitIndex = AsInt(temp[0]);
                var message = temp[1];
                rtData.m_Values.Add((unitIndex, message));
            }

            dicScenario.Add(rtData.m_Index, rtData);
        }

        Debug.Log("success , Scenario");
    }

    async UniTask GetExcel_QAConfig()
    {
        string jsonData = await GetExcel(Constants.EXCEL.QACONFIG,"A1:M16");
        if (string.IsNullOrEmpty(jsonData)) return;

        var values = JsonConvert.DeserializeObject<GoogleSheet>(jsonData).values;
        //6행부터 데이터입력.
        for (int i = 5; i < values.Count; i++)
        {
            var rowData = values[i];
            if (rowData.Count == 0) continue;
            if (string.IsNullOrEmpty(rowData[0])) continue; //인덱스가 비었으면 패스.

            var rowDic = new Dictionary<string, string>();
            values[0].ForEach(x => rowDic.Add(x, ""));
            for (int j = 0; j < rowData.Count; j++)
            {
                rowDic[values[0][j]] = rowData[j];
            }

            var rtData = new RTQAConfig();
            rtData.m_Index = AsInt(rowDic["Index"]);
            rtData.m_Name = AsString(rowDic["Name"]);
            for (int j = 1; j <= 10; j++)
            {
                rtData.m_Values.Add(AsString(rowDic[$"v{j}"]));
            }

            switch (rtData.m_Name)
            {
                case "WhoFirstLikeability":
                {
                    cfg_WhoFirstLikeability = AsInt(rtData.m_Values[0]);
                    break;
                }
                case "LikeabilityInterval":
                {
                    rtData.m_Values.ForEach(x => cfg_LikeabilityInterval.Add(AsInt(x)));
                    break;
                }
                //free
                case "SinglePlay_Free_LessThan2Win_NoAD":
                {
                    rtData.m_Values.ForEach(x => cfg_SinglePlay_Free_LessThan2Win_NoAD.Add(AsInt(x)));
                    break;
                }
                case "SinglePlay_Free_LessThan2Win_YesAD":
                {
                    rtData.m_Values.ForEach(x => cfg_SinglePlay_Free_LessThan2Win_NoAD.Add(AsInt(x)));
                    break;
                }
                case "SinglePlay_Free_MoreThan2Win_NoAD":
                {
                    rtData.m_Values.ForEach(x => cfg_SinglePlay_Free_MoreThan2Win_NoAD.Add(AsInt(x)));
                    break;
                }
                case "SinglePlay_Free_MoreThan2Win_YesAD":
                {
                    rtData.m_Values.ForEach(x => cfg_SinglePlay_Free_MoreThan2Win_YesAD.Add(AsInt(x)));
                    break;
                }
                //paid
                case "SinglePlay_Paid_LessThan2Win":
                {
                    rtData.m_Values.ForEach(x => cfg_SinglePlay_Paid_LessThan2Win.Add(AsInt(x)));
                    break;
                }
                case "SinglePlay_Paid_MoreThan2Win":
                {
                    rtData.m_Values.ForEach(x => cfg_SinglePlay_Paid_MoreThan2Win.Add(AsInt(x)));
                    break;
                }
                case "Match_Story":
                {
                    rtData.m_Values.ForEach(x =>
                    {
                        var temp = AsListInt(x);
                        if (temp == null || temp.Count == 0) return;
                        cfg_Match_Story.Add(temp[0],new List<int>());
                        cfg_Match_Story[temp[0]] = temp.Skip(1).ToList();
                    });
                    break;
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
                //s += $"&range={_range}";//왜인지..range추가하면, 때때로 다른시트 데이터 읽어옴.그래서 주석처리.
            }

            return s;
        }
    }

    #region HelperMethods

    T AsEnum<T>(string str) where T : Enum
    {
        //숫자를 Enum으로.
        return (T) Enum.ToObject(typeof(T), AsInt(str));
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

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ','"', '\'')).Split(',');
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

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ', '"', '\'')).Split(',');
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

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ','"', '\'')).Split(',');
        foreach (var s in temp)
        {
            var asValue = AsFloat(s);
            result.Add(asValue);
        }

        return result;
    }

    List<T> AsListEnum<T>(string str) where T : Enum
    {
        List<T> result = new List<T>();

        if (string.IsNullOrEmpty(str) == true)
        {
            return result;
        }

        string[] temp = string.Join("", str.Split('[', ']', '{', '}', ' ','"', '\'')).Split(',');
        foreach (var s in temp)
        {
            var asValue = AsEnum<T>(s);
            result.Add(asValue);
        }

        return result;
    }

    #endregion
}