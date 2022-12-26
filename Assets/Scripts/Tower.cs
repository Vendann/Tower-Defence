using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  public float Radius, FireDelay, Damage; // радиус обстрела, задержка выстрела, уровень повреждения (урон)
  public LayerMask EnemyLayer; // слой юнитов (для их поиска/обнаружения)
  public Transform BulletPrefab; // указатель/ссылка на префаб снаряда
  private float timeToFire; // время до выстрела
  public Transform gun, enemy, firePoint, EnemyPrefab; // ссылка на координаты объектов

  void Start()
  {
    timeToFire = FireDelay; // инициализируем время до выстрела (timeToFire) до максимума (FireDelay)
  }

  void Update()
  {
    if (timeToFire > 0) 
      timeToFire -= Time.deltaTime; // создаём таймер (постоянное уменьшение времени)
    else if (enemy) // если юнит находится в пределах/радиусе обстрела/выстрела
      Fire(); // делаем выстрел

    if (enemy) // если юнит в пределах/радиусе обстрела/выстрела
    {
      Vector3 lookAt = enemy.position; // сохраняем позицию юнита
      lookAt.y = gun.position.y; // заменяем координату "Y" юнита на координату "Y" орудия,
	  // чтобы не поворачивать орудие по оси "Y" (по высоте)
      gun.rotation = Quaternion.LookRotation (lookAt - gun.position); // поворачиваем орудие
	  //Vector3 dir = Vector3.RotateTowards(gun.transform.forward, lookAt - gun.position, Time.deltaTime * 5, 0);
	  //gun.rotation = Quaternion.LookRotation (dir); // плавный поворот орудия

      if (Vector3.Distance (transform.position, enemy.position) > Radius) // если юнит за пределами выстрела
	  // "Vector3.Distance" - вычислить расстояние между аргументами (башни и юнита)
	  // "transform.position" - координаты башни
	  // "enemy.position" - координаты юнита
        enemy = null; // обнуляем ссылку на юнит для текущей башни
    }
    else if (enemy == null) // если ссылки на юнита нет
    {
      Collider[] coll = Physics.OverlapSphere (transform.position, Radius, EnemyLayer);
	  // создаём сферу "Physics.OverlapSphere" для поиска внутри неё всех коллайдеров юнитов
	  // "transform.position" - центр сферы, "Radius" - радиус сферы, "EnemyLayer" юниты с коллайдерами на слое "Enemy"
	  // "coll" - массив найденных коллайдеров юнитов слоя "Enemy"

      if (coll.Length > 0) // если коллайдеры юнитов найдены
        enemy = coll[0].transform; // сохраняем координаты 1-го найденного юнита
    }
  }

  void Fire() // делаем выстрел
  {
    Transform bullet = Instantiate(BulletPrefab, firePoint.position, Quaternion.identity); // создаём снаряд
	
    bullet.LookAt(enemy.GetChild(0)); // снаряд "смотрит" на центр об."Capsule" юнита
    bullet.GetComponent<Bullet>().Damage = Damage; // записываем урон башни в урон снаряда (инициализируем переменную "Damage" скрипта "Bullet")

    timeToFire = FireDelay; // восстанавливаем время до выстрела
  }
}