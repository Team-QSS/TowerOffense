using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour {

    Transform[] WayPoint_transform;

    int WayPointcount = 0;
    public float MoveSpeed = 2.0f;
    public float OriginalMoveSpeed;
    public bool isSlowed = false;

	// Use this for initialization
	void Awake () {
        int a = Random.Range(0 , WayPoint.points_count);

        WayPoint_transform = new Transform[WayPoint.count[a]];
        for (int i=0; i<WayPoint.count[a]; i++)
        {
            WayPoint_transform[i] = WayPoint.points[a,i];
        }
        OriginalMoveSpeed = MoveSpeed;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //다음 웨이포인트 방향으로 MoveSpeed만큼 움직임
        Vector3 vec = WayPoint_transform[WayPointcount].position - transform.position;

        transform.Translate(vec.normalized * (isSlowed ? MoveSpeed * 0.5f : MoveSpeed) * Time.deltaTime, Space.World);

        Quaternion rotation = Quaternion.LookRotation
            (WayPoint_transform[WayPointcount].position - transform.position,transform.TransformDirection(Vector3.back));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        transform.Rotate(new Vector3(0, 0, 90f));
        //다음 웨이포인트와의 거리가 0.2f이하면 웨이포인트를 움직임
        if (Vector3.Distance(WayPoint_transform[WayPointcount].position, transform.position) <= 0.2f)
        {
            NextWayPoint();
        }
	}

    void NextWayPoint()
    {
        //마지막 WayPoint 일 경우 물체를 파괴하고 끝
        if(WayPointcount >= WayPoint_transform.Length - 1)
        {
            CheckVictory checkVictory = Camera.main.GetComponent<CheckVictory>();
            checkVictory.goalUnitCount++;
            GameObject hpvar = GetComponent<PlayerScript>().HPvar;
            Destroy(hpvar);
            Destroy(transform.parent.gameObject);
            return;
        }

        WayPointcount++;
    }


    protected IEnumerator SlowDown()
    {
        float counter = 0;

        while (counter < 0.5f)
        {
            isSlowed = true;
            counter += Time.deltaTime;
            yield return null;
        }

        isSlowed = false;
    }
}
