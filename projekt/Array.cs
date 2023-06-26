namespace ClassArray;
using ClassEdge;
public class Array
{
    public char vertex;
    public List<Edge> edges;

    public Array(char vertex)
    {
        this.vertex = vertex;
        this.edges = new List<Edge>();
    }
    public void AddEdges(char source, char destination, int weight)
    {
        Edge edge = new Edge(source, destination, weight);
        edges.Add(edge);
    }
    public void RemoveEdge(char destination)
    {
        int index = FindEdge(destination);
        if (index >= 0)
        {
            edges.RemoveAt(index);
        }
    }
    public int FindEdge(char destination)
    {
        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i].destination == destination)
            {
                return i;
            }
        }
        return -1;
    }
}