using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views;
using SkiaSharp.Views.Forms;

namespace ElearningClient.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HandWrtingPage : Frame
    {
        SKPoint currentFrom, currentTo;
        Queue<SKPoint> qFrom, qTo;
        public HandWrtingPage()
        {
            InitializeComponent();
            currentFrom = new SKPoint(0, 0);
            currentTo = new SKPoint(0, 0);
        }
        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            // we get the current surface from the event args
            var surface = e.Surface;
            // then we get the canvas that we can draw on
            var canvas = surface.Canvas;
            // clear the canvas / view
            //canvas.Clear(SKColors.White);

            // create the paint for the path
            var pathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Green,
                StrokeWidth = 5
            };

            // create a path
            var path = new SKPath();
            path.MoveTo(qFrom.Dequeue());
            path.LineTo(qTo.Dequeue());

            System.Diagnostics.Debug.WriteLine("Draw Path from({0}, {1}) to ({2}, {3})", currentFrom.X, currentFrom.Y, currentTo.X, currentTo.Y);
            // draw the path
            canvas.DrawPath(path, pathStroke);
        }

        public void SetFromPoint(SKPoint from)
        {
            currentFrom = from;
            currentTo = from;
        }
        public void SetToPoint(SKPoint to)
        {
            currentFrom = currentTo;
            currentTo = to;
            qFrom.Enqueue(currentFrom);
            qTo.Enqueue(currentTo);
            handWritingCanvasView.InvalidateSurface();
        }
    }
}
