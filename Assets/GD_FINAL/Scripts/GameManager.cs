using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int m_Score;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public float m_WaveDelay = 2f;
    int[,] CurrentWave = new int[8, 4];
    //public Transform m_TankPrefab;
    //public CameraControl m_CameraControl;
    public Transform m_Minion_Pink;
    public GameObject m_Minion_Blue;
    public GameObject m_Minion_Purple;
    private WaitForSeconds m_Wavewait;
    public GameObject m_Minion_Brown;
    public GameObject[] m_Spawnpoint;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private int wave=0;
    private int count = 0;
    private float nextActionTime = 0.0f;
    public float period = 1.0f;
    public MinionManger[] Minions;
	// Use this for initialization

    public void CallCollision(int wave)
    {
        for (int u = 0; u<Minions.Length; u++)
        {

        }
    }
	void Start () {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        m_Wavewait = new WaitForSeconds(m_WaveDelay);
        // m_testing.SetSpawnPoint();
        //m_testing.SpawnMinions();
        //SpawnMinions();
        StartCoroutine(GameLoop());
        Minions = new MinionManger[2000];

    }
	
	// Update is called once per frame
	void Update () {
	   /* if (Time.time > nextActionTime)
        {

            nextActionTime += period;
            SpawnMinions();
        }*/
	}
    public bool checkFixed()
    {
        return false;
    }
    public int[,] DestroyMinions()
    {
        return null;
    }
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
      //  yield return StartCoroutine(WavePalsing());
        yield return StartCoroutine(RoundEnding());

    }
    private IEnumerator RoundEnding()
    {
       

        yield return m_EndWait;
    }
    private IEnumerator RoundStarting()
    {
        /*
        ResetMap();
        resetTurn();
        */
        yield return m_StartWait;
    }
    private IEnumerator RoundPlaying()
    {
        /*
        ControlEnable();
        */
        bool isFinish = false;
        while (!isFinish)
        {
            SpawnMinions();
            yield return WavePalsing();
        }
        
    }
    private IEnumerator WavePalsing()
    {
        yield return m_Wavewait;
    }
    public void SpawnMinions()
    {
        MinionMoving[] group = new MinionMoving[4];
        Debug.Log("FUck off ass hole"+m_Spawnpoint.Length.ToString());
        int cate = Random.Range(0, 4);
        int counter = 0;
        CalMinions();
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (CurrentWave[x, y] == 1)
                {
                   MinionManger temp = new MinionManger();
                    temp.m_Instance=    Instantiate(m_Minion_Pink, m_Spawnpoint[x].transform.position+new Vector3(0,0,5.5f* y), Quaternion.identity) as GameObject;
                    temp.Setup();
                    group[counter] = temp.m_Movement;
                    counter++;
                }
            }
        }
        for (int u = 0; u<4; u++)
        {
            group[u].m_group = group;
        }
    }
    public void CalMinions()
    {
        int cate = Random.Range(0, 4);
        for (int u = 0; u < 8; u++)
            for (int v = 0; v < 4; v++)
            {
                CurrentWave[u, v] = 0;
            }
        int x = Random.Range(0, 7);
        int y = Random.Range(0, 3);
        CurrentWave[x, y] = 1;

        int len = 1;
        int[] now = { x, y };
        while (len < 4)
        {
            int[] next = { 0, 0 };
            int dir = Random.Range(1, 4);
            switch (dir)
            {
                case 1:
                    next[0] = 1;
                    break;
                case 2:
                    next[0] = -1;
                    break;
                case 3:
                    next[1] = 1;
                    break;
                case 4:
                    next[1] = -1;
                    break;
            }
            // Debug.Log(("The x :" + (now[0] + next[0]).ToString()));
            //  Debug.Log(("The y :" + (now[1] + next[1]).ToString()));
            if ((now[0] + next[0] >= 0) && (now[0] + next[0] < 8) && (now[1] + next[1] >= 0) && (now[1] + next[1] < 4))
            {
                if ((CurrentWave[now[0] + next[0], now[1] + next[1]] != 1))
                    len++;
                now[0] += next[0];
                now[1] += next[1];
                // Debug.Log(("The x :" + (now[0] - 1).ToString()));
                // Debug.Log(("The y :" + (now[1] - 1).ToString()));
                CurrentWave[now[0], now[1]] = 1;
            }

        }
        for (int u = 0; u < 8; u++)
            for (int v = 0; v < 4; v++)
            {
                // if (CurrentWave[u, v] == 1)
                //    Debug.Log("x: " + (u + 1).ToString() + "  , y =" + (1 + v).ToString());
            }
        // Debug.Log("===================================");
    }
    // Use this for initialization
}
