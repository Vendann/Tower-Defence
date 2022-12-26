using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour // работа с юнитом
{
  public float Speed; // скорость движения юнита
  public float RotationSpeed; // скорость поворота юнита
  public Transform[] Points; // массив точек маршрута движения юнита
  private Transform currentPoint; // текущая точка движения юнита (куда движемся)
  private int index; // номер текущей точки движения юнита из массива "Points"
  public float MaxHP; // начальное здоровье
  private float HP; // текущее здоровье
  private ResourceManager rm; // ссылка на скрипт "ResourceManager"
  
  void Start()
  {
    index = 0; // начинаем с 0-й точки (1-я по порядку в массиве "Points")
    currentPoint = Points[index]; // текущая точка движения юнита - 0-й элемент массива "Points"
    HP = MaxHP; // устанавливаем юниту текущее здоровье максимальному значению (убрать, если используется "SetHP(float newHP)")
    rm = FindObjectOfType<ResourceManager>(); // создаём указатель/ссылку на скрипт "ResourceManager"
  }

  void Update()
  {
    Vector3 direct_pos = currentPoint.position - transform.position;
    // "direct_pos" - вектор/смещение/дельта перемещения юнита (куда и на сколько движемся)
	// "currentPoint.position" - позиция/координаты текущей точки движения из массива "Points" (куда движемся)
	// "transform.position"- позиция/координаты текущей позиции юнита (экземпляра прф."Enemy")
	// "transform.position" - обращение к свойству "Position" объекта, к кт. привязан данный скрипт
    Vector3 direct_rot = Vector3.RotateTowards(transform.forward, direct_pos, Time.deltaTime * RotationSpeed, 0);
	// ".RotateTowards" (поворот к)
	// "transform.forward" - поворот по синему вектору/направлению/оси
    transform.rotation = Quaternion.LookRotation(direct_rot); // поворот в сторону вектора "direct_pos"

    transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * Speed);
    // ".MoveTowards" (перейти к) - перемещение по прямой с заданным шагом
	// "transform.position" - текущая/начальная позиция
	// "currentPoint.position" - целевая/конечная позиция
	// "Time.deltaTime*Speed" - шаг перемещения
	// "Time.deltaTime" - время в секундах, которое потребовалось для отрисовки последнего кадра

    if (transform.position == currentPoint.position)
	// если достигли текущей точки движения юнита (куда движемся), т.е. достигли очередного угла маршрута движения
      {
        index++; // переходим к следующей точке движения юнита

        if (index == Points.Length) // если номер текущей точки движения юнита вышел за границы массива "Points"
          {
            Destroy (gameObject); // уничтожение игрового объекта
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          }
          else
          {
            currentPoint = Points[index]; // текущая точка движения юнита - "index"-й элемент массива "Points"
          }
      }
  }

  private void OnTriggerEnter (Collider other) // срабатывание триггера
  {
    if (other.CompareTag ("Bullet")) // если коллайдер юнита столкнулся с об."Bullet"
    {
      Destroy (other.gameObject); // удаляем объект, который столкнулся с юнитом (об."Bullet")

      HP -= other.GetComponent<Bullet>().Damage; // уменьшаем текущее здоровье юнита на величину урона об."Bullet"        

	  if (HP <= 0) //если текущее здоровье юнита <= 0
      {
        Destroy (gameObject); // уничтожаем юнит (экз.прф."Enemy")
        
		rm.EnemyKill(); // получаем золото за уничтожение юнита
      }
    }
  }

// реализация увеличения здоровья с новым юнитом (конфликт с "HP=MaxHP;" в "void Start()")
  public void SetHP (float newHP) // "принимаем" здоровье от скр."Spawner"
  {
    HP = newHP; // устанавливаем "новое" здоровье
  }
}