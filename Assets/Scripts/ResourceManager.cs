using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // добавить библиотеку "UI"

public class ResourceManager : MonoBehaviour
{
  public Text GoldTxt; // ссылка на переменную типа "Text"
  public int Gold, TowerCostOne, TowerCostTwo, EnemyCost; // количество золота, стоимость постройки башни, вознаграждение за убитого юнита

  void Start()
  {
	TowerCostOne = 35;
	TowerCostTwo = 50;
  }

  void Update()
  {
    GoldTxt.text = "Золото: " + Gold; // показываем текущее значение золота
  }

  public void BuildTower(int towerID) // построили башню
  {
	if (towerID == 0)
		Gold -= TowerCostOne;
	else if (towerID == 1)
		Gold -= TowerCostTwo;
  }

  public void EnemyKill() // убили юнита
  {
    Gold += EnemyCost;
  }
}