using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviourManager : MonoBehaviour
{
    private TurretBehaviourDefinition behaviourObject;
    private TurretBehaviourDefinition.State currState;
    private EnemyDetector detector;
    // Start is called before the first frame update
    void Start()
    {
        behaviourObject = gameObject.GetComponentInParent<TurretBehaviourDefinition>();
        currState = TurretBehaviourDefinition.State.patrol;
        detector = GetComponentsInChildren<EnemyDetector>()[0];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currState)
        {
            case TurretBehaviourDefinition.State.patrol:{
                if(detector.isVisible)
                    currState = TurretBehaviourDefinition.State.chase;
                else
                    behaviourObject.Patrol();
            }break;
            case TurretBehaviourDefinition.State.chase:{
                if(!detector.isVisible)
                {
                    behaviourObject.SetDesiredDirection();
                    currState = TurretBehaviourDefinition.State.patrol;
                }
                else
                {
                    behaviourObject.Chase(detector.Direction);
                }
            }break;
            case TurretBehaviourDefinition.State.shoot:{

            }break;
        }
    }
}
