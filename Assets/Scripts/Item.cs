using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")] //創建新的
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    public GameObject prefab;  // 物品對應的預製物
    public bool isPuzzlePiece; // 是否為拼圖類型物品
}