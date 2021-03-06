using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
  public int health;
  public int maxHealth;
  public float[] position;
  public int sceneIndex;
  public float totalTime;
  public int level;
  public int bulletNumber;
  public int lifePoint;
  public PlayerData(int health, int maxHealth, Vector2 position, int sceneIndex, float totalTime, MarioLevelEnum level, int bulletNumber, int lifePoint)
  {
    this.health = health;
    this.position = new float[2];
    this.position[0] = position.x;
    this.position[1] = position.y;
    this.sceneIndex = sceneIndex;
    this.totalTime = totalTime;
    this.level = (int)level;
    this.maxHealth = maxHealth;
    this.bulletNumber = bulletNumber;
    this.lifePoint = lifePoint;
  }
}
