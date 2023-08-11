using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbductStrategy 
{
    // Start is called before the first frame update
    public abstract void Captured(BeamCollider beamVar, GameObject go);
}
