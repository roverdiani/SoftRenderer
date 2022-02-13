using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using SFML.System;

namespace SoftRenderer
{
    public class Model
    {
        private readonly List<Vector3f> _verts = new List<Vector3f>();
        private readonly List<List<int>> _faces = new List<List<int>>();

        public int NumVerts => _verts.Count;
        public int NumFaces => _faces.Count;

        public List<int> Face(int idx) => _faces[idx];
        public Vector3f Vert(int i) => _verts[i];
        
        public Model(string filename)
        {
            using StreamReader reader = new StreamReader(filename);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("v "))
                {
                    string[] values = line.Split(" ").Skip(1).ToArray();
                    Vector3f v = new Vector3f
                    {
                        X = float.Parse(values[0], CultureInfo.InvariantCulture),
                        Y = float.Parse(values[1], CultureInfo.InvariantCulture),
                        Z = float.Parse(values[2], CultureInfo.InvariantCulture)
                    };
                    
                    _verts.Add(v);
                }
                else if (line.StartsWith("f "))
                {
                    string[] values = line.Split(" ").Skip(1).ToArray();
                    List<int> vertexIndex = new List<int>();
                    foreach (string value in values)
                    {
                        string[] subValues = value.Split("/");
                        int idx = int.Parse(subValues[0]);
                        idx--;
                        vertexIndex.Add(idx);
                    }

                    _faces.Add(vertexIndex);
                }
            }
            
            Console.WriteLine($"# v# {_verts.Count} f# {_faces.Count}");
        }
    }
}