using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoalRandomStroll : AbstractAIGoal {

    private Vector3 direction;

    public override bool CanEnable() {
        return Random.Range(0, 5) == 0;
    }

    public override void OnActivate() {
        Vector3 random = Random.insideUnitCircle;
        direction += random;
        direction.Normalize();
    }

    public override bool CanKeepTicking() {
        return true;
    }

    public override void OnTick() {
        owner.HandleControl(direction);
    }

    public override void OnDeactivate() {
        owner.StopHandleControl();
    }

}
