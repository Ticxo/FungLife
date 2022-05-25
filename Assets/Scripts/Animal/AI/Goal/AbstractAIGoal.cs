using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAIGoal : MonoBehaviour {
    
    protected Animal owner;
    protected SOAnimal type;

    public virtual void Initialize(Animal owner) {
        this.owner = owner;
        type = owner.GetAnimalType();
    }

    public abstract bool CanEnable();
    
    public virtual void OnActivate() {

    }

    public abstract bool CanKeepTicking();

    public virtual void OnTick() {

    }

    public virtual void OnDeactivate() {

    }

}
