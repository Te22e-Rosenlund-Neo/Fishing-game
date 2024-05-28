using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Boyancy : MonoBehaviour
{

    public Rigidbody rb;
    //where to experience bouyance:
    public float SubDepth;
    //amount of force applied:
    public float DisplacementForce;
    //points applying force
    public int points;

    public float WaterDrag;
    public float WaterAngularDrag;

    public WaterSurface water;

    //holds parameters when searching water surface
    WaterSearchParameters Wsearch;
    //stores result of water surface search
    WaterSearchResult Wresult;

    private void FixedUpdate()
    {
        //apply a distributed gravitational force
        rb.AddForceAtPosition(Physics.gravity / points, transform.position, ForceMode.Acceleration);

        //set up search parameters for projecting on water surface
        Wsearch.startPositionWS = transform.position;

        // project point onto water surface to get result
        water.ProjectPointOnWaterSurface(Wsearch, out Wresult);

        //if object is below water surface:
        if(transform.position.y < Wresult.projectedPositionWS.y){
            //calculate displacement multiplier based on submersion result
            float displacementMulti = Mathf.Clamp01((Wresult.projectedPositionWS.y - transform.position.y) /SubDepth) * DisplacementForce;

            //apply buyant force upwards
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y)*displacementMulti, 0f), transform.position, ForceMode.Acceleration);

            //apply water drag force against velocity
            rb.AddForce(displacementMulti * -rb.velocity * WaterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            //apply water angular drag torque against angular velocity
            rb.AddTorque(displacementMulti * -rb.angularVelocity * WaterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

        }








    }
}
