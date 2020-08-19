using UnityEngine;

public class TurretBehaviourDefinition : MonoBehaviour
{
    public enum State
    {
        patrol, chase, shoot,
    }
    public float rotSpeed = 1f;
    public float chaseSpeed_modifier = 5f;
    private float thresholdAngle = 1f;
    private float shootRange = 5f;
    Vector2 desiredDir;
    private void Awake()
    {
        SetDesiredDirection();
    }

    public void SetDesiredDirection(){
        desiredDir = Random.insideUnitCircle;
    }

    public void Patrol(){
        //Patroling code
        float angle = Vector2.SignedAngle(transform.up,desiredDir);
        if(Mathf.Abs(angle)<thresholdAngle)
        {
            SetDesiredDirection();
        }
        else
            transform.Rotate(0,0,angle*rotSpeed*Time.deltaTime);
        print("Patroling");
    }
    public void Chase(Vector2 Direction){
        //Chasing code
        float angle = Vector2.SignedAngle(transform.up,Direction);
        if(Mathf.Abs(angle)<shootRange)
        {
            Shoot();
        }
        transform.Rotate(0,0,angle*rotSpeed*chaseSpeed_modifier*Time.deltaTime);
        print("Chasing");
    }
    public void Shoot(){
        //Shooting code
        print("Shooting");
    }
}
