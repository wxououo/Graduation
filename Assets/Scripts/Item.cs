using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")] //�Ыطs��
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    public GameObject prefab;  // ���~�������w�s��
    public bool isPuzzlePiece; // �O�_�������������~
}