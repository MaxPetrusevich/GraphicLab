using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class GraphicTool : UserControl
    {
        private PointF[] points;
        private float scaleX = 1.0f;
        private float scaleY = 1.0f;
        private float offsetX = 0.0f;
        private float offsetY = 0.0f;

        public GraphicTool()
        {
            // Устанавливаем размеры контрола
            this.Size = new Size(400, 400);
            // Устанавливаем фон
            this.BackColor = Color.White;
        }

        public void PlotPoints(PointF[] points)
        {
            this.points = points;
            CalculateScaleAndOffset();
            this.Invalidate(); // Перерисовываем контрол
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            // Рисуем оси X и Y
            Pen axisPen = new Pen(Color.Black);
            g.DrawLine(axisPen, 0, Height / 2, Width, Height / 2); // Ось X
            g.DrawLine(axisPen, Width / 2, 0, Width / 2, Height); // Ось Y
            // Добавляем подписи к осям
            Font axisFont = new Font("Arial", 8);
            Brush axisBrush = Brushes.Black;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            g.DrawString("X", axisFont, axisBrush, Width - 10, Height / 2 - 10, format); // Подпись оси X
            g.DrawString("Y", axisFont, axisBrush, Width / 2 + 10, 0, format); // Подпись оси Y
            if (points != null && points.Length > 1)
            {
                // Рисуем линии, соединяющие точки
                Pen linePen = new Pen(Color.Red);
                for (int i = 0; i < points.Length - 1; i++)
                {
                    g.DrawLine(linePen, TranslatePoint(points[i]), TranslatePoint(points[i + 1]));
                }
                // Рисуем последнюю линию к точке
                g.DrawLine(linePen, TranslatePoint(points[points.Length - 1]), TranslatePoint(points[points.Length - 1]));
            }
        }

        private PointF TranslatePoint(PointF point)
        {
            // Переводим координаты в координаты на графике с учетом масштаба и смещения
            float x = point.X * scaleX + Width / 2 + offsetX;
            float y = -point.Y * scaleY + Height / 2 + offsetY;
            return new PointF(x, y);
        }

        private void CalculateScaleAndOffset()
        {
            if (points == null || points.Length == 0)
                return;

            // Находим минимальные и максимальные значения координат
            float minX = points[0].X;
            float maxX = points[0].X;
            float minY = points[0].Y;
            float maxY = points[0].Y;

            foreach (PointF point in points)
            {
                minX = Math.Min(minX, point.X);
                maxX = Math.Max(maxX, point.X);
                minY = Math.Min(minY, point.Y);
                maxY = Math.Max(maxY, point.Y);
            }

            // Рассчитываем масштаб и смещение
            scaleX = Width / (maxX - minX);
            scaleY = Height / (maxY - minY);
            offsetX = -minX * scaleX;
            offsetY = -minY * scaleY;
        }
    }
}
