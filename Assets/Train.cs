using Kutie;
using System.Collections.Generic;
using UnityEngine;

public class Train : SingletonMonoBehaviour<Train>
{
    [SerializeField] Car startCar;
    [SerializeField] GameObject dilemmaCarPrefab;
    [SerializeField] GameObject gameCarPrefab;
    [SerializeField] GameObject shopCarPrefab;
    [SerializeField] int minCars = 4;
    [SerializeField] int maxCars = 6;

    List<Car> cars = new();

    public Car CurrentCar { get; private set; } = null;

    void Reset()
    {
        cars = new(GetComponentsInChildren<Car>());
    }

    override protected void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        cars.Add(startCar);
        GenerateCars();
    }

    void GenerateCars()
    {
        int nCars = Random.Range(minCars, maxCars + 1);
        bool nextCanBeShop = false;
        for(int i = 0; i < nCars; i++)
        {
            if (nextCanBeShop)
            {
                if(Random.value > 0.5f)
                {
                    AddCar(gameCarPrefab);
                }
                else
                {
                    AddCar(shopCarPrefab);
                    nextCanBeShop = false;
                }
            }
            else
            {
                AddCar(gameCarPrefab);
                nextCanBeShop = true;
            }
            AddCar(dilemmaCarPrefab);
        }
    }

    void AddCar(GameObject prefab)
    {
        var car = Instantiate(prefab, transform);
        car.transform.position = cars[^1].EndPoint.position;
        cars.Add(car.GetComponent<Car>());
    }

    public void EnterCar(Car car)
    {
        if (CurrentCar)
        {
            CurrentCar.enabled = false;
        }
        CurrentCar = car;
        CurrentCar.enabled = true;
    }
}
