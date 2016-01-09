using UnityEngine;
using System.Collections;

public class MinionManger : MonoBehaviour {

    [HideInInspector]   public GameObject m_Instance;
    public MinionMoving m_Movement;
    public MinionManger[] m_group;
    private int m_wavenum;
    private Rigidbody try2;
    public void forceStop()
    {
        m_Movement.stop();
    }
    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<MinionMoving>();
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("GG");
        try2 = m_Movement.GetComponent<Rigidbody>();
        if (try2.isKinematic == true)
        {
            Debug.Log("FUUUUUUCKKKKING");
        }
	   
	}
}
