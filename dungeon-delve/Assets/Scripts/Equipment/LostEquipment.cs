using System.Collections.Generic;

public class LostEquipment
{
    private int encounter;
    private List<Equipment> eq = new List<Equipment>();
    private LostEquipment next;

    private static LostEquipment head;

    private LostEquipment(int encounterNumber, Equipment[] equipment)
    {
        encounter = encounterNumber;
        foreach (Equipment e in equipment)
        {
            eq.Add(e);
        }
    }

    public static void Insert(int encounterNumber, Equipment[] lostEq)
    {
        if (head == null)
        {
            head = new LostEquipment(encounterNumber, lostEq);
            return;
        }
        if (head.encounter < encounterNumber)
        {
            LostEquipment temp = new LostEquipment(encounterNumber, lostEq);
            temp.next = head;
            head = temp;
            return;
        }
        if (head.encounter == encounterNumber)
        {
            foreach (Equipment e in lostEq)
            {
                head.eq.Add(e);
            }
            return;
        }
        if(head.next == null)
        {
            head.next = new LostEquipment(encounterNumber, lostEq);
        }
        InsertLower(head, encounterNumber, lostEq);
    }

    private static void InsertLower(LostEquipment previousNode, 
        int encounterNumber, Equipment[] lostEq)
    {
        if (previousNode.next.encounter < encounterNumber)
        {
            LostEquipment temp = new LostEquipment(encounterNumber, lostEq);
            temp.next = previousNode.next;
            previousNode.next = temp;
            return;
        }
        if (previousNode.next.encounter == encounterNumber)
        {
            foreach (Equipment e in lostEq)
            {
                previousNode.next.eq.Add(e);
            }
            return;
        }
        if (previousNode.next.next == null)
        {
            previousNode.next.next = new LostEquipment(encounterNumber, lostEq);
        }
        InsertLower(previousNode.next, encounterNumber, lostEq);
    }

    public static Equipment[] GetLostEquipment(int encounterNumber)
    {
        if (head == null)
        {
            return null;
        }
        //because this is a sorted list, if the encounter number is greater than this encounter, it is not in the list
        if (head.encounter > encounterNumber)
        {
            return null;
        }
        if (head.encounter == encounterNumber)
        {
            Equipment[] equipment = head.eq.ToArray();
            head = head.next;
            return equipment;
        }
        return GetLostEquipment(head, encounterNumber);
    }

    public static bool IsAnyEquipment()
    {
        if(head == null)
        {
            return false;
        }
        return true;
    }

    private static Equipment[] GetLostEquipment(LostEquipment node, int encounterNumber)
    {
        if(node.next == null)
        {
            return null;
        }
        //because this is a sorted list, if the encounter number is greater than this encounter, it is not in the list
        if(node.next.encounter > encounterNumber)
        {
            return null;
        }
        if(node.next.encounter == encounterNumber)
        {
            Equipment[] equipment = node.eq.ToArray();
            node.next = node.next.next;
            return equipment;
        }
        return GetLostEquipment(node.next, encounterNumber);
    }
}
