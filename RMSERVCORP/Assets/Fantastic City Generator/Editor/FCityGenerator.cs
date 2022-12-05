using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class FCityGenerator : EditorWindow
{

    private CityGenerator cityGenerator;

    private bool generateLightmapUVs = false;
    private bool intenseTraffic = false;


    [MenuItem("Window/Fantastic City Generator")]
    static void Init()
    {

        FCityGenerator window = (FCityGenerator)EditorWindow.GetWindow(typeof(FCityGenerator));

        window.Show();

    }



    public void LoadAssets()
    {

        string[] s;

        //BB - Street buildings in suburban areas (not in the corner)
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BB", "*.prefab");
        if (cityGenerator.BB.Length != s.Length)
            cityGenerator.BB = LoadAssets_sub(s);

        //BC - Down Town Buildings(Not in the corner)
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BC", "*.prefab");
        if (cityGenerator.BC.Length != s.Length)
            cityGenerator.BC = LoadAssets_sub(s);

        //BK - Buildings that occupy an entire block
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BK", "*.prefab");
        if (cityGenerator.BK.Length != s.Length)
            cityGenerator.BK = LoadAssets_sub(s);

        //BR - Residential buildings in suburban areas (not in the corner)
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BR", "*.prefab");
        if (cityGenerator.BR.Length != s.Length)
            cityGenerator.BR = LoadAssets_sub(s);

        //DC - Corner buildings that occupy both sides of the block
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/DC", "*.prefab");
        if (cityGenerator.DC.Length != s.Length)
            cityGenerator.DC = LoadAssets_sub(s);

        //EB - Corner buildings in suburban areas
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/EB", "*.prefab");
        if (cityGenerator.EB.Length != s.Length)
            cityGenerator.EB = LoadAssets_sub(s);

        //EC - Down Town Corner Buildings 
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/EC", "*.prefab");
        if (cityGenerator.EC.Length != s.Length)
            cityGenerator.EC = LoadAssets_sub(s);

        //MB - Buildings that occupy both sides of the block
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/MB", "*.prefab");
        if (cityGenerator.MB.Length != s.Length)
            cityGenerator.MB = LoadAssets_sub(s);

        //SB - Large buildings that occupy larger blocks
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/SB", "*.prefab");
        if (cityGenerator.SB.Length != s.Length)
            cityGenerator.SB = LoadAssets_sub(s);


    }



    private GameObject[] LoadAssets_sub(string[] s)
    {

        int i = s.Length;
        GameObject[] g = new GameObject[i];

        for (int h = 0; h < i; h++)
            g[h] = AssetDatabase.LoadAssetAtPath(s[h], typeof(GameObject)) as GameObject;

        return g;

    }



    private void GenerateCity(int size)
    {
        LoadAssets();

        if (size == 1)
            cityGenerator.GenerateStreetsVerySmall();
        else if (size == 2)
            cityGenerator.GenerateStreetsSmall();
        else if (size == 3)
            cityGenerator.GenerateStreets();
        else if (size == 4)
            cityGenerator.GenerateStreetsBig();

        DestroyImmediate(GameObject.Find("CarContainer"));

        InverseCarDirection(true);


    }


    void OnGUI()
    {

        GUILayout.Space(10);


        GUILayout.Label("Fantastic City Generator", EditorStyles.boldLabel);



        EditorGUILayout.BeginHorizontal();

        //cityGenerator = EditorGUILayout.ObjectField(cityGenerator, typeof(CityGenerator), true) as CityGenerator ;
        if (!cityGenerator)
            cityGenerator = AssetDatabase.LoadAssetAtPath("Assets/Fantastic City Generator/Generate.prefab", (typeof(CityGenerator))) as CityGenerator;

        LoadAssets();


        EditorGUILayout.EndHorizontal();

        

        GUILayout.Space(5);

        GUILayout.BeginVertical("box");


        GUILayout.Space(5);
        GUILayout.Label(new GUIContent("Generate Streets", "Make City"));

        GUILayout.Space(5);


        GUILayout.BeginHorizontal("box");

        if (GUILayout.Button("Small"))
            GenerateCity(1);


        if (GUILayout.Button("Medium"))
            GenerateCity(2);

        if (GUILayout.Button("Large"))
            GenerateCity(3);

        if (GUILayout.Button("Very Large"))
            GenerateCity(4);


        GUILayout.Space(5);


        GUILayout.EndHorizontal();



        GUILayout.EndVertical();

        GUILayout.Space(10);



        GUILayout.BeginVertical("box");

        GUILayout.Space(5);

        GUILayout.Label(new GUIContent("Buildings", "Make or Clear Buildings"));

        GUILayout.Space(5);
        
        GUILayout.BeginHorizontal("box");


        GUILayout.Space(5);

        if (GUILayout.Button("Generate Buildings"))
        {
            if (!GameObject.Find("Marcador")) return;
            cityGenerator.GenerateAllBuildings();
        }


        if (GUILayout.Button("Clear Buildings"))
        {
            if (!GameObject.Find("Marcador")) return;
            cityGenerator.DestroyBuildings();
        }



        GUILayout.EndHorizontal();



        GUILayout.EndVertical();




        GUILayout.Space(10);



        GUILayout.BeginVertical("box");

        GUILayout.Space(5);

        GUILayout.Label(new GUIContent("Traffic System", "Make or Clear Traffic System"));

        GUILayout.Space(5);


        GUILayout.BeginHorizontal("box");


        GUILayout.Space(5);

        if (GUILayout.Button("Add Traffic System"))
        {

            if (EditorApplication.isPlaying)
            {
                Debug.Log("Not allowed in play mode");
                return;
            }

            AddVehicles(intenseTraffic);
        }

        

        //intense traffic

        if (GUILayout.Button("Remove Traffic System"))
        {
            DestroyImmediate(GameObject.Find("CarContainer"));
        }


        

        GUILayout.EndHorizontal();

        intenseTraffic = GUILayout.Toggle(intenseTraffic, "Intense Traffic", GUILayout.Width(240));



        GameObject rm = GameObject.Find("Road-Mark-Rev");
        if (rm)
        {
            GUILayout.Space(10);

            if (GUILayout.Button("Inverse Car Direction"))
            {


                if (EditorApplication.isPlaying)
                    Debug.Log("Not allowed in play mode");
                else
                {
                    bool je = GameObject.Find("CarContainer");

                    if (je)
                    DestroyImmediate(GameObject.Find("CarContainer"));

                    InverseCarDirection(GameObject.Find("RoadMarkRev"));
                    if (je)
                        AddVehicles(intenseTraffic);
                }

            }
            GUILayout.Space(5);


        }

        GUILayout.EndVertical();


       
         
        GUILayout.Space(10);

         
            GUILayout.BeginVertical("box");


            if (GUILayout.Button("Combine Meshes"))
            {

                if (!GameObject.Find("Marcador")) return;

                float vertexCount = 0;
                float tt;
                GameObject module;
                GameObject[] my_Modules;



                my_Modules = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Marcador").ToArray();

                tt = my_Modules.Length;

                vertexCount = 0;

                for (int i = 0; i < tt; i++)
                {

                    //Debug.Log("i: " + i );

                    vertexCount = 0;


                    module = my_Modules[i];

                    GameObject newBlock = new GameObject("_block");
                    newBlock.transform.position = module.transform.position;
                    newBlock.transform.rotation = module.transform.rotation;
                    newBlock.transform.parent = module.transform.parent;


                    foreach (Transform child in module.transform)
                    {  // E1, E2, 100

                        Component[] temp = child.GetComponentsInChildren(typeof(MeshFilter));

                        foreach (MeshFilter currentChild in temp)
                        {

                            vertexCount += currentChild.sharedMesh.vertexCount;
                            if (vertexCount > 50000)
                            {
                                vertexCount = 0;
                                newBlock = new GameObject("_block");
                                newBlock.transform.position = module.transform.position;
                                newBlock.transform.rotation = module.transform.rotation;
                                newBlock.transform.parent = module.transform.parent;
                            }

                            if (currentChild.gameObject.name.Contains("(Clone)"))
                            {
                                currentChild.gameObject.transform.parent = newBlock.transform;
                            }


                        }


                    }

                    DestroyImmediate(my_Modules[i].gameObject);

                }



                GameObject[] myModules = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "_block").ToArray();


                tt = myModules.Length;



                for (int i = 0; i < tt; i++)
                {

                    float f = i / tt;

                    EditorUtility.DisplayProgressBar("Combining meshes", "Please wait", f);

                    module = myModules[i];

                    GameObject newObjects = new GameObject("Combined meshes");
                    newObjects.transform.parent = module.transform.parent;
                    newObjects.transform.localPosition = Vector3.zero;
                    newObjects.transform.localRotation = Quaternion.identity;


                    CombineMeshes(module.gameObject, newObjects, i);


                }

                EditorUtility.ClearProgressBar();


            }

            generateLightmapUVs = GUILayout.Toggle(generateLightmapUVs, "Generate Lightmap UVs", GUILayout.Width(240));

            GUILayout.EndVertical();
     


    }



    private void AddVehicles(bool additionalCars = false)
    {


        if (GameObject.Find("RoadMark") && GameObject.Find("RoadMarkRev"))
            InverseCarDirection(true);

        TrafficSystem trafficSystem = AssetDatabase.LoadAssetAtPath("Assets/Fantastic City Generator/Traffic System/Traffic System.prefab", (typeof(TrafficSystem))) as TrafficSystem;
        trafficSystem.LoadCars(additionalCars);
    }

    private void InverseCarDirection(bool actualside)
    {


        GameObject[] roadMark = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name.Equals("Road-Mark")).ToArray();
        for (int i = 0; i < roadMark.Length; i++)
            roadMark[i].transform.Find("RoadMark").gameObject.SetActive(actualside);
            
        roadMark = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name.Equals("Road-Mark-Rev")).ToArray();
        for (int i = 0; i < roadMark.Length; i++)
            roadMark[i].transform.Find("RoadMarkRev").gameObject.SetActive(!actualside);
            

    }


    private List<GameObject> newObjects = new List<GameObject>();


    public void CombineMeshes(GameObject objs, GameObject _Objects, int idx)
    {



        // Preserve Cloths
        Component[] temp = objs.GetComponentsInChildren(typeof(Cloth));
        foreach (Cloth currentChild in temp)
        {
            currentChild.gameObject.transform.parent = _Objects.transform;
            //currentChild.gameObject.isStatic = false;
        }


        //Preserve BoxCollider components
        temp = objs.GetComponentsInChildren(typeof(BoxCollider));
        foreach (BoxCollider currentChild in temp)
        {

            GameObject bc = new GameObject("BoxCollider");
            bc.transform.position = currentChild.transform.position;
            bc.transform.rotation = currentChild.transform.rotation;
            bc.transform.localScale = currentChild.transform.localScale;
            bc.transform.parent = _Objects.transform;

            UnityEditorInternal.ComponentUtility.CopyComponent(currentChild);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(bc);

        }

        //Preserve MeshCollider components
        temp = objs.GetComponentsInChildren(typeof(MeshCollider));
        foreach (MeshCollider currentChild in temp)
        {

            GameObject bc = new GameObject("MeshCollider");
            bc.transform.position = currentChild.transform.position;
            bc.transform.rotation = currentChild.transform.rotation;
            bc.transform.localScale = currentChild.transform.parent.localScale;

            bc.transform.parent = _Objects.transform;

            UnityEditorInternal.ComponentUtility.CopyComponent(currentChild);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(bc);

        }



        newObjects.Clear();

        Combine2(objs, _Objects, idx);

    }



    private void Combine2(GameObject _objs, GameObject _Objects, int idx)
    {



        GameObject oldGameObjects = _objs;

        Component[] filters = GetMeshFilters(_objs);

        Matrix4x4 myTransform = _objs.transform.worldToLocalMatrix;
        Hashtable materialToMesh = new Hashtable();

        for (int i = 0; i < filters.Length; i++)
        {


            MeshFilter filter = (MeshFilter)filters[i];
            Renderer curRenderer = filters[i].GetComponent<Renderer>();
            Mesh_CombineUtility.MeshInstance instance = new Mesh_CombineUtility.MeshInstance();
            instance.mesh = filter.sharedMesh;
            if (curRenderer != null && curRenderer.enabled && instance.mesh != null)
            {
                instance.transform = myTransform * filter.transform.localToWorldMatrix;

                Material[] materials = curRenderer.sharedMaterials;
                for (int m = 0; m < materials.Length; m++)
                {


                    instance.subMeshIndex = System.Math.Min(m, instance.mesh.subMeshCount - 1);

                    try
                    {
                        ArrayList objects = (ArrayList)materialToMesh[materials[m]];

                        if (objects != null)
                            objects.Add(instance);
                        else
                        {
                            objects = new ArrayList();
                            objects.Add(instance);
                            materialToMesh.Add(materials[m], objects);
                        }


                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message + "   Verify materials in " + curRenderer.name); 

                    }



                }
            }
        }



        foreach (DictionaryEntry mtm in materialToMesh)
        {
            ArrayList elements = (ArrayList)mtm.Value;

            Mesh_CombineUtility.MeshInstance[] instances = (Mesh_CombineUtility.MeshInstance[])elements.ToArray(typeof(Mesh_CombineUtility.MeshInstance));


            Material mat = (Material)mtm.Key;

            GameObject go = new GameObject(mat.name);

            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.transform.position = Vector3.zero;

            go.AddComponent(typeof(MeshFilter));
            go.AddComponent<MeshRenderer>();
            go.GetComponent<Renderer>().material = (Material)mtm.Key;


            MeshFilter filter = (MeshFilter)go.GetComponent(typeof(MeshFilter));
            filter.sharedMesh = Mesh_CombineUtility.Combine(instances, false);

            newObjects.Add(go);

        }

        if (newObjects.Count < 1)
        {
            return;
        }


        DestroyImmediate(oldGameObjects);


        if (newObjects.Count > 0)
        {
            for (int x = 0; x < newObjects.Count; x++)
            {


                newObjects[x].transform.parent = _Objects.transform;
                newObjects[x].transform.localPosition = Vector3.zero;
                newObjects[x].transform.localRotation = Quaternion.identity;

                // Generate Lightmap UVs ?
                if (generateLightmapUVs)
                {
                    Unwrapping.GenerateSecondaryUVSet(newObjects[x].GetComponent<MeshFilter>().sharedMesh);
                }



            }
        }





    }

    private Component[] GetMeshFilters(GameObject objs)
    {
        List<Component> filters = new List<Component>();
        Component[] temp = null;


        temp = objs.GetComponentsInChildren(typeof(MeshFilter));
        for (int y = 0; y < temp.Length; y++)
            filters.Add(temp[y]);




        return filters.ToArray();

    }






    public static List<T> LoadAllPrefabsOfType<T>(string path) where T : MonoBehaviour
    {
        if (path != "")
        {
            if (path.EndsWith("/"))
            {
                path = path.TrimEnd('/');
            }
        }

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");

        //loop through directory loading the game object and checking if it has the component you want
        List<T> prefabComponents = new List<T>();
        foreach (FileInfo fileInfo in fileInf)
        {
            string fullPath = fileInfo.FullName.Replace(@"\", "/");
            string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
            GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;

            if (prefab != null)
            {
                T hasT = prefab.GetComponent<T>();
                if (hasT != null)
                {
                    prefabComponents.Add(hasT);
                }
            }
        }
        return prefabComponents;
    }

}