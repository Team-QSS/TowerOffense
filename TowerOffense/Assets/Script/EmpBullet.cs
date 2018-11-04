using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpBullet : BulletModel {

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (target)
            target.movement.StartCoroutine("SlowDown");
        Destroy(gameObject);
    }

}
