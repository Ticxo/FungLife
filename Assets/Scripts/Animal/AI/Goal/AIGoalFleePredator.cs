using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoalFleePredator : AbstractAIGoal {

    private Vector3 direction;

    public override bool CanEnable() {
        return owner.Hunter && owner.Hunter != owner.Target && !owner.IsPossessed;
    }

    public override void OnActivate() {
        Vector3 random = Random.insideUnitCircle;
        random.Normalize();
        direction = owner.transform.position - owner.Hunter.transform.position + random;
        direction.Normalize();
    }

    public override bool CanKeepTicking() {
        return CanEnable();
    }

    public override void OnTick() {
        owner.HandleControl(direction);
    }

    public override void OnDeactivate() {
        owner.StopHandleControl();
    }

}
