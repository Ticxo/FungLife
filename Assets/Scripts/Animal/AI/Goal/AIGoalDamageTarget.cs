using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoalDamageTarget : AbstractAIGoal {

    private float radiusSqr;

    public override void Initialize(Animal owner) {
        base.Initialize(owner);
        radiusSqr = type.AttackRange * type.AttackRange;
    }

    public override bool CanEnable() {
        return owner.IsPossessed || (owner.Target && Vector3.SqrMagnitude(owner.Target.transform.position - owner.transform.position) <= radiusSqr);
    }

    public override void OnActivate() {
        if(owner.IsPossessed) {
            owner.AttackNearby();
        }else {
            owner.Attack(owner.Target);
        }
    }

    public override bool CanKeepTicking() {
        return false;
    }

    public override void OnTick() {
        
    }

}
