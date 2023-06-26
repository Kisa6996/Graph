using System;
using System.IO;
using ClassGraph;
class Arc
{
    public char vertex;
    public int price;
}
class Program
{
    static void Main()
    {
        //Путь
        string fileName = "input.txt";

        using (StreamReader reader = new StreamReader(fileName))
        {
            // Берём количество строк в файлк и создаём массив
            int n = Int32.Parse(reader.ReadLine());

            Graph graph = new Graph(n);

            for (int i = 0; i < n; i++)
            {
                string line = reader.ReadLine();

                var vertexsString = line.Split(new char[] { '-', ',' }).Where(x => Char.IsLetter(x[0])).ToArray();
                char[] vertexs = vertexsString.SelectMany(s => s.ToCharArray()).ToArray();
                int[] weights = line.Split(new char[] { ',', '-' }).Where(s => int.TryParse(s, out _))
                        .Select(int.Parse)
                        .ToArray();


                graph.AddVertex(vertexs[0]);

                for (int j = 1; j < vertexs.Length; j++)
                {
                    graph.AddEdge(vertexs[0], vertexs[j], weights[j - 1], false);
                }
            }
            // graph.RemoveVertex('A');
            // graph.RemoveVertex('B');
            // graph.RemoveVertex('C');
            // graph.RemoveVertex('D');
            // graph.AddVertex('D');
            graph.Print();
            graph.Kruskal();










            // graph.Round();
        }


    }
}