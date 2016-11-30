using System;
using System.Collections.Generic;
using System.Windows;
using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Visualization;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;


namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	public class CircleAppearenceCalculationMethod : IWordAppearenceCalculationMethod
	{
		public IWordCloudAppearenceArguments Arguments { get; set; }
		public double MaxWeight { get; set; }
        public bool CanAddWords { get; private set; } = true;
        public int StopAfterWords { get; set; } = 10;
        public int Radius { get; set; } = 1;
        //public List<Polygon> Polygons { get; private set; }
        public List<Rect> Taken { get; private set; } = new List<Rect>();
        private double pos;

        public double Area(Rect r) { // (Gro�-Klein)^2
            return (r.Bottom - r.Top) * (r.Right - r.Left);
        }

        public VisualizedWord CalculateWordAppearence(IWeightedWord word, int itemIndex/*, VisualizedWord preDecessors*/ ) {
            //Ausdehnung
            var size = Arguments.WordSizeCalculator(word.Text, CalculateRelativeValue(Arguments.FontSizeRange, word.Weight));
            var s = new System.Windows.Size(size.Width, size.Height);

            //Startpunkt ermitteln
            var p = new Point();

            if (itemIndex == 0) { //vll auch (0,0), sp�ter!
                p.X = s.Width / 2;
                p.Y = s.Height / 2;
                pos = 0;
            } else {
                p = GetSpiralPoint(pos/*, radius = 7*/);
            }

            //mach' ein Rechteck d'raus
            var rectangle = new System.Windows.Rect() {
                Location = p,
                Size = s
            };

            //bis Platz gefunden
            var found = false;

            var offsetvector = new System.Windows.Vector(
                Arguments.PanelSize.Width  / 2,
                Arguments.PanelSize.Height  / 2
            );

            //Kollisionserkennung: �ber Vorg�nger iterieren
            //Teste Position (x,y) ist frei f�r Wort mit Gr��e size
            var intersect = new Rect();
            var tau = 2 * Math.PI;
            rectangle.Offset(offsetvector);
            while ( !(itemIndex <= 0 || found ) ) {
                foreach (Rect r in Taken) {
                    intersect = Rect.Intersect(rectangle, r);
                    if (intersect.IsEmpty) { // darf ich benutzen
                        found = true; // mit rumschleifen aufh�ren
                        break;
                    } else { // Platz belegt
                        // neue Postion anhand Fl�che bestimmen, min Schritte von 3,6�
                        var a = (int)(this.Area(intersect) % (Arguments.FontSizeRange.Max * 4));
                        pos += tau / (a + Radius )/ 2 ;
                        p = GetSpiralPoint(pos, 2);//this.Radius
                        var v = new Vector(p.X, p.Y);
                        // neuen Punkt finden
                        rectangle.Offset(v);
                    }
                }
            };
            //bzw. of Position (x,y) bis Position (x+size.Width,y+size.Height) frei/leer ist
            //wenn nicht, neue Position anhand Spirale a(r,phi) => a(x,y)

            //rechtecke f�r n�chsten Durchlauf speichern
            if (!rectangle.IsEmpty) {
                Taken.Add(rectangle);
            }

            var visualizedTag = new VisualizedWord(word)
            {
                //FontSize =  Range.GetRelativeValue(Arguments.FontSizeRange, word.Weight),
                FontSize =  (this.Arguments.FontSizeRange.Max * word.Weight) % 0.5,
                Position = new Position(rectangle.Top, rectangle.Left),
                Opacity = Arguments.OpacityRange.Min/Arguments.OpacityRange.Max*word.Weight//CalculateRelativeValue(Arguments.OpacityRange, word.Weight)
            };


            // raise flag to stop new Words
            if (StopAfterWords >= 1 && itemIndex >= (StopAfterWords - 1)) { 
				    CanAddWords = false;
            }

			return visualizedTag;
		}

        private double CalculateRelativeValue(Range range, double current) {
			return range.CalculateRelativeValue(new Range(0, MaxWeight), current);
		}

        private Point GetSpiralPoint(double position, double radius = 7)
        {
            var tau = 2 * Math.PI;
            double mult = position / tau * radius;
            double angle = position % tau;
            return new Point((int)(mult * Math.Sin(angle)), (int)(mult * Math.Cos(angle)));
        }

        private Point archimedeanSpiral(System.Windows.Size size) {
            //ohne lambda
            var e = size.Width / size.Height;
            var r = new Random();
            //var t = new Random() { return this.NextDouble() } < .5 ? 1 : -1;
            var t = r.NextDouble() < .5 ? 1 : -1;
            //t /= .1;
            return new System.Windows.Point(e * (t /= 10) * Math.Cos(t), t * Math.Sin(t));
            //mit lambda?
            //System.Windows.Point p = (t) => { e * (t *= .1) * Math.Cos(t), t* Math.Sin(t) };
            //oder
            //return (t) => { e * ( t *= .1 ) * Math.Cos( t ), t * Math.Sin( t ) };
            //return function(t) {
            //    return [e * (t *= .1) * Math.Cos(t), t * Math.Sin(t)];
            //};
        }
        //f = arch(s)
        //f(t)

        //private System.Windows.Point rectangularSpiral(System.Windows.Size size) {
        //    var dy = 4;
        //    var dx = dy * size.Width / size.Height;
        //    var x = 0;
        //    var y = 0;
        //    return function(t) {
        //        var sign = t < 0 ? -1 : 1;
        //        // See triangular numbers: T_n = n * (n + 1) / 2.
        //        switch ((Math.sqrt(1 + 4 * sign * t) - sign) & 3)
        //        {
        //            case 0: x += dx; break;
        //            case 1: y += dy; break;
        //            case 2: x -= dx; break;
        //            default: y -= dy; break;
        //        }
        //        return [x, y];
        //    };
        //}



    }
}