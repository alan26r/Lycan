using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotsX, slotsY;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    bool showInventory;
    bool showToolTip;
    string toolTip;
    bool draggingItem;
    Item draggedItem;
    int prevIndex;

    Item_DataBase dataBase;

    private void Start()
    {
        for(int i = 0; i < (slotsX * slotsY); i++)
        {
            inventory.Add(new Item());
        }

        dataBase = GameObject.FindGameObjectWithTag("Item dataBase").GetComponent<Item_DataBase>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

    private void OnGUI()
    {
        toolTip = "";

        GUI.skin = skin;

        if(GUI.Button(new Rect(40,400,100,40), "Save"))
        {
            SaveInventory();
        }
        if(GUI.Button(new Rect(40, 450, 100, 40), "Load"))
        {
            LoadInventory();
        }

        if(showInventory)
        {
            DrawInventory();

            if (showToolTip)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f,
                    Event.current.mousePosition.y, 200, 200), toolTip, skin.GetStyle("Tooltip"));
            }
        }

        if(draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x,
                    Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
        }
    }

    void DrawInventory()
    {
        Event currentEvent = Event.current;
  

        int i = 0;
        for (int y = 0; y < slotsY; y++)
        {

            for (int x = 0; x < slotsX; x++)  
            {
                Rect slotRect = new Rect(x * 60, y * 60, 50, 50);
                GUI.Box(new Rect(x * 110, y * 110, 100, 100), y.ToString(), skin.GetStyle("Slot"));
                slots[i] = inventory[i];
                Item item = slots[i];
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].itemIcon);

                    if(slotRect.Contains(currentEvent.mousePosition))
                    {
                        toolTip = CreateToolTip(slots[i]);
                        showToolTip = true;

                        if(currentEvent.button == 0 && currentEvent.type == EventType.MouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = item;
                            inventory[i] = new Item();
                        }

                        if(currentEvent.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }

                        if (currentEvent.isMouse && currentEvent.type == EventType.MouseDown && currentEvent.button == 1)
                        {
                            if(item.itemType == Item.ItemType.Consumable)
                            {
                                UseConsumable(slots[i],i,true);
                            }
                        }
                    }
                }
                else
                {
                    if(slotRect.Contains(currentEvent.mousePosition))
                    {
                        if(currentEvent.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                if(!draggingItem)
                {
                    showToolTip = true;
                }

                if(toolTip == "")
                {
                    showToolTip = false;
                }
                i++;
            }
        }
    }

    string CreateToolTip(Item item)
    {
        return toolTip = "<color=#1E86E0>" + item.itemName + "</color>\n\n" + "<color=#F2F2F2>" + item.itemDesc + "</ color >\n";
    }

    void RemoveItem(int id)
    {
        for(int i =0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch(item.itemID)
        {
            case 2:
                {
                    print("UseConsumable" + item.itemName);
                    break;
                }
            case 3:
                {
                    print("IncreaseStat");
                    break;
                }
        }
        if(deleteItem)
        {
            inventory[slot] = new Item();
        }
    }

    void AddItems(int id)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].itemName == null)
            {
                for(int j = 0; j < dataBase.items.Count; j++)
                {
                    if(dataBase.items[j].itemID == id)
                    {
                        inventory[i] = dataBase.items[j];
                    }
                }
                break;
            }
        }
    }

    bool InventoryContains(int id)
    {
        bool result = false;
        for(int i = 0; i < inventory.Count; i++)
        {
            result = inventory[i].itemID == id;
            if(result)
            {
                break;
            }
        }
        return result;
    }

    void SaveInventory()
    {
        for(int i =0; i <inventory.Count; i++)
        {
            PlayerPrefs.SetInt("Inventory " + i, inventory[i].itemID);
        }
        
    }

    void LoadInventory()
    {
        for(int i =0; i < inventory.Count; i++)
        {
            inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >=0 ? dataBase.items[PlayerPrefs.GetInt("Inventory " + 1)] : new Item();
        }
    }

}
