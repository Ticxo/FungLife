using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoalChaseTarget : AbstractAIGoal {

    public override bool CanEnable() {
        return owner.Target && !owner.IsPossessed;
    }

    public override bool CanKeepTicking() {
        return CanEnable();
    }

    public override void OnTick() {
        Vector3 direction = owner.Target.transform.position - owner.transform.position;
        direction.Normalize();
        owner.HandleControl(direction);
    }

    public override void OnDeactivate() {
        owner.StopHandleControl();
    }

}
