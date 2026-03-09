using System.Linq;
using UnityEngine;

public class FruitsMono : MonoBehaviour
{
    [SerializeField] private FactoryOfFruits factoryOfFruits;
    [SerializeField] private RelationFruit[] relationFruits;
    [SerializeField] private Map map;
    private TeaTime _spawn;
    private bool _allFruitsDead;
    public bool AllFruitAreDead => _allFruitsDead;

    public bool Finished { get; private set; }

    public void StartToSpawn()
    {
        Finished = false;
        _spawn.Play();
    }

    private void Start()
    {
        _spawn = this.tt().Pause().Loop(1, t =>
        {
            CalculateFruitsPercentage();
            foreach (var relationFruit in relationFruits)
            {
                //Debug.Log(relationFruit.fruitId + " " + relationFruit.quantity);
                for (var i = 0; i < relationFruit.quantity; i++)
                {
                    var randomPosition = map.GetRandomPositionToFruit();
                    if (randomPosition == -1)
                    {
                        Debug.LogWarning("No hay espacio disponible para más frutas");
                        continue;
                    }

                    var fruit = factoryOfFruits.SpawnFruit(relationFruit.fruitId,
                        map.GetPointToFruitByPosition(randomPosition));
                    fruit.OnFruitDie += FruitOnOnFruitDie;
                }
            }

            t.Break();
        }).Add(() => { Finished = true; });
    }

    private void FruitOnOnFruitDie()
    {
        //validate if all fruits are dead
        _allFruitsDead = map.GetAllFruits().All(pointToFruit => pointToFruit.GetFruit() != null && pointToFruit.GetFruit().AreDead);
    }

    private void CalculateFruitsPercentage()
    {
        var totalFruits = map.GetFruits();
        foreach (var relationFruit in relationFruits)
        {
            relationFruit.quantity = (int)(totalFruits * relationFruit.percentage) / 100;
        }
        
        // Distribuir el resto (por redondeo) solo en el ultimo tipo de fruta
        var rest = totalFruits - relationFruits.Sum(fruit => fruit.quantity);
        if (rest > 0 && relationFruits.Length > 0)
        {
            relationFruits[relationFruits.Length - 1].quantity += rest;
        }
    }
}