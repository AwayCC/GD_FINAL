using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct CamNavControlPoint
{
    public Vector3 Position;
    public Quaternion Rotation;
}

public class CameraControl : MonoBehaviour
{
    public GameObject[] m_Nodes;
    Vector2 swipeThreshold = new Vector2(0, 0);
    public Camera m_Camera;
    private float CurCamIdx = 0;
    private List<CamNavControlPoint> ControlPoints = new List<CamNavControlPoint>();
    public GameObject[] m_demoMinions;

    private bool startmoving = false;
    private float initTime = 0.0f;
    private const float CamPreBuffer = 0.5f;
    private const float movingIdx = 0.01f;
    private bool deleteDemo = false;

    public void startCameraMove()
    {
        startmoving = true;
        initTime = Time.frameCount;
    }
    void Start()
    {
        for(int u = 0; u < m_Nodes.Length; u++)
        {
            ControlPoints.Add(new CamNavControlPoint() { Position = m_Nodes[u].transform.position, Rotation = m_Nodes[u].transform.rotation });
        }

    }

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }


    private void FixedUpdate()
    {
        //Move();
        //Zoom();
        float startingTime = Time.frameCount - initTime;
        Debug.Log("current CamIdx: " + CurCamIdx.ToString() + ", starting time: " + startingTime + ", init time: " + initTime);
        if (startmoving == true)
        {
            if (startingTime > CamPreBuffer)
                Swipe( movingIdx );
        }
    }
    private void Swipe(float swipeVal)
    {
        if (CurCamIdx < 0.99f)
        {
            if(CurCamIdx>=0.5f && !deleteDemo)
            {
                for (int u = 0; u < m_demoMinions.Length; u++)
                    Destroy(m_demoMinions[u]);
                deleteDemo = true;
            }
            CurCamIdx += swipeVal;
            int floored = Mathf.FloorToInt(CurCamIdx);
            int ceilinged = Mathf.CeilToInt(CurCamIdx);
            m_Camera.transform.position = Vector3.Lerp(ControlPoints[floored].Position, ControlPoints[ceilinged].Position, CurCamIdx - floored);
            m_Camera.transform.rotation = Quaternion.Lerp(ControlPoints[floored].Rotation, ControlPoints[ceilinged].Rotation, CurCamIdx);
        }
    }
    
}