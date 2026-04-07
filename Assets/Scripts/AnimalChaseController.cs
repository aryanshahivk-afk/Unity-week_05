using ithappy.Animals_FREE;
using UnityEngine;

[RequireComponent(typeof(CreatureMover))]
[RequireComponent(typeof(CharacterController))]
public class AnimalChaseController : MonoBehaviour
{
    private CreatureMover m_Mover;
    private Vector2 m_Axis;
    private Vector3 m_Target;
    private bool m_IsRun = false;
    private bool m_IsJump = false;

    private bool timeOn = false;
    private float countDown = 0f;

    private Vector3 home = Vector3.zero;
    private GameObject player;
    private CharacterController cc;

    private void Awake()
    {
        m_Mover = GetComponent<CreatureMover>();
        cc = GetComponent<CharacterController>();
    }

    void Start()
    {
        home = transform.position;
        home.y += 1f;

        player = GameObject.FindWithTag("Player");
        timeOn = true;
        countDown = Random.Range(1.0f, 6.0f);
    }

    void Update()
    {
        if (timeOn)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0f)
            {
                timeOn = false;
                cc.enabled = false;
                transform.position = home;
                cc.enabled = true;
            }
            else
            {
                return;
            }
        }

        AnimalWalk();
    }

    private void AnimalWalk()
    {
        if (player == null) return;

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;
        Vector3 normDirection = distance > 0f ? direction / distance : Vector3.zero;

        m_Axis.x = normDirection.x;
        m_Axis.y = normDirection.z;
        m_Target = Vector3.zero;

        if (m_Mover != null)
        {
            m_Mover.SetInput(in m_Axis, in m_Target, in m_IsRun, m_IsJump);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);

            cc.enabled = false;
            transform.position = new Vector3(0f, -1000f, 0f);
            cc.enabled = true;

            timeOn = true;
            countDown = Random.Range(2.0f, 8.0f);
        }
    }
}