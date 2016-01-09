using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinionMoving : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 12f;                 // How fast the tank moves forward and back.
    public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.

    private int[,] CurrentWave = new int[6, 4];
    private int count=1;
    public MinionMoving[] m_group = new MinionMoving[3];
    public GameManager root;
    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
    public bool isCollided=false;
    private Animation m_Running;
    public void stop()
    {
        OnCollisionAffected(new Collision());
    }
    void OnCollisionAffected(Collision col)
    {
        //Debug.Log("Collision Happened");
        isCollided = true;
        m_Running = GetComponent<Animation>();
        m_Running.Stop();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
    }
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Collision Happened");
        isCollided = true;
        m_Running = GetComponent<Animation>();
        m_Running.Stop();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
        for (int u = 0; u < 4; u++)
            if(m_group[u]!=this)
            m_group[u].stop();
    }
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }

    private void Affected(int type)
    {
        switch (type)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
    private void Start()
    {
    }


    private void Update()
    {
        // Store the value of both input axes.
        m_MovementInputValue = 1.0f;//Input.GetAxis(m_MovementAxisName);
       // m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
    }


    private void EngineAudio()
    {
       
    }


    private void FixedUpdate()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        var val = m_Rigidbody.velocity;
        // Debug.Log(val.ToString());
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        if (val.z >= 0f)
        {
            val.z = 0f;
            m_Rigidbody.velocity = val;
        }  
        if(!isCollided)
        Move();

    }


    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        /*
        for (int u = 0; u < 6; u++)
            for (int v = 0; v < 4; v++)
            {
                CurrentWave[u, v] = 0;
            }
        int x = Random.Range(0, 5);
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
            if ((now[0] + next[0] > 0) && (now[0] + next[0] <= 6) && (now[1] + next[1] > 0) && (now[0] + next[0] <= 4))
            {
                if((CurrentWave[now[0] + next[0], now[1] + next[1]] != 1))
                    len++;
                now[0] += next[0];
                now[1] += next[1];
               // Debug.Log(("The x :" + (now[0] - 1).ToString()));
               // Debug.Log(("The y :" + (now[1] - 1).ToString()));
                CurrentWave[now[0], now[1]] = 1;
            }
           
        }
        for (int u = 0; u < 6; u++)
            for (int v = 0; v < 4; v++)
            {
                if (CurrentWave[u, v] == 1)
                    Debug.Log("x: " + (u+1).ToString() + "  , y =" + (1+v).ToString());
            }
        Debug.Log("===================================");
    **/
    }
    
}