using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestBook : MonoBehaviour {
    
    [SerializeField] private RectTransform questPanelTransform;
    [SerializeField] private Vector2 openPosition;
    [SerializeField] private Vector2 closePosition;

    private bool isOpen = true;

    public void ToggleQuestPanel() {
        isOpen = !isOpen;
        questPanelTransform.anchoredPosition = isOpen ? openPosition : closePosition;
    }

}
