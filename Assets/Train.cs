using Kutie;
using System.Collections.Generic;
using UnityEngine;

public class Train : SingletonMonoBehaviour<Train>
{
    [SerializeField] Car startCar;
    [SerializeField] GameObject dilemmaCarPrefab;
    [SerializeField] GameObject gameCarPrefab;
    [SerializeField] GameObject shopCarPrefab;
    [SerializeField] int carsPerGeneration = 5;
    [SerializeField] int minCars = 3;

    LinkedList<Car> cars = new();

    Car LastCar => cars.Count == 0 ? startCar : cars.Last.Value;
    Car SecondLastCar => cars.Count < 2 ? null : cars.Last.Previous.Value;

    int carIndex = -1;

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
        GenerateCars(carsPerGeneration);
    }

    void GenerateCars(int nCars)
    {
        for(int i = 0; i < nCars; i++)
        {
            if (SecondLastCar is GameCar)
            {
                if(Random.value > 0.5f)
                {
                    AddCar(gameCarPrefab);
                }
                else
                {
                    AddCar(shopCarPrefab);
                }
            }
            else
            {
                AddCar(gameCarPrefab);
            }
            AddCar(dilemmaCarPrefab);
        }
    }

    void AddCar(GameObject prefab)
    {
        var car = Instantiate(prefab, transform);
        car.transform.position = LastCar.EndPoint.position;
        cars.AddLast(car.GetComponent<Car>());
    }

    public void EnterCar(Car car)
    {
        if (CurrentCar)
        {
            CurrentCar.enabled = false;
        }
        CurrentCar = car;
        CurrentCar.enabled = true;

        carIndex++;

        // only start deleting cars after we clear
        //      the start car
        //      the first game car
        //      the first dilemma car
        // (as otherwise, we will see the car being deleted)
        if (carIndex >= 3)
        {
            Destroy(cars.First.Value.gameObject);
            cars.RemoveFirst();
            if(cars.Count < minCars)
            {
                GenerateCars(carsPerGeneration);
            }
        }
    }
}
