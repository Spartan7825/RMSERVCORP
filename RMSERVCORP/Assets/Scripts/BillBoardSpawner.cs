using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardSpawner : MonoBehaviour
{

    
    public Terrain WorldTerrain;
    public LayerMask TerrainLayer;
    public static float TerrainLeft, TerrainRight, TerrainTop, TerrainBottom, TerrainWidth, TerrainLength, TerrainHeight;

    Vector3 citysize;

    public Collider cityCollider;
    public static float CityLeft, CityRight, CityTop, CityBottom, CityWidth, CityLength, CityHeight;

    public static ArrayList units = new ArrayList();
    public static ArrayList positions = new ArrayList();
    public static ArrayList rortations = new ArrayList();


    public GameObject BillBoard;

    public int ItemstoSpawn;

    public void Awake()
    {
        /*
        TerrainLeft = WorldTerrain.transform.position.x;
        TerrainBottom = WorldTerrain.transform.position.z;
        TerrainWidth = WorldTerrain.terrainData.size.x;
        TerrainLength = WorldTerrain.terrainData.size.z;
        TerrainHeight = WorldTerrain.terrainData.size.y;
        TerrainRight = TerrainLeft + TerrainWidth;
        TerrainTop = TerrainBottom + TerrainLength;


        */

        TerrainLeft = cityCollider.transform.position.x;
        TerrainBottom = cityCollider.transform.position.z;
        TerrainWidth = cityCollider.bounds.size.x;
        TerrainLength = cityCollider.bounds.size.z;
        TerrainHeight = cityCollider.bounds.size.y;
        TerrainRight = TerrainLeft + TerrainWidth/2;
        TerrainTop = TerrainBottom + TerrainLength/2;

        TerrainLeft = -TerrainRight;
        TerrainBottom = -TerrainTop;



        InstantiateRandomPosition(BillBoard, ItemstoSpawn, 0f);
    }


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateRandomPosition(GameObject Resource, int Amount, float AddedHeight)
    {
        var i = 0;
        float terrainHeight = 0f;
        RaycastHit hit;
        float randomPositionX, randomPositionY, randomPositionZ;
        Vector3 randomPosition = Vector3.zero;



        do
        {
            i++;
            randomPositionX = Random.Range(TerrainLeft, TerrainRight);
            randomPositionZ = Random.Range(TerrainBottom, TerrainTop);
        

            if (Physics.Raycast(new Vector3(randomPositionX, 9999f, randomPositionZ), Vector3.down, out hit, Mathf.Infinity, TerrainLayer))
            {
                terrainHeight = hit.point.y;
            }
            randomPositionY = terrainHeight + AddedHeight;
            randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

          

                Instantiate(Resource, randomPosition, Quaternion.identity);
           

               
           
        } while (i < Amount);




    }
}

