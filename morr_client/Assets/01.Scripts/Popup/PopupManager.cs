using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    private List<UIBase> popups = new List<UIBase>();
    private GameObject rootPanel;
    
    public void ShowPopupUI(string _popup = null)
    {
        GameObject go = null;
        //find
        {
            var find = popups.FirstOrDefault(x => x.name == _popup);
            if (find == default(UIBase))
            {
                rootPanel ??= GameObject.Find("Popups");
                GameObject temp = Resources.Load<GameObject>($"UI/{_popup}");
                go = Instantiate(temp, rootPanel.transform, false);
                go.name = go.name.Replace("(Clone)", "");
                find = go.GetComponent<UIBase>();
                popups.Add(find);
            }
            else
            {
                go = find.gameObject;
            }
        }
        
        //set order
        {
            go.transform.SetAsLastSibling();
            go.SetActive(true);
        }
    }

    public void ClosePopupUI(string _popup = null)
    {
        if (string.IsNullOrEmpty(_popup)) return;
        if(popups.Count == 0) return;

        var find = popups.FirstOrDefault(x => x.name == _popup);
        if (find == default(UIBase)) return;
        
        //set order
        find.transform.SetAsFirstSibling();
        find.gameObject.SetActive(false);
    }
}