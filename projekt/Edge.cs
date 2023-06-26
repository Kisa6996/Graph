namespace ClassEdge;
public class Edge
{
    public char source;
    public char destination;
    public int weight;

    public Edge(char source, char destination, int weigh)
    {
        this.source = source;
        this.destination = destination;
        this.weight = weigh;
    }

}