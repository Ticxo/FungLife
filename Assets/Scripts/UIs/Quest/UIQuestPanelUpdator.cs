using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestPanelUpdator : MonoBehaviour {
    
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private VerticalLayoutGroup group;
    [SerializeField] private UIQuestUpdator questPrefab;
    [SerializeField] private QuestManager questManager;

    public void UpdatePanel() {
        var list = questManager.Quests;
        foreach(AbstractQuest quest in list) {
            UIQuestUpdator questUpdator = Instantiate<UIQuestUpdator>(questPrefab, gameObject.transform);
            questUpdator.Initialize(quest);
        }
        rectTransform.sizeDelta = new Vector2(0, list.Count * questPrefab.PanelHeight + (list.Count - 1) * group.spacing + group.padding.vertical);
    }

}
