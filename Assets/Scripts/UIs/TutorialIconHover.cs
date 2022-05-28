using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialIconHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private string hoverMessage;

    public void OnPointerEnter(PointerEventData eventData) {
        panel.SetActive(true);
        dialogueBox.text = hoverMessage;
    }

    public void OnPointerExit(PointerEventData eventData) {
        panel.SetActive(false);
        dialogueBox.text = "";
    }
}
