using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AgentPath : MonoBehaviour
{

    /// <summary>
    /// Use the transforms of children GameObjects in 3d space as agent path points on Catmull–Rom spline
    /// </summary>

    //How many points you want on the curve between two children objects
    public float amountOfPoints = 30.0f;

    //set from 0-1
    public float alpha = 0.5f;

    //Points is the transform of all children objects
    private List<Transform> points = new List<Transform>();

    //Store points on the Catmull curve in newPoints
    List<Vector3> newPoints = new List<Vector3>();

    //Target list is a copy of newpoints to be used in other scripts
    public List<Vector3> target = new List<Vector3>();

    public void CatmulRom()
    {
        newPoints.Clear();

        //next two lines use System.Linq to query the children objects with tag of"cone" 
        //Transform[] tempName = GetComponentsInChildren<Transform>().Where(r => r.tag == "cone").ToArray();
        //Debug.Log(tempName[1].transform.position);

        /*
        //get all transform from children objects, but minus the parent object itself
        Transform[] allpoints = GetComponentsInChildren<Transform>();
        points = new List<Transform>();

        for(int i = 0; i < allpoints.Length; i++)
        {
            if(allpoints[i]!=transform)
            {
                points.Add(allpoints[i]);
            }
        }

        */
        points = GetComponentsInChildren<Transform>().Where(r => r.tag == "location").ToList();

        // calculate the catmull curve

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 p0;
            Vector3 p1;
            Vector3 p2;
            Vector3 p3;

            if (i == 0)
            {
                p0 = points[points.Count - 1].position;
                p1 = points[i].position;
                p2 = points[i + 1].position;
                p3 = points[i + 2].position;
            }
            else if (i == (points.Count - 1))
            {
                p0 = points[points.Count - 2].position;
                p1 = points[i].position;
                p2 = points[0].position;
                p3 = points[1].position;
            }
            else if (i == (points.Count - 2))
            {
                p0 = points[points.Count - 3].position;
                p1 = points[i].position;
                p2 = points[i + 1].position;
                p3 = points[0].position;
            }
            else
            {
                p0 = points[i - 1].position;
                p1 = points[i].position;
                p2 = points[i + 1].position;
                p3 = points[i + 2].position;
            }
      

            float t0 = 0.0f;
            float t1 = GetT(t0, p0, p1);
            float t2 = GetT(t1, p1, p2);
            float t3 = GetT(t2, p2, p3);

            for (float t = t1; t < t2; t += ((t2 - t1) / amountOfPoints))
            {
                Vector3 A1 = (t1 - t) / (t1 - t0) * p0 + (t - t0) / (t1 - t0) * p1;
                Vector3 A2 = (t2 - t) / (t2 - t1) * p1 + (t - t1) / (t2 - t1) * p2;
                Vector3 A3 = (t3 - t) / (t3 - t2) * p2 + (t - t2) / (t3 - t2) * p3;

                Vector3 B1 = (t2 - t) / (t2 - t0) * A1 + (t - t0) / (t2 - t0) * A2;
                Vector3 B2 = (t3 - t) / (t3 - t1) * A2 + (t - t1) / (t3 - t1) * A3;

                Vector3 C = (t2 - t) / (t2 - t1) * B1 + (t - t1) / (t2 - t1) * B2;

                newPoints.Add(C);

            }

        }

        // make copy of newpoints
        target = newPoints;
    }

    float GetT(float t, Vector3 p0, Vector3 p1)
    {
        float a = Mathf.Pow((p1.x - p0.x), 2.0f) + Mathf.Pow((p1.y - p0.y), 2.0f) + Mathf.Pow((p1.z - p0.z), 2.0f);
        float b = Mathf.Pow(a, 0.5f);
        float c = Mathf.Pow(b, alpha);

        return (c + t);
    }

    //Visualize the points in editor

    void OnDrawGizmos()
    //void OnDrawGizmosSelected()
    {

        CatmulRom();
        Gizmos.color = Color.white;
        foreach (Vector3 temp in newPoints)
        {
            Vector3 pos = new Vector3(temp.x, temp.y, temp.z);
            //Debug.Log(pos);
            Gizmos.DrawSphere(pos, 0.5f);
        }

        for (int i = 0; i < newPoints.Count; i++)
        {
            Vector3 currentPos = newPoints[i];
            Vector3 previousPos = Vector3.zero;

            if (i > 0)
            {
                previousPos = newPoints[i - 1];
            }
            else if (i == 0)
            {
                previousPos = newPoints[newPoints.Count - 1];
            }
            Gizmos.DrawLine(previousPos, currentPos);
        }
    }
}