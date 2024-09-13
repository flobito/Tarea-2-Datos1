using System;

public enum SortDirection
{
    Ascending,
    Descending
}

public class Node<T> where T : IComparable<T>
{
    public T Data;
    public Node<T> Next;
    public Node<T> Previous;

    public Node(T data)
    {
        this.Data = data;
        this.Next = null;
        this.Previous = null;
    }
}

public class DoublyLinkedList<T> where T : IComparable<T>
{
    public Node<T> Head;
    public Node<T> Tail;

    public DoublyLinkedList()
    {
        this.Head = null;
        this.Tail = null;
    }

    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (Head == null)
        {
            Head = Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Previous = Tail;
            Tail = newNode;
        }
    }

    public void PrintAscending()
    {
        Node<T> current = Head;
        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    public void PrintDescending()
    {
        Node<T> current = Tail;
        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Previous;
        }
        Console.WriteLine();
    }

    public void MergeSorted(DoublyLinkedList<T> listB, SortDirection direction)
    {
        if (direction != SortDirection.Ascending && direction != SortDirection.Descending)
        {
            throw new ArgumentException("Exceptipon.");
        }

        if (listB == null)
        {
            throw new ArgumentNullException(nameof(listB), "ListB no puede ser null.");
        }

        Node<T> currentA = this.Head;
        Node<T> currentB = listB.Head;
        DoublyLinkedList<T> mergedList = new DoublyLinkedList<T>();

        while (currentA != null && currentB != null)
        {
            if (direction == SortDirection.Ascending)
            {
                if (currentA.Data.CompareTo(currentB.Data) <= 0)
                {
                    mergedList.Add(currentA.Data);
                    currentA = currentA.Next;
                }
                else
                {
                    mergedList.Add(currentB.Data);
                    currentB = currentB.Next;
                }
            }
            else // Desc
            {
                if (currentA.Data.CompareTo(currentB.Data) >= 0)
                {
                    mergedList.Add(currentA.Data);
                    currentA = currentA.Next;
                }
                else
                {
                    mergedList.Add(currentB.Data);
                    currentB = currentB.Next;
                }
            }
        }

        
        while (currentA != null)
        {
            mergedList.Add(currentA.Data);
            currentA = currentA.Next;
        }
        while (currentB != null)
        {
            mergedList.Add(currentB.Data);
            currentB = currentB.Next;
        }

        
        this.Head = mergedList.Head;
        this.Tail = mergedList.Tail;

        
        if (direction == SortDirection.Descending)
        {
            Reverse();
        }
    }

    private void Reverse()
    {
        Node<T> current = Head;
        Node<T> prev = null;
        Node<T> temp = null;

        
        while (current != null)
        {
            temp = current.Next;
            current.Next = current.Previous;
            current.Previous = temp;
            prev = current;
            current = temp;
        }

        // Actualiza head y tail
        if (prev != null)
        {
            Head = prev;
            Tail = prev;
            while (Tail.Next != null)
            {
                Tail = Tail.Next;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        DoublyLinkedList<int> listA = new DoublyLinkedList<int>();
        DoublyLinkedList<int> listB = new DoublyLinkedList<int>();

        // Annade elementos a listA
        listA.Add(0);
        listA.Add(2);
        listA.Add(6);
        listA.Add(10);

        // Annade elementos a listB
        listB.Add(25);
        listB.Add(3);
        listB.Add(7);
        listB.Add(11);
        listB.Add(40);
        listB.Add(50);

        // En orden Asc
        try
        {
            listA.MergeSorted(listB, SortDirection.Ascending);
            listA.PrintAscending(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        // Resetea las listas en Desc
        listA = new DoublyLinkedList<int>();
        listB = new DoublyLinkedList<int>();

        //  Annade elementos a listA
        listA.Add(10);
        listA.Add(6);
        listA.Add(2);
        listA.Add(0);

        // Annade elementos a listB
        listB.Add(50);
        listB.Add(40);
        listB.Add(11);
        listB.Add(7);
        listB.Add(3);
        listB.Add(25);
      
        try
        {
            listA.MergeSorted(listB, SortDirection.Descending);
            listA.PrintDescending(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}