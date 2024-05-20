using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventIntermediary : MonoBehaviour
{
    public Shark parents_script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAttack()
    {
        parents_script.StopAttack();
    }
}
