using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour, IMap
{
    [SerializeField] private List<PointToTopo> pointToTopos;
    [SerializeField] private List<PointToFruit> pointToFruits;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IMap>(this);
        pointToTopos = new List<PointToTopo>();
        pointToFruits = new List<PointToFruit>();
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IMap>();
    }

    public PointToTopo GetPointToTopoByPosition(int position)
    {
        if (position >= 0 && position < pointToTopos.Count)
        {
            return pointToTopos[position];
        }
        throw new Exception("Position out of range for topos");
    }
    public PointToFruit GetPointToFruitByPosition(int position)
    {
        // Prioriza el indice de la lista; mantiene compatibilidad con datos que usen Position.
        if (position >= 0 && position < pointToFruits.Count)
        {
            return pointToFruits[position];
        }
        throw new Exception("Position out of range for fruits");
    }

    public int GetRandomPositionToTopo()
    {
        var listOfFreeTopos = pointToTopos.Where(point => point.IsFree).ToList();
        if (listOfFreeTopos.Count == 0)
        {
            Debug.LogWarning("No hay posiciones libres para topos");
            return -1;
        }

        var position = Random.Range(0, listOfFreeTopos.Count);
        return position;
    }

    public void SaveTopo(PointToTopo topo)
    {
        pointToTopos.Add(topo);
    }

    public void SaveFruit(PointToFruit pointToFruit)
    {
        pointToFruits.Add(pointToFruit);
    }

    public int GetRandomPositionToFruit()
    {
        // Obtener todas las posiciones libres
        var availablePositions = new List<int>();
        for (int i = 0; i < pointToFruits.Count; i++)
        {
            if (!pointToFruits[i].HasFruit)
            {
                availablePositions.Add(i);
            }
        }

        // Validar que hay espacio disponible
        if (availablePositions.Count == 0)
        {
            Debug.LogError("No more space for fruits");
            return -1;
        }

        // Elegir una posición aleatoria de las disponibles
        var randomIndex = Random.Range(0, availablePositions.Count);
        var position = availablePositions[randomIndex];
        pointToFruits[position].HasFruit = true;
        return position;
    }

    public int GetFruits()
    {
        return pointToFruits.Count;
    }

    public List<PointToFruit> GetAllFruits()
    {
        return pointToFruits;
    }
}