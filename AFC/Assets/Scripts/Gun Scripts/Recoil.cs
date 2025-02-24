using UnityEngine;


public class Recoil : MonoBehaviour
{
    public Transform recoilMod;
    public GameObject weapon;
    public float maxRecoil_x;
    public float recoilSpeed;
    public float recoil;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //every time you fire a bullet, add to the recoil.. of course you can probably set a max recoil etc..
            recoil += 0.1f;
        }

        recoiling();
    }

    void recoiling()
    {
        if (recoil > 0)
        {
            var maxRecoil = Quaternion.Euler(maxRecoil_x, 0, 0);
            // Dampen towards the target rotation
            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, maxRecoil, Time.deltaTime * recoilSpeed);
            weapon.transform.localEulerAngles = recoilMod.localEulerAngles;
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0;
            var minRecoil = Quaternion.Euler(0, 0, 0);
            // Dampen towards the target rotation
            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, minRecoil, Time.deltaTime * recoilSpeed / 2);
            weapon.transform.localEulerAngles = recoilMod.localEulerAngles;
        }
    }
}