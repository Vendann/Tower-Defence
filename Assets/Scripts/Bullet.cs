using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public float Speed, Damage; // скорость и урон снаряда

  void Start()
  {
    Destroy (gameObject, 2); // удалить об."Bullet" ч-з 2 сек. после создания, чтобы не летела бесконечно долго
  }
    
  void Update()
  {
    transform.Translate (Vector3.forward * Speed * Time.deltaTime);
	// перемещение об."Bullet" на вектора
	// "Vector3.forward" - направление перемещения вектора, "вперёд"
	// "Speed*Time.deltaTime" - величина перемещения вектора
  }
}
