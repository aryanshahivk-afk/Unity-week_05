using UnityEngine; 
 
public class ProjectileLauncher : MonoBehaviour 
{ 
    [SerializeField] 
    private Rigidbody projectileRigidBody; 
    [SerializeField] 
    private float projectilePower = 3000; 
    [SerializeField] 
    private GameObject muzzle; 
 
    [SerializeField] 
    private float COOLDOWN_TIME = 0.5f; 
    private float coolDown = 0; 
 
    // Start is called once before the first 
    // execution of Update after the MonoBehaviour is created 
    void Start() 
    { 
         
    } 
 
    // Update is called once per frame 
    void Update() 
    { 
        if (coolDown <= 0) 
        { 
            if (Input.GetButtonUp("Fire1")) 
            { 
                coolDown = COOLDOWN_TIME; 
 
                // Instantiate the projectile. 
                Rigidbody aInstance = Instantiate(projectileRigidBody, 
                        muzzle.transform.position, transform.rotation) as Rigidbody; 
 
                // Add force. 
                Vector3 forward = transform.TransformDirection(Vector3.forward); 
                aInstance.AddForce(forward * projectilePower); 
 
                // Destroy the object after X seconds. 
                Destroy(aInstance.gameObject, 8); 
            } 
        } 
        else 
        { 
            coolDown = coolDown - Time.deltaTime; 
        } 
 
    } 
} 