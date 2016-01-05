using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;
    public Slider m_LoadSlider;
    public int m_MaxShellNumber = 5;
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f;
    public float m_MaxChargeTime = 0.75f;
    public Color m_LoadColor = Color.yellow;
    public Image m_FillImage;

    public float m_MinLoadForce = 0.0f;
    public float m_MaxLoadForce = 100f;
    public float m_MaxLoadTime = 0.5f;

    
    private string m_FireButton;         
    private float m_CurrentLaunchForce;
    private float m_CurrentLoadForce;
    private float m_ChargeSpeed;
    private float m_LoadSpeed;      
    private bool m_Fired;
    private int m_CurrentShell;

    private void OnEnable()
    {
        m_CurrentLoadForce = m_MinLoadForce;
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        m_LoadSlider.value = m_MinLoadForce;
        m_CurrentShell = m_MaxShellNumber;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        m_LoadSpeed = (m_MaxLoadForce - m_MinLoadForce) / m_MaxLoadTime;
    }
    

    private void Update()
    {

        m_LoadSlider.value = m_MinLoadForce;
        if(m_CurrentShell<m_MaxShellNumber)
        {
            m_LoadSlider.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
            m_CurrentLoadForce += m_LoadSpeed * Time.deltaTime;
            m_LoadSlider.value = m_CurrentLoadForce;
            m_FillImage.color = m_LoadColor;
        }
        else
        {
            m_LoadSlider.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
        }
        if (m_CurrentLoadForce>= m_MaxLoadForce)
        {
            m_CurrentShell++;
            m_CurrentLoadForce = 0;
        }
        m_AimSlider.value = m_MinLaunchForce;
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && ! m_Fired)
        {
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(m_FireButton)){
            m_Fired=false;
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        else if (Input.GetButton(m_FireButton)&&!m_Fired)
        {
            m_CurrentLaunchForce += m_ChargeSpeed *Time.deltaTime;
            m_AimSlider.value = m_CurrentLaunchForce;
        }
        else if (Input.GetButtonUp(m_FireButton)&& !m_Fired){
            Fire();
        }

        // Track the current state of the fire button and make decisions based on the current launch force.
    }


    private void Fire()
    {
        if (m_CurrentShell == 0)
            return;
        m_Fired = true ;
        Rigidbody shellInstance = Instantiate(m_Shell,m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        m_CurrentShell--;
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        m_CurrentLaunchForce =m_MinLaunchForce;
        // Instantiate and launch the shell.
    }
}