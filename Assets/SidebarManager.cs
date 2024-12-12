using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SidebarManager : MonoBehaviour
{
    public UIDocument uiDocument;
    public UIManager uiManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        var button = root.Q<Button>("open_league_menu");
        if (button != null)
        {
            button.clicked += uiManager.OpenLeagueSelectUI;
        }
    }
}
