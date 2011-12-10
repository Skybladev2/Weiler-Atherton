using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace TriangulationResearch
{
    enum Operation
    {
        Union,
        Intersect,
        Difference
    }

    struct Pair<T> : IEquatable<Pair<T>>
    {
        readonly T first;
        readonly T second;

        public Pair(T first, T second)
        {
            this.first = first;
            this.second = second;
        }

        public T First { get { return first; } }
        public T Second { get { return second; } }

        public override int GetHashCode()
        {
            return first.GetHashCode() ^ second.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Pair<T>)obj);
        }

        public bool Equals(Pair<T> other)
        {
            return other.first.Equals(first) && other.second.Equals(second) ||
                    other.first.Equals(second) && other.second.Equals(first);
        }
    }

    class WeilerAtherton
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject">Первый контур.</param>
        /// <param name="clip">Второй контур.</param>
        /// <param name="operation">Операция, проводимая над контурами.</param>
        /// <returns></returns>
        public static ICollection<CircularLinkedList<Vector2>> Process(CircularLinkedList<Vector2> subject,
                                                                        CircularLinkedList<Vector2> clip,
                                                                        Operation operation)
        {
            LinkedListNode<Vector2> curSubject = subject.First;
            Dictionary<Vector2, Pair<Vector2>> intersections = new Dictionary<Vector2, Pair<Vector2>>();

            while (curSubject != subject.Last)
            {
                LinkedListNode<Vector2> curClip = clip.First;
                while (curClip != clip.Last)
                {
                    Vector2 intersectionPoint;
                    if (IntersectSegment(curSubject.Value,
                                        curSubject.Next.Value,
                                        curClip.Value,
                                        curClip.Next.Value,
                                        out intersectionPoint))
                    {
                        subject.AddAfter(curSubject, intersectionPoint);
                        clip.AddAfter(curClip, intersectionPoint);
                        intersections.Add(intersectionPoint, new Pair<Vector2>(curClip.Value, curClip.Next.Value));
                    }
                    curClip = curClip.Next;
                }
                curSubject = curSubject.Next;
            }

            CircularLinkedList<Vector2> entering = new CircularLinkedList<Vector2>();
            CircularLinkedList<Vector2> exiting = new CircularLinkedList<Vector2>();
            List<CircularLinkedList<Vector2>> polygons = new List<CircularLinkedList<Vector2>>();
            MakeEnterExitList(subject, clip, intersections, entering, exiting);
            subject.RemoveLast();
            clip.RemoveLast();

            Traverse(subject, clip, entering, exiting, polygons, operation);

            if (polygons.Count == 0)
            {
                switch (operation)
                {
                    case Operation.Union:
                        polygons.Add(subject);
                        polygons.Add(clip);
                        break;
                    case Operation.Intersect:
                        break;
                    case Operation.Difference:
                        polygons.Add(subject);
                        break;
                    default:
                        break;
                }
            }

            return polygons;
        }

        public static void Swap<T>(ref T left, ref T right) where T : class
        {
            T temp;
            temp = left;
            left = right;
            right = temp;
        }

        private static void Traverse(CircularLinkedList<Vector2> subject,
                                        CircularLinkedList<Vector2> clip,
                                        CircularLinkedList<Vector2> entering,
                                        CircularLinkedList<Vector2> exiting,
                                        List<CircularLinkedList<Vector2>> polygons,
                                        Operation operation)
        {
            if (operation == Operation.Intersect)
                Swap<CircularLinkedList<Vector2>>(ref entering, ref exiting);

            if (operation == Operation.Difference)
                clip = clip.Reverse();

            CircularLinkedList<Vector2> currentList = subject;
            CircularLinkedList<Vector2> otherList = clip;

            while (entering.Count > 0)
            {
                CircularLinkedList<Vector2> polygon = new CircularLinkedList<Vector2>();
                Vector2 start = entering.First.Value;
                int count = 0;
                LinkedListNode<Vector2> transitionNode = entering.First;
                bool enteringCheck = true;

                while (transitionNode != null && (count == 0 || (count > 0 && start != transitionNode.Value)))
                {
                    transitionNode = TraverseList(currentList, entering, exiting, polygon, transitionNode, start, otherList);

                    enteringCheck = !enteringCheck;

                    if (currentList == subject)
                    {
                        currentList = clip;
                        otherList = subject;
                    }
                    else
                    {
                        currentList = subject;
                        otherList = clip;
                    }

                    count++;
                }

                polygons.Add(polygon);
            }
        }

        private static LinkedListNode<Vector2> TraverseList(CircularLinkedList<Vector2> contour,
                                                            CircularLinkedList<Vector2> entering,
                                                            CircularLinkedList<Vector2> exiting,
                                                            CircularLinkedList<Vector2> polygon,
                                                            LinkedListNode<Vector2> currentNode,
                                                            Vector2 startNode,
                                                            CircularLinkedList<Vector2> contour2)
        {
            LinkedListNode<Vector2> contourNode = contour.Find(currentNode.Value);
            if (contourNode == null)
                return null;

            entering.Remove(currentNode.Value);

            while (contourNode != null
                    &&
                        !entering.Contains(contourNode.Value)
                        &&
                        !exiting.Contains(contourNode.Value)
                    )
            {
                polygon.AddLast(contourNode.Value);
                contourNode = contour.NextOrFirst(contourNode);

                if (contourNode.Value == startNode)
                    return null;
            }

            entering.Remove(contourNode.Value);
            polygon.AddLast(contourNode.Value);

            return contour2.NextOrFirst(contour2.Find(contourNode.Value));
        }

        private static void MakeEnterExitList(CircularLinkedList<Vector2> subject,
                                                CircularLinkedList<Vector2> clip,
                                                Dictionary<Vector2, Pair<Vector2>> intersections,
                                                CircularLinkedList<Vector2> entering,
                                                CircularLinkedList<Vector2> exiting)
        {
            LinkedListNode<Vector2> curr = subject.First;
            while (curr != subject.Last)
            {
                if (intersections.ContainsKey(curr.Value))
                {
                    bool isEntering = Vector2Cross(curr.Next.Value.X - curr.Previous.Value.X,
                                                  curr.Next.Value.Y - curr.Previous.Value.Y,
                                                  intersections[curr.Value].Second.X - intersections[curr.Value].First.X,
                                                  intersections[curr.Value].Second.Y - intersections[curr.Value].First.Y) < 0;

                    if (isEntering)
                        entering.AddLast(curr.Value);
                    else
                        exiting.AddLast(curr.Value);
                }

                curr = curr.Next;
            }
        }

        private static float Vector2Cross(float x1, float y1, float x2, float y2)
        {
            return x1 * y2 - x2 * y1;
        }

        public static bool IntersectSegment(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out Vector2 intersection)
        {
            //addIntersection = true;
            Vector2 dir1 = end1 - start1;
            Vector2 dir2 = end2 - start2;

            //считаем уравнения прямых проходящих через отрезки
            float a1 = -dir1.Y;
            float b1 = +dir1.X;
            float d1 = -(a1 * start1.X + b1 * start1.Y);

            float a2 = -dir2.Y;
            float b2 = +dir2.X;
            float d2 = -(a2 * start2.X + b2 * start2.Y);

            //подставляем концы отрезков, для выяснения в каких полуплоскотях они
            float seg1_line2_start = a2 * start1.X + b2 * start1.Y + d2;
            float seg1_line2_end = a2 * end1.X + b2 * end1.Y + d2;

            float seg2_line1_start = a1 * start2.X + b1 * start2.Y + d1;
            float seg2_line1_end = a1 * end2.X + b1 * end2.Y + d1;

            //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
            if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
            {
                intersection = Vector2.Zero;
                return false;
            }

            float u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
            intersection = start1 + u * dir1;

            return true;
        }
    }
}
