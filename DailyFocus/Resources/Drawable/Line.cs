using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.Resources.Drawable
{
    public class Line : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 2;
            canvas.DrawLine(0, 0, 0, 100);
        }
    }
}
