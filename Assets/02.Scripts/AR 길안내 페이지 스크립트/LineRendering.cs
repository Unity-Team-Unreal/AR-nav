using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineRendering : MonoBehaviour
{
    [SerializeField] AREarthManager earthmanager;
    GeospatialPose pose = new GeospatialPose();
    Pose position;
    Vector3 vector;

    float latitude;
    float longitude;

    [SerializeField] GameObject anchorPrefab;

    [SerializeField] LineRenderer line;

    void Start()
    {
        AnchorSpawn();
        CreateLine();
    }

    void AnchorSpawn()
    {
        vector = new Vector3(0,0,0);
        pose.Latitude = latitude;
        pose.Longitude = longitude;

        position = earthmanager.Convert(pose);

        Instantiate(anchorPrefab, position.position, position.rotation);
        line.positionCount = POI.datalist.Count;

        for (int i = 0; i < POI.datalist.Count; i++)
        {
            line.SetPosition(i, vector);
        }

    }

    void CreateLine() 
    {
        for (int i = 0; i < POI.datalist.Count; i++)
        {
            line.SetPosition(i, vector);
        }
    } 
}
