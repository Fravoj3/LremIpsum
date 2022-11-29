using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Simple Camera Movement")]
    [Space]
    [Header("Bloky, po kter�ch se bude kamera pohybovat s hr��em horizont�ln�:")]
    public GameObject[] horizontalsObj;
    [Space]
    [Header("Bloky, po kter�ch se bude kamera pohybovat s hr��em vertik�ln�:")]
    public GameObject[] verticalsObj;

    [Header("Pozice speci�ln�ch pohyb� kamery")]
    public CameraPosition[] positions;
    [Header("Aktiva�n� pozice hr��e pro speci�ln� polohy")]
    public GameObject[] activationPositionsObj;

    public int offsetZ;
    public int offsetY;

    Point[] horizontals;
    Point[] verticals;
    Point[] activationPositions;

    bool active = false;

    void Start()
    {
        StartCoroutine(LateStart(3f));
        
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // Z�sk�n� Point objekty pozic
        horizontals = new Point[horizontalsObj.Length];
        verticals = new Point[verticalsObj.Length];
        activationPositions = new Point[activationPositionsObj.Length];

        for (int i = 0; i < horizontalsObj.Length; i++)
        {
            horizontals[i] = najdiPoint(horizontalsObj[i]);
        }

        for (int i = 0; i < verticalsObj.Length; i++)
        {
            verticals[i] = najdiPoint(verticalsObj[i]);
        }

        for (int i = 0; i < activationPositionsObj.Length; i++)
        {
            activationPositions[i] = najdiPoint(activationPositionsObj[i]);
        }

        if (positions.Length != activationPositionsObj.Length)
        {
            Debug.LogError("Chyba p�i zad�n� parametr� do CameraMovemet. Po�et CameraPosition a ActivationPositions nesed�!");
        }
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            if (standingOn(horizontals))
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, (GameObject.Find("Player").transform.position.z + offsetZ));
                Vector3 velocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.125f);
            }
            if (standingOn(verticals))
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, (GameObject.Find("Player").transform.position.y + offsetY), transform.position.z);
                Vector3 velocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.125f);
            }
            for (int i = 0; i < activationPositions.Length; i++)
            {
                if (Player.currentPlayerPoint == activationPositions[i])
                {
                    Vector3 velocity = Vector3.zero;
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(positions[i].x, positions[i].y, positions[i].z), ref velocity, 0.250f);
                    break;
                }
            }
        }
    }

    // Najdi point na z�klad� GameObject
    Point najdiPoint(GameObject obj)
    {
        Point point = null;
        for (int k = 0; k < Player.mapa.Count; k++)
        {
            if (((Point)Player.mapa[k]).button == obj)
            {
                point = (Point)Player.mapa[k];
                break;
            }
        }
        return point;
    }
    bool standingOn(Point[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if(Player.currentPlayerPoint == arr[i])
            {
                return true;
            }
        }
        return false;
    }
}
