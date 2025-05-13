using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class Health : NetworkBehaviour
{
    private float regentimer = 0;
    public float hitcheck;
    [SyncVar(hook = nameof(OnHealthChanged))]
    public float hepo;
    public float maxhp;
    public MeshRenderer player;
    public float regencool = 0;
    public Image healthbar;
    public GameObject hitind;
    public GameObject hitfab;
    public float burn;
    public float burnTimer;
    public GameObject intcam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hepo = maxhp;
        player.enabled = true;
        hitcheck = hepo;
    }

    // Update is called once per frame
    void Update()
    {
        if (hepo >= maxhp)
        {
            hepo = maxhp;
            hitcheck = hepo;
        }
        regentimer += Time.deltaTime;
        regencool += Time.deltaTime;
        if (hepo < 1)
        {
            Debug.Log("OH SHIT OH FUCK IVE BEEN SHOT");
            player.enabled = false;
            hepo = maxhp;

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            hepo -= 10;
        }
        if (hepo < hitcheck)
        {
            float pain = hitcheck - hepo;
            hitind = Instantiate(hitfab, transform.position, Quaternion.identity); //Quaternion.RotateTowards(hitind.transform.rotation, hit.collider.transform.rotation., 360));
            hitind.transform.rotation = intcam.transform.rotation;
            hitind.GetComponent<TextMeshPro>().text = pain.ToString();
            hitcheck = hepo;
            regencool = 0;
        }
        if (regencool > 3 && regentimer >= .1 && hepo < maxhp)
        {
            hepo += 1;
            regentimer = 0;
            OnHealthChanged(hitcheck, hepo);
        }
        if (burn > 0)
        {
            if (burnTimer > 0)
            {
                burnTimer -= Time.deltaTime;
            }
            if (burnTimer <= 0)
            {
                burn -= 1;
                hepo -= 2;
                burnTimer = 1;
                Debug.Log("RingOfFire");
            }
            if (isLocalPlayer && healthbar != null)
            {
                healthbar.fillAmount = hepo / maxhp;
            }

        }
    }
    void OnHealthChanged(float oldHealth, float newHealth)
    {
        if (oldHealth > newHealth)
        {
            regencool = 0;
            regentimer = 0;
        }
        hitcheck = newHealth;
        if (isLocalPlayer && healthbar != null)
        {
            healthbar.fillAmount = newHealth / maxhp;
        }
    }
    [Server]
    public void TakeDamage(float amount, NetworkConnectionToClient shooterConn)
    {
        hepo -= amount;
        TargetShowHitIndicator(shooterConn, amount, intcam.transform.rotation);

        if (hepo <= 0)
        {
            // Die();
        }
    }
    [TargetRpc]
    void TargetShowHitIndicator(NetworkConnection target, float amount, Quaternion camRot)
    {
        GameObject hitind = Instantiate(hitfab, transform.position, Quaternion.identity);
        hitind.transform.rotation = camRot;
        hitind.GetComponent<TextMeshPro>().text = amount.ToString();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            GetComponentInParent<Player_Movement>().darted = true;
            GetComponentInParent<Player_Movement>().dartTimer = 3;
        }
    }
}
    


