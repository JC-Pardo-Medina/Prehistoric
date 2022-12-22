using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StegoMovement : MonoBehaviour
{
    private Vector3 waypointActual;
    private int wpElegido;

    public float velocidad;

    public List<GameObject> waypoints = new List<GameObject>();

    private ArrayList listaWaypoints = new ArrayList();

    void Start()
    {
        foreach (GameObject waypoint in waypoints)
        {
            listaWaypoints.Add(waypoint.transform.position);
            Destroy(waypoint);
        }

        waypointActual = (Vector3)listaWaypoints[0];
    }


    void Update()
    {
        comprobarDistancia();
        moveNPCTowards(waypointActual);

    }


    void comprobarDistancia()
    {
        if (Vector3.Distance(transform.position, waypointActual) < 2.0f)
            selectNewWaypoint();
    }

    void selectNewWaypoint()
    {
        if (++wpElegido == listaWaypoints.Count) wpElegido = 0;
        waypointActual = (Vector3)listaWaypoints[wpElegido];
    }

    void moveNPCTowards(Vector3 waypointActual)
    {

        Vector3 direccion = waypointActual - transform.position;

        Vector3 movimiento = direccion.normalized * velocidad * Time.deltaTime;

        transform.LookAt(waypointActual);
        transform.position = transform.position + movimiento;

    }
}
