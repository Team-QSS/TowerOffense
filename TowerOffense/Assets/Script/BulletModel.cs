using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletModel : MonoBehaviour {
    public TowerAI callme = null;
    protected WaitForSeconds WaitForBulletDispose;
    public float speed = 45.0f;
    protected PlayerScript target;

    // Use this for initialization
    protected virtual void Awake () {
        WaitForBulletDispose = new WaitForSeconds(callme.range / speed);
        StartCoroutine(Destroy());
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        transform.Translate(new Vector3(0, 1).normalized * speed * Time.deltaTime , Space.Self);
	}

    protected virtual IEnumerator Destroy()
    {
        yield return WaitForBulletDispose;
        Destroy(transform.gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        target = collision.gameObject.GetComponent<PlayerScript>();

        if (!target) return;

        if (collision.gameObject.tag == "Shield")
        {
            int a = Random.Range(0, 6);

            int T = new UpgradeList(new FileLoader().Load()).IncreaseEffect;

            if (a <= T)
            {
                Destroy(gameObject);
                return;
            }
        }
        target.DealDmg(callme.Damage);
    }
}
