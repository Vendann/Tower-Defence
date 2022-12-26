using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public Enemy EnemyPrefab, LilEnemyPrefab;
  public float TimeToSpawn; // частота (промежуток времени) создания новых юнитов
  public Transform[] Points; // массив точек движения юнита
  public Transform[] AltPoints;
  private float timer;
  public float MainHP, IncreaseHP; // основное и дельта здоровья для следующих юнитов
  public float percentSpawn;
  public float percentWay;

  void Start()
  {
    timer = TimeToSpawn; // время создания 1-го юнита
  }
    
  void Update()
  {
    timer -= Time.deltaTime; // из "timer" вычитаем время рендеринга/отрисовки

    if (timer <= 0)
    {
	  if (Random.value > percentWay)
	  {
		if (Random.value > percentSpawn)
	    {
		  Enemy enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
		  enemy.Points = Points;
		  timer = TimeToSpawn;
		  enemy.SetHP (MainHP);
		  MainHP += IncreaseHP;
	    }
	  
	    else
	    {
		  Enemy enemy = Instantiate(LilEnemyPrefab, transform.position, Quaternion.identity);
		  enemy.Points = Points;
		  timer = TimeToSpawn;
		  enemy.SetHP (MainHP);
		  MainHP += IncreaseHP;
	    }
	  }
	  
	  else
	  {
		if (Random.value > percentSpawn)
	    {
		  Enemy enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
		  enemy.Points = AltPoints;
		  timer = TimeToSpawn;
		  enemy.SetHP (MainHP);
		  MainHP += IncreaseHP;
	    }
	  
	    else
	    {
		  Enemy enemy = Instantiate(LilEnemyPrefab, transform.position, Quaternion.identity);
		  enemy.Points = AltPoints;
		  timer = TimeToSpawn;
		  enemy.SetHP (MainHP);
		  MainHP += IncreaseHP;
	    }
	  }
	  
	
	  
	  // создаём объект (новый юнит)
	  // "EnemyPrefab" - что создаём/клонируем (исходный объект)
	  // "transform.position" - где создаём, в какой позиции (координаты об."Spawner")
	  // "Quaternion.identity" - как повёрнут, какой поворот/разворот кватерниона (поворот прф."Enemy")
       // передаём созданному объекту (новому юниту) массив точек маршрута движения

       // восстанавливаем время создания новых юнитов

      // реализация увеличения здоровья с новым юнитом
       // передаём "новое" здоровье экз.прф."Enemy"
       // увеличиваем здоровье для каждого нового юнита
    }
  }
}