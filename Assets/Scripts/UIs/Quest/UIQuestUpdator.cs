using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestUpdator : MonoBehaviour {
    
    [SerializeField] private RectTransform rectTransform;
    [Header("Quest")]
    [SerializeField] private TextMeshProUGUI dialogue;
    [Header("Icon")]
    [SerializeField] private Image completionImage;
    [SerializeField] private Sprite notCompleteIcon;
    [SerializeField] private Sprite completeIcon;

    private AbstractQuest quest;
    public AbstractQuest Quest { get => quest; }
    
    public float PanelHeight { get => rectTransform.sizeDelta.y; }

    public void Initialize(AbstractQuest quest) {
        this.quest = quest;
        quest.questStatusChangeEvent += UpdatePanel;
        UpdatePanel();
    }

    private void UpdatePanel() {
        dialogue.text = quest.ToString();
        completionImage.sprite = quest.IsComplete ? completeIcon : notCompleteIcon;
    }
}
