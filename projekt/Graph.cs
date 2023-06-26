namespace ClassGraph;
using ClassArray;
using ClassEdge;

public class Graph
{
    // Класс ребра

    private Array[] list;

    private int n;

    public Graph(int n)
    {
        this.n = n;
        this.list = new Array[n];

    }
    public void AddVertex(char vertex)
    {
        bool flag = true;
        int i = 0;

        while ((i < n) && (flag))
        {
            // Нашли место вставки
            if (list[i] == null)
            {
                Array array = new Array(vertex);
                list[i] = array;
                flag = false;
            }
            else
            {
                // Такой элемент уже есть
                if (list[i].vertex == vertex)
                {
                    flag = false;
                }
            }
            i++;

            // Увеличиваем на массив
            if ((i == n) && (flag))
            {
                Array[] newList = new Array[n + 1];
                for (int j = 0; j < n; j++)
                {
                    newList[j] = list[j];
                }
                this.list = newList;
                this.n = n + 1;
            }
        }
        if (n == 0)
        {
            Array[] newList = new Array[1];
            Array array = new Array(vertex);
            newList[0] = array;
            this.list = newList;
            this.n = 1;
        }
    }

    public void RemoveVertex(char vertex)
    {
        int element = FindElement(vertex);
        if (element >= 0)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < list[element].edges.Count; i++)
            {
                stack.Push(list[element].edges[i].destination);
            }

            while (stack.Count != 0)
            {
                char value = stack.Pop();
                int number = FindElement(value);
                list[number].RemoveEdge(vertex);
            }

            Array[] newList = new Array[n - 1];
            int k = 0;
            for (int j = 0; j < n - 1; j++)
            {
                if (element == j)
                {
                    k = k + 1;
                }
                newList[j] = list[k];
                k++;
            }
            this.list = newList;
            this.n = n - 1;
        }
    }

    public int AddEdge(char source, char destination, int weight, bool flag = true)
    {
        int destIndex = FindElement(destination);
        int sourceIndex = FindElement(source);
        // Дуга уже есть

        if (destIndex >= 0 && sourceIndex >= 0)
        {
            if (list[sourceIndex].FindEdge(destination) >= 0)
            {
                return 0;
            }
        }
        if (source != destination)
        {
            if (flag)
            {
                // не существующая вершина
                if (destIndex < 0)
                {
                    AddVertex(destination);
                    destIndex = FindElement(destination);
                }
                list[destIndex].AddEdges(destination, source, weight);
            }


            // не существующая вершина
            if (sourceIndex < 0)
            {
                AddVertex(source);
                sourceIndex = FindElement(source);
            }
            list[sourceIndex].AddEdges(source, destination, weight);

            return 0;
        }

        // одинаковые вершины
        else
        {
            if (sourceIndex < 0)
            {
                AddVertex(source);
                sourceIndex = FindElement(source);
            }
            list[sourceIndex].AddEdges(source, destination, weight);

            return 0;
        }

    }

    public void RemoveEdge(char source, char destination)
    {
        int sourceIndex = FindElement(source);
        int destIndex = FindElement(destination);

        if (sourceIndex >= 0 && destIndex >= 0)
        {
            list[sourceIndex].RemoveEdge(destination);
            list[destIndex].RemoveEdge(source);
        }
    }
    public int FindElement(char vertex)
    {
        for (int i = 0; i < n; i++)
        {
            if (list[i] != null)
            {
                if (list[i].vertex == vertex)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public void Kruskal()
    {
        List<Edge> ostov = new List<Edge>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < list[i].edges.Count; j++)
            {
                if ((ostov.FindIndex(obj => (obj.source == list[i].edges[j].destination))) < 0)
                {
                    ostov.Add(list[i].edges[j]);
                }
            }
        }
        ostov.Sort((p1, p2) => p1.weight.CompareTo(p2.weight));
        Graph graph = new Graph(0);
        int k = 0;
        ostov.ForEach(element =>
{

    if (n != k)
    {
        graph.AddVertex(element.source);
        graph.AddEdge(element.source, element.destination, element.weight);
        k++;
        if (graph.IsCyclic())
        {
            k--;
            graph.RemoveEdge(element.source, element.destination);
        }
    }

});
        graph.Print();
    }
    public bool IsCyclic()
    {
        bool[] visited = new bool[this.n];

        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                if (IsCyclicUtil(i, visited, -1))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsCyclicUtil(int v, bool[] visited, int parent)
    {

        visited[v] = true;

        List<Edge> neighbors = this.list[v].edges;
        foreach (Edge neighbor in neighbors)
        {
            int n = this.FindElement(neighbor.destination);
            if (!visited[n])
            {
                if (IsCyclicUtil(n, visited, v))
                {
                    return true;
                }
            }
            else if (n != parent)
            {
                return true;
            }
        }

        return false;
    }
    public void Round()
    {
        if (n != 0)
        {
            Stack<char> stack = new Stack<char>();
            List<char> myList = new List<char>();

            int element = 0;
            char value = list[element].vertex;
            myList.Add(value);
            Console.Write(value);
            do
            {
                for (int i = 0; i < list[element].edges.Count; i++)
                {
                    stack.Push(list[element].edges[i].destination);
                }
                if (stack.Count() != 0)
                {
                    value = stack.Pop();
                }

                while (myList.Contains(value) && stack.Count() != 0)
                {
                    value = stack.Pop();
                }
                if (!myList.Contains(value))
                {
                    element = FindElement(value);
                    myList.Add(value);
                    Console.Write(value);
                }
            } while (stack.Count != 0);
            Console.WriteLine();
        }
    }
    public void Print()
    {
        for (int i = 0; i < list.Length; i++)
        {
            Console.Write(list[i].vertex);
            List<Edge> eges = list[i].edges;
            for (int j = 0; j < eges.Count; j++)
            {
                Console.Write("-");
                Console.Write(eges[j].destination);
                Console.Write(",");
                Console.Write(eges[j].weight);
            }
            Console.WriteLine("");
        }
        Console.WriteLine("");
    }

}
