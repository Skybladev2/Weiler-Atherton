using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using System.Drawing;

namespace TriangulationResearch
{
    class Program
    {
        private static PointF[] firstTriangle = null;
        private static PointF[] secondTriangle = null;

        static void Main(string[] args)
        {
            
            //Vector3 a = new Vector3(0, 1, 0);
            //Vector3 b = new Vector3(1, 0, 0);
            //Console.WriteLine(Vector3.Cross(a, b));

            CircularLinkedList<Vector2> subject = new CircularLinkedList<Vector2>();
            CircularLinkedList<Vector2> clip = new CircularLinkedList<Vector2>();

            //#region Big and small square
            //subject.AddLast(new Vector2(0, 0));
            //subject.AddLast(new Vector2(0, 4));
            //subject.AddLast(new Vector2(4, 4));
            //subject.AddLast(new Vector2(4, 0));
            //subject.AddLast(new Vector2(0, 0));

            //clip.AddLast(new Vector2(4, 1));
            //clip.AddLast(new Vector2(4, 3));
            //clip.AddLast(new Vector2(6, 3));
            //clip.AddLast(new Vector2(6, 1));
            //clip.AddLast(new Vector2(4, 1));
            //#endregion

            //#region Shared edge
            //subject.AddLast(new Vector2(0, 0));
            //subject.AddLast(new Vector2(0, 1));
            //subject.AddLast(new Vector2(1, 0));
            //subject.AddLast(new Vector2(0, 0));

            //clip.AddLast(new Vector2(0, 1));
            //clip.AddLast(new Vector2(1, 1));
            //clip.AddLast(new Vector2(1, 0));
            //clip.AddLast(new Vector2(0, 1));
            //#endregion

            //#region Triangle and polygon
            //subject.AddLast(new Vector2(-3, 1));
            //subject.AddLast(new Vector2(2, 2));
            //subject.AddLast(new Vector2(2, -3));
            //subject.AddLast(new Vector2(-3, 1));

            //clip.AddLast(new Vector2(1, -1));
            //clip.AddLast(new Vector2(5, -1));
            //clip.AddLast(new Vector2(2, -5));
            //clip.AddLast(new Vector2(-6, 3));
            //clip.AddLast(new Vector2(1, 1));
            //clip.AddLast(new Vector2(3, 0));
            //clip.AddLast(new Vector2(1, -1));
            //#endregion

            //#region Simple triangles
            //subject.AddLast(new Vector2(-2, 0));
            //subject.AddLast(new Vector2(1, 1));
            //subject.AddLast(new Vector2(-1, -2));
            //subject.AddLast(new Vector2(-2, 0));

            //clip.AddLast(new Vector2(-1, -1));
            //clip.AddLast(new Vector2(1, 0));
            //clip.AddLast(new Vector2(1, -2));
            //clip.AddLast(new Vector2(-1, -1));
            //#endregion

            //#region Two equal
            //subject.AddLast(new Vector2(0, 0));
            //subject.AddLast(new Vector2(0, 1));
            //subject.AddLast(new Vector2(1, 0));
            //subject.AddLast(new Vector2(0, 0));

            //clip.AddLast(new Vector2(0, 0));
            //clip.AddLast(new Vector2(0, 1));
            //clip.AddLast(new Vector2(1, 0));
            //clip.AddLast(new Vector2(0, 0));
            //#endregion

            //#region Two triangles 1
            //subject.AddLast(new Vector2(-3, 0));
            //subject.AddLast(new Vector2(0, 3));
            //subject.AddLast(new Vector2(3, 0));
            //subject.AddLast(new Vector2(-3, 0));

            //clip.AddLast(new Vector2(-2, 1));
            //clip.AddLast(new Vector2(2, 1));
            //clip.AddLast(new Vector2(0, -1));
            //clip.AddLast(new Vector2(-2, 1));
            //#endregion

            //#region Two triangles 2
            //subject.AddLast(new Vector2(-3, 0));
            //subject.AddLast(new Vector2(0, 3));
            //subject.AddLast(new Vector2(3, 0));
            //subject.AddLast(new Vector2(-3, 0));

            //clip.AddLast(new Vector2(-2, 2));
            //clip.AddLast(new Vector2(2, 2));
            //clip.AddLast(new Vector2(0, 0));
            //clip.AddLast(new Vector2(-2, 2));
            //#endregion

            #region Second touches 2 sides from inside and exit through third side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 1));
            clip.AddLast(new Vector2(2, 1));
            clip.AddLast(new Vector2(0, -1));
            clip.AddLast(new Vector2(-2, 1));

            Draw(subject, clip, "Second touches 2 sides from inside and exit through third side");
            #endregion

            #region Second touches 1 side from inside and exit through other sides
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 2));
            clip.AddLast(new Vector2(2, 2));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-2, 2));

            Draw(subject, clip, "Second touches 1 side from inside and exit through other sides");
            #endregion

            #region Second touches 1 side from inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 1));
            clip.AddLast(new Vector2(1, 1));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-1, 1));

            Draw(subject, clip, "Second touches 1 side from inside");
            #endregion

            #region Second partially overlays 2 sides from same vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays 2 sides from same vertex");
            #endregion

            #region Second partially overlays 1 side from vertex and totally overlays other side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(1, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays 1 side from vertex and totally overlays other side");
            #endregion

            #region Second partially overlays 1 side and touches other side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 1));
            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(-1, 0));
            clip.AddLast(new Vector2(-2, 1));

            Draw(subject, clip, "Second partially overlays 1 side and touches other side");
            #endregion

            #region Second partially overlays 1 side and touches other vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(1, 0));
            clip.AddLast(new Vector2(-1, 0));

            Draw(subject, clip, "Second partially overlays 1 side and touches other vertex");
            #endregion

            #region Second partially overlays 1 side from inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 1));
            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(-1, 1));
            clip.AddLast(new Vector2(-2, 1));

            Draw(subject, clip, "Second partially overlays 1 side from inside");
            #endregion

            #region Second touches every side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(1, 2));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-1, 2));

            Draw(subject, clip, "Second touches every side");
            #endregion

            #region Second totally overlays 1 side from inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 2));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second totally overlays 1 side from inside");
            #endregion

            #region Second touches 2 sides from inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(1, 1));
            clip.AddLast(new Vector2(-1, 0));
            clip.AddLast(new Vector2(-1, 2));

            Draw(subject, clip, "Second touches 2 sides from inside");
            #endregion

            #region Second totally overlays 1 side from outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(0, -3));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second totally overlays 1 side from outside");
            #endregion

            #region Second partially overlays 1 side from vertex from outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-1, -2));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays 1 side from vertex from outside");
            #endregion

            #region Second partially overlays 1 side from outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 0));
            clip.AddLast(new Vector2(2, 0));
            clip.AddLast(new Vector2(0, -2));
            clip.AddLast(new Vector2(-2, 0));

            Draw(subject, clip, "Second partially overlays 1 side from outside");
            #endregion

            #region Second touches 1 side from outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, -2));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(1, -2));
            clip.AddLast(new Vector2(-1, -2));

            Draw(subject, clip, "Second touches 1 side from outside");
            #endregion

            #region Second completely inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-4, 0));
            subject.AddLast(new Vector2(0, 4));
            subject.AddLast(new Vector2(4, 0));
            subject.AddLast(new Vector2(-4, 0));

            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(1, 2));
            clip.AddLast(new Vector2(0, 1));
            clip.AddLast(new Vector2(-1, 2));

            Draw(subject, clip, "Second completely inside");
            #endregion

            #region Second touches 1 side and exits from another side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 1));
            clip.AddLast(new Vector2(-1, 3));
            clip.AddLast(new Vector2(-1, 0));
            clip.AddLast(new Vector2(-3, 1));

            Draw(subject, clip, "Second touches 1 side and exits from another side");
            #endregion

            #region Second touches 2 sides and exits from another side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 2));
            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(-1, 0));
            clip.AddLast(new Vector2(-3, 2));

            Draw(subject, clip, "Second touches 2 sides and exits from another side");
            #endregion

            #region Second touches 1 side and intersects same side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 2));
            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(-1, 1));
            clip.AddLast(new Vector2(-2, 2));

            Draw(subject, clip, "Second touches 1 side and intersects same side");
            #endregion

            #region Second touches 1 vertex from outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(4, 1));
            clip.AddLast(new Vector2(4, -1));
            clip.AddLast(new Vector2(3, 0));

            Draw(subject, clip, "Second touches 1 vertex from outside");
            #endregion

            #region Second touches 1 vertex from inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 1));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(1, 1));
            clip.AddLast(new Vector2(-1, 1));

            Draw(subject, clip, "Second touches 1 vertex from inside");
            #endregion

            #region Second partially overlays 1 side from vertex and exits from another vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 4));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays 1 side from vertex and exits from another vertex");
            #endregion

            #region Second touches 1 vertex from inside and intersects other vertex of the same side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 4));
            clip.AddLast(new Vector2(0, 2));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second touches 1 vertex from inside and intersects other vertex of the same side");
            #endregion

            #region Second touches 1 vertex from inside and intersects other vertex of the same side and intersects adjacent side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 4));
            clip.AddLast(new Vector2(0, -1));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second touches 1 vertex from inside and intersects other vertex of the same side and intersects adjacent side");
            #endregion

            #region Second touches 1 vertex from outside and shares 1 vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 1));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(3, 3));
            clip.AddLast(new Vector2(3, -1));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second touches 1 vertex from outside and shares 1 vertex");
            #endregion

            #region Second touches 1 vertex from inside and shares 1 vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 1));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-1, 1));

            Draw(subject, clip, "Second touches 1 vertex from inside and shares 1 vertex");
            #endregion

            #region Triangles are equal
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Triangles are equal");
            #endregion

            #region Second has 2 shared vertices and intersects 1 side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(-2, 2));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 2 shared vertices and intersects 1 side");
            #endregion

            #region Second touches 2 sides and last vertex outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 1));
            clip.AddLast(new Vector2(0, 4));
            clip.AddLast(new Vector2(2, 1));
            clip.AddLast(new Vector2(-2, 1));

            Draw(subject, clip, "Second touches 2 sides and last vertex outside");
            #endregion

            #region Second has 2 shared vertices and last vertex outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 2));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 2 shared vertices and last vertex outside");
            #endregion

            #region Second has 2 shared vertices and 1 side of first partially overlays second side from vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(1, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 2 shared vertices and 1 side of first partially overlays second side from vertex");
            #endregion

            #region Second partially overlays one side with shared vertex and stays inside forever
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 2));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays one side with shared vertex and stays inside forever");
            #endregion

            #region Second partially overlays one side with shared vertex and exits through side with shared vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(-2, 2));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays one side with shared vertex and exits through side with shared vertex");
            #endregion

            #region Second partially overlays one side with shared vertex and exits through side without shared vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 4));
            clip.AddLast(new Vector2(2, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays one side with shared vertex and exits through side without shared vertex");
            #endregion

            #region Second partially overlays one side with shared vertex and touches opposite side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(1, 2));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second partially overlays one side with shared vertex and touches opposite side");
            #endregion

            #region Second has 1 shared vertex, second vertex touches opposite side and sides intersect
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 3));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-2, 3));

            Draw(subject, clip, "Second has 1 shared vertex, second vertex touches opposite side and sides intersect");
            #endregion

            #region Second has 1 shared vertex, second vertex touches opposite side, third vertex outside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 1));
            subject.AddLast(new Vector2(2, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 2));
            clip.AddLast(new Vector2(1, 0.5f));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 1 shared vertex, second vertex touches opposite side, third vertex outside");
            #endregion

            #region Second has 1 shared vertex and opposite side intersects adjacent side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 2));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(-1, 1));
            clip.AddLast(new Vector2(-2, 2));

            Draw(subject, clip, "Second has 1 shared vertex and opposite side intersects adjacent side");
            #endregion

            #region Second has 1 shared vertex, opposite side intersects 2 sides, adjacent side intersects 1 side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(-2, 2));
            clip.AddLast(new Vector2(2, 2));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 1 shared vertex, opposite side intersects 2 sides, adjacent side intersects 1 side");
            #endregion

            #region Second has 1 shared vertex and intersects both adjacent sides
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 2));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(2, 2));
            clip.AddLast(new Vector2(-2, 2));

            Draw(subject, clip, "Second has 1 shared vertex and intersects both adjacent sides");
            #endregion

            #region Second has 1 shared vertex and intersects adjacent side and opposite side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 2));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(2, -1));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 1 shared vertex and intersects adjacent side and opposite side");
            #endregion

            #region Second has 1 shared vertex and lays completely inside
            subject.Clear();
            clip.Clear();

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            subject.AddLast(new Vector2(-1, 1));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(1, 1));
            subject.AddLast(new Vector2(-1, 1));

            Draw(subject, clip, "Second has 1 shared vertex and lays completely inside");
            #endregion

            #region Second has 1 shared vertex and intersects opposite side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 1));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(-1, 3));
            clip.AddLast(new Vector2(2, 1));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 1 shared vertex and intersects opposite side");
            #endregion

            #region Second has 1 shared vertex, second vertex lays on opposite side and adjacent side intersects opposite
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 2));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 0));
            clip.AddLast(new Vector2(0, 2));
            clip.AddLast(new Vector2(0, -1));
            clip.AddLast(new Vector2(-1, 0));

            Draw(subject, clip, "Second has 1 shared vertex, second vertex lays on opposite side and adjacent side intersects opposite");
            #endregion

            #region First partially overlays one side from inside
            subject.Clear();
            clip.Clear();

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            subject.AddLast(new Vector2(-2, 0));
            subject.AddLast(new Vector2(0, 2));
            subject.AddLast(new Vector2(2, 0));
            subject.AddLast(new Vector2(-2, 0));

            Draw(subject, clip, "First partially overlays one side from inside");
            #endregion

            #region First touches second from inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-1, 1));
            subject.AddLast(new Vector2(1, 1));
            subject.AddLast(new Vector2(0, 0));
            subject.AddLast(new Vector2(-1, 1));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(3, 0));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "First touches second from inside");
            #endregion

            #region Second has 1 shared vertex, opposite side touches one vertex and adjacent side intesects opposite side of the first
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 0));
            clip.AddLast(new Vector2(-2, 3));
            clip.AddLast(new Vector2(1, 3));
            clip.AddLast(new Vector2(-3, 0));

            Draw(subject, clip, "Second has 1 shared vertex, opposite side touches one vertex and adjacent side intesects opposite side of the first");
            #endregion

            #region Second partially overlays one side and intersects adjacent side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-2, 1));
            clip.AddLast(new Vector2(-1, 2));
            clip.AddLast(new Vector2(0, -1));
            clip.AddLast(new Vector2(-2, 1));

            Draw(subject, clip, "Second partially overlays one side and intersects adjacent side");
            #endregion

            #region Second partially overlays one side and intersects other sides
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 2));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-1, 0));
            clip.AddLast(new Vector2(0, 3));
            clip.AddLast(new Vector2(1, 0));
            clip.AddLast(new Vector2(-1, 0));

            Draw(subject, clip, "Second partially overlays one side and intersects other sides");
            #endregion

            #region Second has 1 vertex on side and intersects adjacent side
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-2, 0));
            subject.AddLast(new Vector2(0, 2));
            subject.AddLast(new Vector2(2, 0));
            subject.AddLast(new Vector2(-2, 0));

            clip.AddLast(new Vector2(-1, 1));
            clip.AddLast(new Vector2(-1, 3));
            clip.AddLast(new Vector2(1, 2));
            clip.AddLast(new Vector2(-1, 1));

            Draw(subject, clip, "Second has 1 vertex on side and intersects adjacent side");
            #endregion

            #region Second has 1 vertex on side and intersects adjacent side containing 2 vertices inside
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-2, 0));
            subject.AddLast(new Vector2(2, 1));
            subject.AddLast(new Vector2(2, 0));
            subject.AddLast(new Vector2(-2, 0));

            clip.AddLast(new Vector2(0, 0.5f));
            clip.AddLast(new Vector2(3, 2));
            clip.AddLast(new Vector2(3, -1));
            clip.AddLast(new Vector2(0, 0.5f));

            Draw(subject, clip, "Second has 1 vertex on side and intersects adjacent side containing 2 vertices inside");
            #endregion

            #region Second has 1 vertex on side and touches opposite vertex
            subject.Clear();
            clip.Clear();

            subject.AddLast(new Vector2(-3, 0));
            subject.AddLast(new Vector2(0, 3));
            subject.AddLast(new Vector2(3, 0));
            subject.AddLast(new Vector2(-3, 0));

            clip.AddLast(new Vector2(-3, 3));
            clip.AddLast(new Vector2(3, 3));
            clip.AddLast(new Vector2(0, 0));
            clip.AddLast(new Vector2(-3, 3));

            Draw(subject, clip, "Second has 1 vertex on side and touches opposite vertex");
            #endregion
            
            //ICollection<CircularLinkedList<Vector2>> polys = WeilerAtherton.Process(subject, clip, Operation.Difference);

            #region Triangulaion
            //int c = 1;
            //foreach (CircularLinkedList<Vector2> poly in polys)
            //{
            //    #region Old non-hole triangulate
            //    // Small test application demonstrating the usage of the triangulate
            //    // class.

            //    Console.WriteLine("Poly {0}", c++);
            //    // Create a pretty complicated little contour by pushing them onto
            //    // an stl vector.

            //    // allocate an STL vector to hold the answer.

            //    List<Vector2> result = new List<Vector2>();

            //    //  Invoke the triangulator to triangulate this polygon.
            //    if (!Triangulate.Process(new List<Vector2>(poly), result))
            //        Console.WriteLine("Triangulate failed!");

            //    // print out the results.
            //    int tcount = result.Count / 3;

            //    for (int i = 0; i < tcount; i++)
            //    {
            //        Vector2 p1 = result[i * 3 + 0];
            //        Vector2 p2 = result[i * 3 + 1];
            //        Vector2 p3 = result[i * 3 + 2];
            //        Console.WriteLine("Triangle {0} => ({1};{2}) ({3};{4}) ({5};{6})\n", i + 1, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y);
            //    }
            //    #endregion
            //}
            #endregion

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void Draw(CircularLinkedList<Vector2> subject, CircularLinkedList<Vector2> clip, string imageName)
        {
            firstTriangle = GetTrianglePointArray(subject);
            secondTriangle = GetTrianglePointArray(clip);
            Plotter.Draw(firstTriangle, secondTriangle, imageName, true);
        }

        private static PointF[] GetTrianglePointArray(CircularLinkedList<Vector2> list)
        {
            LinkedListNode<Vector2> currentNode = list.First;
            PointF[] result = new PointF[3];
            for (int i = 0; i < 3; i++)
            {
                result[i] = new PointF(currentNode.Value.X, currentNode.Value.Y);
                currentNode = list.NextOrFirst(currentNode);
            }

            return result;
        }
    }
}

