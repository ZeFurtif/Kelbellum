using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{

    Collider m_Collider;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;

    [Header("Grass Prefab")]
    public GameObject grassPrefab;
    public Vector3 offset;
    public float density = 10;

    [Header("Grass Exceptions")]
    public Material exceptionsShader;
    private Material[] toMeshRenderer;
    private MeshRenderer mRenderer;
    [Range(0.0f, 1.0f)]
    public float rarity;

    // Start is called before the first frame update
    void Start()
    {

        toMeshRenderer = new Material[1];
        toMeshRenderer[0] = exceptionsShader;

        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider>();
        //Fetch the center of the Collider volume
        m_Center = m_Collider.bounds.center;
        //Fetch the size of the Collider volume
        m_Size = m_Collider.bounds.size;
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;
        //Output this data into the console

        OutputData();
        Populate();
    }

    void OutputData()
   {
        //Output to the console the center and size of the Collider volume
        Debug.Log("Collider Center : " + m_Center);
        Debug.Log("Collider Size : " + m_Size);
        Debug.Log("Collider bound Minimum : " + m_Min);
        Debug.Log("Collider bound Maximum : " + m_Max);
    }

    void Populate()
    {
        Vector3 currentPos = m_Min;
        for (int i = 0; i < density; i++)
        {
            for (int j = 0; j < density; j++)
            {
                Vector3 rOffset = offset + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f,1f));
                GameObject latest = Instantiate(grassPrefab, currentPos + rOffset, Quaternion.identity);
                latest.transform.SetParent(this.transform);

                if (UnityEngine.Random.Range(0f,1f) < rarity && exceptionsShader != null)
                {
                    mRenderer = latest.GetComponent<MeshRenderer>();
                    mRenderer.materials = toMeshRenderer;
                }

                currentPos.x += (m_Size.x / density);
            }
            currentPos.x = m_Min.x;
            currentPos.z += (m_Size.z / density);
        }
    }

}
