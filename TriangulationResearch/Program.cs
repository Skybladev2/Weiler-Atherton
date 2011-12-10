using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace TriangulationResearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector3 a = new Vector3(0, 1, 0);
            Vector3 b = new Vector3(1, 0, 0);
            Console.WriteLine(Vector3.Cross(a, b));

            CircularLinkedList<Vector2> subject = new CircularLinkedList<Vector2>();
            CircularLinkedList<Vector2> clip = new CircularLinkedList<Vector2>();

            #region Big and small square
            subject.AddLast(new Vector2(0, 0));
            subject.AddLast(new Vector2(0, 4));
            subject.AddLast(new Vector2(4, 4));
            subject.AddLast(new Vector2(4, 0));
            subject.AddLast(new Vector2(0, 0));

            clip.AddLast(new Vector2(4, 1));
            clip.AddLast(new Vector2(4, 3));
            clip.AddLast(new Vector2(6, 3));
            clip.AddLast(new Vector2(6, 1));
            clip.AddLast(new Vector2(4, 1));
            #endregion

            #region Shared edge
            subject.AddLast(new Vector2(0, 0));
            subject.AddLast(new Vector2(0, 1));
            subject.AddLast(new Vector2(1, 0));
            subject.AddLast(new Vector2(0, 0));

            clip.AddLast(new Vector2(0, 1));
            clip.AddLast(new Vector2(1, 1));
            clip.AddLast(new Vector2(1, 0));
            clip.AddLast(new Vector2(0, 1));
            #endregion

            #region Triangle and polygon
            subject.AddLast(new Vector2(-3, 1));
            subject.AddLast(new Vector2(2, 2));
            subject.AddLast(new Vector2(2, -3));
            subject.AddLast(new Vector2(-3, 1));

            clip.AddLast(new Vector2(1, -1));
            clip.AddLast(new Vector2(5, -1));
            clip.AddLast(new Vector2(2, -5));
            clip.AddLast(new Vector2(-6, 3));
            clip.AddLast(new Vector2(1, 1));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(1, -1));
            #endregion

            #region Simple triangles
            subject.AddLast(new Vector2(-2, 0));
            subject.AddLast(new Vector2(1, 1));
            subject.AddLast(new Vector2(-1, -2));
            subject.AddLast(new Vector2(-2, 0));

            clip.AddLast(new Vector2(-1, -1));
            clip.AddLast(new Vector2(1, 0));
            clip.AddLast(new Vector2(1, -2));
            clip.AddLast(new Vector2(-1, -1));
            #endregion

            ICollection<CircularLinkedList<Vector2>> polys = WeilerAtherton.Process(subject, clip, Operation.Difference);

            #region Old non-hole triangulate
            //// Small test application demonstrating the usage of the triangulate
            //// class.


            //// Create a pretty complicated little contour by pushing them onto
            //// an stl vector.

            //List<Vector2> a = new List<Vector2>();

            //a.Add(new Vector2(0, 6));
            //a.Add(new Vector2(0, 0));
            //a.Add(new Vector2(3, 0));
            //a.Add(new Vector2(4, 1));
            //a.Add(new Vector2(6, 1));
            //a.Add(new Vector2(8, 0));
            //a.Add(new Vector2(12, 0));
            //a.Add(new Vector2(13, 2));
            //a.Add(new Vector2(8, 2));
            //a.Add(new Vector2(8, 4));
            //a.Add(new Vector2(11, 4));
            //a.Add(new Vector2(11, 6));
            //a.Add(new Vector2(6, 6));
            //a.Add(new Vector2(4, 3));
            //a.Add(new Vector2(2, 6));

            //// allocate an STL vector to hold the answer.

            //List<Vector2> result = new List<Vector2>();

            ////  Invoke the triangulator to triangulate this polygon.
            //Triangulate.Process(a, result);

            //// print out the results.
            //int tcount = result.Count / 3;

            //for (int i = 0; i < tcount; i++)
            //{
            //    Vector2 p1 = result[i * 3 + 0];
            //    Vector2 p2 = result[i * 3 + 1];
            //    Vector2 p3 = result[i * 3 + 2];
            //    Console.WriteLine("Triangle {0} => ({1},{2}) ({3},{4}) ({5},{6})\n", i + 1, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y);
            //}
            #endregion

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        
    }
}
