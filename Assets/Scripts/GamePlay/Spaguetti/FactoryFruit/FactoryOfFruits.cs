using UnityEngine;

public class FactoryOfFruits : MonoBehaviour
{
    [SerializeField] private FruitsConfiguration toposConfiguration;
    private FruitsFactory _fruitFactory;

    private void Awake()
    {
        _fruitFactory = new FruitsFactory(Instantiate(toposConfiguration));
    }
    
    public Fruit SpawnFruit(string id, PointToFruit parent)
    {
        var fruit = _fruitFactory.Create(id);
        fruit.Configure(parent);
        parent.SetFruit(fruit);
        return fruit;
    }
}