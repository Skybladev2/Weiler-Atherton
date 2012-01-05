using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TriangulationResearch
{
    static class Plotter
    {
        private static int cellSize = 20;
        private static Color gridColor = Color.FromArgb(192, 192, 192);
        private static int imageSideSize = 241;

        public static void Draw(PointF[] firstTriangle, PointF[] secondTriangle, string imageName, bool clip)
        {
            Bitmap bitmap = new Bitmap(imageSideSize, imageSideSize);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, imageSideSize, imageSideSize);

            DrawFineCells(graphics);
            DrawCoarseCells(graphics);
            DrawAxis(graphics);

            DrawTriangle(graphics, firstTriangle[0], firstTriangle[1], firstTriangle[2], Color.Black);
            DrawTriangle(graphics, secondTriangle[0], secondTriangle[1], secondTriangle[2], Color.Red);

            if (clip)
            {
                PointF[] newArray = new PointF[firstTriangle.Length + secondTriangle.Length];
                Array.Copy(firstTriangle, newArray, firstTriangle.Length);
                Array.Copy(secondTriangle, 0, newArray, firstTriangle.Length, secondTriangle.Length);

                bitmap = Clip(bitmap, newArray);
            }

            bitmap.Save(Path.Combine(Path.Combine(Path.Combine(Application.StartupPath, "..\\.."), "Docs"), imageName + ".png"), ImageFormat.Png);
        }

        private static void DrawTriangle(Graphics g, PointF a1, PointF b1, PointF c1, Color contourColor)
        {
            Brush b = new SolidBrush(Color.FromArgb(128, 232, 238, 247));
            Brush contourBrush = new SolidBrush(contourColor);
            Pen p = new Pen(contourBrush, 1);
            PointF[] points = new PointF[3];
            points[0] = new PointF(imageSideSize / 2 + a1.X * cellSize, imageSideSize / 2 - a1.Y * cellSize);
            points[1] = new PointF(imageSideSize / 2 + b1.X * cellSize, imageSideSize / 2 - b1.Y * cellSize);
            points[2] = new PointF(imageSideSize / 2 + c1.X * cellSize, imageSideSize / 2 - c1.Y * cellSize);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillPolygon(b, points);
            g.DrawPolygon(p, points);
        }

        private static void DrawAxis(Graphics graphics)
        {
            Brush b = new SolidBrush(Color.FromArgb(128, 0, 0, 0));
            Pen p = new Pen(b, 1);
            graphics.DrawLine(p, 0, imageSideSize / 2, imageSideSize, imageSideSize / 2);
            graphics.DrawLine(p, imageSideSize / 2, 0, imageSideSize / 2, imageSideSize);
        }

        private static void DrawFineCells(Graphics g)
        {
            Brush b = new SolidBrush(gridColor);
            Pen p = new Pen(b, 1);
            for (int i = 0; i <= imageSideSize / cellSize; i++)
            {
                for (int j = 0; j <= imageSideSize; j += 2)
                {
                    g.FillRectangle(b, j, i * cellSize, 1, 1);
                    g.FillRectangle(b, i * cellSize, j, 1, 1);
                }
            }
        }

        private static void DrawCoarseCells(Graphics g)
        {
            Brush b = new SolidBrush(gridColor);
            Brush eraseBrush = new SolidBrush(Color.White);
            Pen p = new Pen(b, 1);
            Pen erasePen = new Pen(eraseBrush, 1);

            for (int i = 0; i <= imageSideSize / cellSize; i++)
            {
                g.DrawLine(erasePen, 0, i * cellSize * 2, imageSideSize, i * cellSize * 2);
                g.DrawLine(erasePen, i * cellSize * 2, 0, i * cellSize * 2, imageSideSize);

                for (int j = 0; j <= imageSideSize; j += 4)
                {
                    g.DrawLine(p, j, i * cellSize * 2, j + 2, i * cellSize * 2);
                    g.DrawLine(p, i * cellSize * 2, j, i * cellSize * 2, j + 2);
                }
            }
        }

        private static Bitmap Clip(Bitmap bitmap, PointF[] points)
        {
            float minX = FindMinX(points);
            float maxX = FindMaxX(points);
            float minY = FindMinY(points);
            float maxY = FindMaxY(points);

            int left = (int)(imageSideSize / 2 + minX * cellSize - cellSize);
            int right = (int)(imageSideSize / 2 + maxX * cellSize + cellSize + 1);
            int top = (int)(imageSideSize / 2 - maxY * cellSize - cellSize);
            int down = (int)(imageSideSize / 2 - minY * cellSize + cellSize + 1);
            Rectangle rect = new Rectangle(left, top, right - left, down - top);

            Bitmap newBitmap = new Bitmap(right - left, down - top);
            Graphics g = Graphics.FromImage(newBitmap);

            g.DrawImage(bitmap, 0, 0, rect, GraphicsUnit.Pixel);
            return newBitmap;
        }

        private static float FindMinX(PointF[] points)
        {
            float result = points[0].X;
            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].X < result)
                    result = points[i].X;
            }

            return result;
        }

        private static float FindMinY(PointF[] points)
        {
            float result = points[0].Y;
            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].Y < result)
                    result = points[i].Y;
            }

            return result;
        }

        private static float FindMaxX(PointF[] points)
        {
            float result = points[0].X;
            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].X > result)
                    result = points[i].X;
            }

            return result;
        }

        private static float FindMaxY(PointF[] points)
        {
            float result = points[0].Y;
            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].Y > result)
                    result = points[i].Y;
            }

            return result;
        }
    }
}
