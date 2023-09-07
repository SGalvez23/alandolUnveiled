using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] ropeSegments;
    public int numLinks = 5;

    private void Awake()
    {
        GenerateRope();

        //for (int j = 0; j < ropeSegments.Length; j++)
        //{
            //ropeSegments[j].GetComponent<BoxCollider2D>().isTrigger = false;
        //}
    }

    private void GenerateRope()
    {
        Rigidbody2D prevBody = hook;

        for(int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0, ropeSegments.Length);
            GameObject newSeg = Instantiate(ropeSegments[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBody;

            prevBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}
