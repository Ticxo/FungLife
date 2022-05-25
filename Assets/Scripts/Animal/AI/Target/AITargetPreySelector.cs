using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargetPreySelector : AbstractAITarget {

    public override Animal GetTarget() {
        return AnimalManager.instance.GetClosestTarget(owner, type.DetectionRadius);
    }

}
