using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractQuest {
    
    public delegate void QuestStatusChange();
    public event QuestStatusChange questStatusChangeEvent;

    private bool isComplete;
    public bool IsComplete { get => isComplete; protected set { 
        if(isComplete == value) return;
        isComplete = value;
        CallStatusChangeEvent();
    } }

    protected virtual void OnAnimalDeath(Animal target, Animal killer) {
        
    }

    protected virtual void OnAnimalPossess(Animal possessed) {
        
    }

    protected void CallStatusChangeEvent() {
        if(questStatusChangeEvent != null)
            questStatusChangeEvent();
    }
    
}