using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthPoint : MonoBehaviour
{
  public Slider slider;
  public Vector2 Offset = Vector2.up;
  public int MaxHP = 100;
  [SerializeField] int _hp = 100;
  public int HP
  {
    get { return _hp; }
  }

  private void Start()
  {
    NotifyDataChanged();
  }

  private void Update()
  {
    // slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    slider.transform.position = Camera.main.WorldToScreenPoint((Vector2)transform.position + Offset);
  }

  void TakeDame(int dame)
  {
    if (dame > 0)
    {
      if (dame > HP)
      {
        Die();
      }
      else
      {
        _hp -= dame;
        NotifyDataChanged();
      }
    }
  }

  void Die()
  {
    Destroy(gameObject);
  }

  void NotifyDataChanged()
  {
    RefreshHPBar();
  }

  void RefreshHPBar()
  {
    slider.maxValue = MaxHP;
    slider.value = HP;

    slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, slider.normalizedValue);
  }
}