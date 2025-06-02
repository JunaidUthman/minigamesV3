using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //------------- Game attributes ------------
    float cooldownTimer = 0;
    float fireDelay = 0.2f;
    public GameObject bullet;
    private bool shoot = false;

    void Update()
    {


        cooldownTimer -= Time.deltaTime; ;
        if ((Input.GetButton("Fire1") || shoot) && cooldownTimer <= 0 )
        {
            Vector3 offset = transform.rotation * new Vector3(0, 0.5f, 0);
            Instantiate(bullet, transform.position + offset, transform.rotation);
            cooldownTimer = fireDelay;
        }
    }

    public void OnShootDown() => shoot = true;
    public void OnShootUp() => shoot = false;
}
