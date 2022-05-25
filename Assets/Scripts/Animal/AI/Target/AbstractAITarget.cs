using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAITarget : MonoBehaviour {
    
    protected Animal owner;
    protected SOAnimal type;

    public virtual void Initialize(Animal owner) {
        this.owner = owner;
        type = owner.GetAnimalType();
    }

    public abstract Animal GetTarget();

}
