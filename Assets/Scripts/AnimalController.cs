using ithappy.Animals_FREE;
using UnityEngine;

[RequireComponent(typeof(CreatureMover))]
public class AnimalController : MonoBehaviour
{
    private CreatureMover m_Mover;
    private Vector2 m_Axis;
    private Vector3 m_Target;
    private bool m_IsRun = false;
    private bool m_IsJump = false;

    [SerializeField] private GameObject waypoint1;
    [SerializeField] private GameObject waypoint2;
    [SerializeField] private GameObject waypoint3;
    [SerializeField] private GameObject waypoint4;
    [SerializeField] private GameObject target;

    private const float CLOSE_DISTANCE = 1f;

    [SerializeField] private float WaitTime = 4f;
    private float currentTime = 0f;

    private void Awake()
    {
        m_Mover = GetComponent<CreatureMover>();
        currentTime = WaitTime;
    }

    public void BindMover(CreatureMover mover)
    {
        m_Mover = mover;
    }

    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;
        Vector3 normDirection = distance > 0f ? direction / distance : Vector3.zero;

        m_Axis.x = normDirection.x;
        m_Axis.y = normDirection.z;
        m_Target = Vector3.zero;

        if (distance < CLOSE_DISTANCE)
        {
            if (currentTime <= 0f)
            {
                if (target == waypoint1) target = waypoint2;
                else if (target == waypoint2) target = waypoint3;
                else if (target == waypoint3) target = waypoint4;
                else if (target == waypoint4) target = waypoint1;

                currentTime = WaitTime;
            }
            else
            {
                currentTime -= Time.deltaTime;
                m_Axis.x = 0f;
                m_Axis.y = 0f;
            }
        }

        if (m_Mover != null)
        {
            m_Mover.SetInput(in m_Axis, in m_Target, in m_IsRun, m_IsJump);
        }
    }
}