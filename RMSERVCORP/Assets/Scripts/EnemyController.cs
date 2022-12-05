using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    // Start is called before the first frame update


    [SerializeField] public Animator myAnimationController;

    public Vector3[] waypoints;
    int current = 0;
    public float speed;
    public float missileSpeed;
    float AcceptableDistance = 1;

    private Vector3 pos1 = new Vector3(-340, 0.1099997f, 196);
    private Vector3 pos2 = new Vector3(-300, 0.1099997f, 196);
    

   
    public Transformation trans;


    public Transform truck;
    public Transform robot;
    public Transform player;

    public ParticleSystem explosion;

    public GameObject rocketPrefab;

    public float damage;


    private float nextActionTime = 1.5f;
    private float period = 2.5f;


    public GameObject TargetForplayer;
    public GameObject TargetFireForPlayer;

    public RebuildingScript rsForHM;

    public GameObject fireLoc;

    public void Start()
    {
        player = truck;
    }

    // Update is called once per frame
    void Update()
    {

        if (Transformation.instance.GetIsTruck())
        {
            player = truck;
            player.GetComponent<HomingMissile>().target = this.TargetForplayer;
            player.GetComponent<HomingMissile>().targetFire = this.TargetFireForPlayer;
            player.GetComponent<HomingMissile>().rebuildscript = this.rsForHM;

        }
        else if (!Transformation.instance.GetIsTruck())
        {
            player = robot;
            player.GetComponent<HomingMissile>().target = this.TargetForplayer;
            player.GetComponent<HomingMissile>().targetFire = this.TargetFireForPlayer;
            player.GetComponent<HomingMissile>().rebuildscript = this.rsForHM;

        }

        if (Vector3.Distance(player.transform.position, transform.position) < 30)
        {
            myAnimationController.SetBool("FocusPlayer", true);
            transform.LookAt(player);

            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time+ period;
                Fire();
                
            }
           


        }
        else {


            if (Vector3.Distance(waypoints[current], transform.position) < AcceptableDistance)
            {
                myAnimationController.SetBool("FocusPlayer", false);
                current++;
                if (current >= waypoints.Length)
                {
                    current = 0;         
                }   
            }
            transform.LookAt(waypoints[current]);
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current], Time.deltaTime * speed);

        }

    }



    public void Fire() {
        GameObject rocket = Instantiate(rocketPrefab, fireLoc.transform.position, Quaternion.identity);
        StartCoroutine(launch(rocket));
    
    }

    public IEnumerator launch(GameObject rocket)
    {
        while (Vector3.Distance(player.transform.position, rocket.transform.position) > 1.0f)

        {
            rocket.transform.position += (player.transform.position - rocket.transform.position).normalized * missileSpeed * Time.deltaTime;
            rocket.transform.LookAt(player.transform);
            yield return null;

        }
        Instantiate(explosion, rocket.transform.position, rocket.transform.rotation);
        Destroy(rocket);
        Damage d = player.GetComponent<Damage>();
       d.singlePlayerDamage(damage);
       
        // Destroy(target);

    }


}
