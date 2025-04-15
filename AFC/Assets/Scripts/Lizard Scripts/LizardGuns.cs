using UnityEngine;
using Mirror;
using TMPro;
public class LizardGuns : NetworkBehaviour
{
    public GameObject revolver;
    public GameObject rifle;
    public GameObject intcam;
    public Camera playerCamera;
    public Vector3 target;
    public string thing_hit = "nothingyet";
    public float range;
    public float sndshots = 0;
    public float sndtime;
    public GameObject hitind;
    public GameObject hitfab;
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        range = GetComponentInParent<PewPew>().shootingRange;
    }

    // Update is called once per frame
    void Update()
    {
        sndtime += Time.deltaTime;
        if (sndtime >= .1 && sndshots > 0)
        {
            Secondary(transform.position, new Vector3(1, -1, 0));
        }
        if (GetComponentInParent<PewPew>().maingun)
        {
            This(transform.position, new Vector3(1, -1, 0));
        }
        if (GetComponentInParent<PewPew>().secondary)
        {
            Secondary(transform.position, new Vector3(1, -1, 0));
        }
    }

    void This(Vector3 playerPos, Vector3 offset)
    {
       
        intcam.GetComponent<intcamlookie>().xRotation = playerCamera.GetComponent<Lookie>().xRotation;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        playerCamera.GetComponent<Lookie>().recoil();


        if (Physics.Raycast(ray, out hit, range))
        {
            target = hit.transform.position;
            Debug.Log("Hit object tag: " + hit.collider.tag);
            thing_hit = (hit.collider.tag);


            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                enemy = hit.collider.gameObject;
                cmdchangehealth(enemy);
                //gets health script owner
                //hit.collider.gameObject.GetComponent<Health>().intcam = intcam;
                //hit.collider.gameObject.GetComponent<Health>().hepo -= GetComponentInParent<PewPew>().dmg;
                //cmdchangehealth()

                //hitind = Instantiate(hitfab, hit.point, Quaternion.identity); //Quaternion.RotateTowards(hitind.transform.rotation, hit.collider.transform.rotation., 360));
                //hitind.transform.rotation = intcam.transform.rotation;
                //hitind.GetComponent<TextMeshPro>().text = "40";
                // Logic for hitting a player
                // You can add additional actions here, like applying damage or triggering an effect
            }
        }

        //Ray raytwo = new Vector3(target)(new Vector3(offset));
    }
    [Command]
    private void cmdchangehealth(GameObject enemy)
    {
        enemy.GetComponent<Health>().hepo -= 40;
        enemy.GetComponent<Health>().intcam = intcam;
        Debug.Log("server contacted smacksmack");
    }
    void Secondary(Vector3 playerPos, Vector3 offset)
    {
        rifle.SetActive(false);
        revolver.SetActive(true);
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        sndshots += 1;
        sndtime = 0;
        if (sndshots == 6)
        {
            sndshots = 0;
            revolver.SetActive(false);
            rifle.SetActive(true);
        }
        GetComponentInParent<PewPew>().nospam = 0;
        playerCamera.GetComponent<Lookie>().secondaryrecoil();
        if (Physics.Raycast(ray, out hit, range))
        {
            target = hit.transform.position;
            Debug.Log("Hit object tag: " + hit.collider.tag);
            thing_hit = (hit.collider.tag);
            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                //gets health script owner
                hit.collider.gameObject.GetComponent<Health>().hepo -= GetComponentInParent<PewPew>().snddmg;
                hit.collider.gameObject.GetComponent<Health>().intcam = intcam;
                //hitind = Instantiate(hitfab, hit.point, Quaternion.identity); //Quaternion.RotateTowards(hitind.transform.rotation, hit.collider.transform.rotation., 360));
                //hitind.transform.rotation = intcam.transform.rotation;
                //hitind.GetComponent<TextMeshPro>().text = "10";
                // Logic for hitting a player
                // You can add additional actions here, like applying damage or triggering an effect
            }
        }
        //Ray raytwo = new Vector3(target)(new Vector3(offset));
    }
}
