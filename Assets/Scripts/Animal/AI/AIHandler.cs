using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour {

    [SerializeField] private bool isActive = true;
    [SerializeField] private Animal owner;
    [SerializeField] private List<AbstractAITarget> targetSelector;
    [SerializeField] private List<AbstractAIGoal> goalSelector;

    private float aiTickInterval;
    private AbstractAIGoal currentGoal;

    private void Start() {
        aiTickInterval = owner.GetAnimalType().DetectionInterval;
        InitializeAIs();
        StartCoroutine(TickAIGoals());
    }

    private void FixedUpdate() {
        if(currentGoal && currentGoal.CanKeepTicking())
            currentGoal.OnTick();
    }

    private void InitializeAIs() {
        foreach(AbstractAITarget aiTarget in targetSelector) {
            aiTarget.Initialize(owner);
        }
        foreach(AbstractAIGoal aiGoal in goalSelector) {
            aiGoal.Initialize(owner);
        }
    }

    private IEnumerator TickAIGoals() {
        while(isActive) {

            // Target Selector
            Animal target = null;
            foreach(AbstractAITarget aiTarget in targetSelector) {
                target = aiTarget.GetTarget();
                if(target != null) break;
            }
            if(owner.Target)
                owner.Target.Hunter = null;
            owner.Target = target;
            if(target)
                target.Hunter = owner;

            // Goal Selector
            foreach(AbstractAIGoal aiGoal in goalSelector) {
                if(!aiGoal.CanEnable()) continue;

                if(currentGoal && aiGoal != currentGoal) 
                    currentGoal.OnDeactivate();

                aiGoal.OnActivate();
                currentGoal = aiGoal;
                break;
            }

            yield return new WaitForSeconds(aiTickInterval);
        }
    }

}
