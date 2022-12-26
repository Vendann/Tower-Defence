using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
  public Material MainMaterial;	// исходный материал (материал по умолчанию)
  public Material Can1Material;	// материал, если можем строить (при наведении курсора на объект)
  public Material Cant1Material;	// материал, если не можем строить (при наведении курсора на объект)
  public Material Can2Material;
  public Material Cant2Material;
  public bool CanBuild;			// можем ли строить
  public GameObject[] TowerPrefab; // экземпляр башни
  public static int TowerID;
  private ResourceManager rm; // ссылка на скрипт "ResourceManager"
  private int cost;

  void Start()
  {
    rm = FindObjectOfType<ResourceManager>(); // создаём указатель/ссылку на скрипт "ResourceManager"
	TowerID = 0;
  }

  void Update()
  {
	if (Input.GetKeyDown("1"))
	  TowerID = 0;
    if (Input.GetKeyDown("2"))
	  TowerID = 1;
  }

  private void OnMouseOver() // курсор находится над объектом (курсор наведён на объект)
  {
	if (TowerID == 0)
	{
      if (CanBuild) // если строительство разрешено
      {
        GetComponent<Renderer>().material = Can1Material;
	    // "GetComponent<Renderer>.material" - ссылка на материал (свойство "Materials") кмп."Mesh Renderer"
      }
      else // если строительство не разрешено
      {
        GetComponent<Renderer>().material = Cant1Material;
      }
	}
	else if (TowerID == 1)
	{
	  if (CanBuild) // если строительство разрешено
      {
        GetComponent<Renderer>().material = Can2Material;
	    // "GetComponent<Renderer>.material" - ссылка на материал (свойство "Materials") кмп."Mesh Renderer"
      }
      else // если строительство не разрешено
      {
        GetComponent<Renderer>().material = Cant2Material;
      }
	}
  }

  private void OnMouseExit() // курсор покидает объект, выходит за его границы
  {
    GetComponent<Renderer>().material = MainMaterial; // возвратить объекту исходный материал
  }

  private void OnMouseUp() // отжата кнопка мыши над ячейкой игрового поля
  {
	if (TowerID == 0)
    {
      cost = rm.TowerCostOne;
    }
    if (TowerID == 1)
    {
      cost = rm.TowerCostTwo; // уменьшаем значение золота
    }
	
    if (CanBuild && rm.Gold >= cost) // если строительство разрешено и хватает золота для строительства
    {
      Tower tower = Instantiate (TowerPrefab[TowerID], transform.position, Quaternion.Euler (0, Random.Range(0, 360), 0)).GetComponent<Tower>();
	  // создаём объект "TowerPrefab" в центре текущей ячейки с координатами "transform.position"
	  // "Quaternion.Euler" - поворот со случайной координатой "Random.Range(0,360)" по оси "Y"
	  // ".GetComponent<Tower>()" - ссылка на экземпляр башни "TowerPrefab" (т.к. "TowerPrefab" типа "GameObject")
      
	  CanBuild = false; // запрещаем строительство текущей ячейки
	  
      rm.BuildTower(TowerID); // уменьшаем значение золота
    }
  }
}