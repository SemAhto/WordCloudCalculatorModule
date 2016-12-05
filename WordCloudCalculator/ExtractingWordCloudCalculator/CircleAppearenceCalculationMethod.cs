using System;
using System.Collections.Generic;
using System.Windows;
using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;


namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	public class CircleAppearenceCalculationMethod : IWordAppearenceCalculationMethod
	{
		public IWordCloudAppearenceArguments Arguments { get; set; }
		public double MaxWeight { get; set; }
        public bool CanAddWords { get; private set; } = true;
        public int StopAfterWords { get; set; } = 300;
        public int Radius { get; set; } = 1;
        public int Sectors{ get; set; } = 64;
        //public List<Polygon> Polygons { get; private set; }
        public List<Rect> Taken { get; private set; } = new List<Rect>();
        private int Cnt { get; set; } = 0;
        private double pos;
        
        public double CalculateRelativeValue(Range Range, double Weight, double Step, double Multiplier = 1) {
            //= Convert.ToInt32((Range.Min / Range.Max * Weight % Range.Max + Range.Min) / Step) * Step,
            //var ratio = Range.CalculateRelativeValue(Range, Weight);
            var ratio = Range.Min / Range.Max * Weight /*/ (Range.Max - Range.Min)*/;
            var i = (ratio % (Range.Max - Range.Min) + Range.Min) ;
            var r = Convert.ToInt32(i / Step) * Step;
if(r > Range.Max)
            {
                ;
            }
            return r;
        }

        public double Area(Rect r) { // (Groß-Klein)^2
            if (r.IsEmpty) { return 0; }
            return (r.Bottom - r.Top) * (r.Right - r.Left);
        }
        public bool RectIntersectsRect(Rect r1, Rect r2) {
            var p1 = new Point(r1.Left, r1.Top);
            var p2 = new Point(r1.Right, r1.Top);
            var p3 = new Point(r1.Left, r1.Bottom);
            var p4 = new Point(r1.Right, r1.Bottom);
            if (PointIntersectsRect(p1, r2)) return true;
            if (PointIntersectsRect(p2, r2)) return true;
            if (PointIntersectsRect(p3, r2)) return true;
            if (PointIntersectsRect(p4, r2)) return true;
            return false;
        }
        public bool PointIntersectsRect(Point p, Rect r2)
        {
            if ( ( p.Y >= r2.Top && p.Y <= r2.Bottom) && (p.X <= r2.Right && p.X >= r2.Left) ) {
                return true;    
            }
            return false;
        }

        public VisualizedWord CalculateWordAppearence(IWeightedWord word, int itemIndex/*, VisualizedWord preDecessors*/ ) {
           
            //Ausdehnung
            var size = Arguments.WordSizeCalculator(word.Text, CalculateRelativeValue(Arguments.FontSizeRange, word.Weight));
            

                
            //Startpunkt ermitteln
            var p = new Point();

            if (itemIndex == 0) { //vll auch (0,0), später!
                p.X = 0;
                p.Y = 0;
                pos = 0;
            } else {
                p = GetSpiralPoint(pos/*, radius = 2*/);

            }


            //mach' ein Rechteck d'raus
            var rectangle = new System.Windows.Rect() {
                Location = p,
                Size = size
            };
            var rectgeom = rectangle;//geometrisch  

            //bis Platz gefunden
            var found = false;
            var nofound = false;

            var offsetvector = new System.Windows.Vector(
                Arguments.PanelSize.Width  / 2,
                Arguments.PanelSize.Height  / 2
            );

            //Kollisionserkennung: über Vorgänger iterieren
            //Teste Position (x,y) ist frei für Wort mit Größe size
            var intersect = new Rect();
            var tau = 2* Math.PI;
            var itemCnt = 0;
            
            rectgeom.Offset(offsetvector);
            while ( !((itemIndex <= 0) || found ) ) {
                nofound = false;
                foreach (Rect r in Taken) {
                    var f = this.RectIntersectsRect(rectangle, r);
                    if (f) { // darf ich benutzen, anliegende kanten werden als intersected bewertet
                        nofound = true;
                    }
                }   
                if ( nofound
                    || rectgeom.Left < 0 || rectgeom.Left >= Arguments.PanelSize.Width
                    || rectgeom.Top < 0 || rectgeom.Top >= Arguments.PanelSize.Height
                    || rectgeom.Right < 0 || rectgeom.Right >= Arguments.PanelSize.Width
                    || rectgeom.Bottom < 0 || rectgeom.Bottom >= Arguments.PanelSize.Height
                ) {
                    nofound = true;
                }
                // Platz belegt
                // neue Postion anhand Fläche bestimmen, min Schritte von 3,6°

                if (nofound) {
                    itemCnt++;
                    Cnt++;
                    var a = (this.Area(intersect) / this.Area(rectgeom));
                    pos += tau / Sectors + a;
                    //var a = 0.2 + (this.Area(intersect) / this.Area(rectgeom));
                    //pos += tau / Sectors * a;
                    p = GetSpiralPoint(pos);//this.Radius
                    //p.X -= (Arguments.WordMargin.Left );
                    //p.Y -= (Arguments.WordMargin.Top );
                    //rectangle.Width += Arguments.WordMargin.Left + Arguments.WordMargin.Right;
                    //rectangle.Height += Arguments.WordMargin.Top + Arguments.WordMargin.Bottom;
                    rectangle.Location=p;
                    rectgeom = rectangle;
                    rectgeom.Offset(offsetvector);
                    //nofound = false;
                } else {
                    found = true;
                    itemCnt = 0;
                    if (itemIndex > 44) {
                        ;
                    }
                }
                if (itemCnt > Sectors * 2) {
                    break;
                }
            }
                     
                //bzw. of Position (x,y) bis Position (x+size.Width,y+size.Height) frei/leer ist
                //wenn nicht, neue Position anhand Spirale a(r,phi) => a(x,y)

            //var durch = 0;
            //foreach (Rect r in Taken)
            //{
            //    Console.Write(durch);
            //    Console.Write(":");
            //    Console.Write(r.TopLeft);
            //    Console.Write("-");
            //    Console.Write(r.TopRight);
            //    Console.Write("\r\n");
            //    durch++;
            //};

            // raise flag to stop new Words
            if (
                (StopAfterWords >= 1 && itemIndex >= (StopAfterWords - 1))
                || !this.RectIntersectsRect(rectgeom, new Rect(new Point(0, 0), new System.Windows.Size(Arguments.PanelSize.Width, Arguments.PanelSize.Height)))
            ) {
                CanAddWords = false;
            }

            //rechtecke für nächsten Durchlauf speichern
            if (!rectangle.IsEmpty || CanAddWords) {
                Taken.Add(rectangle);

                var vergl = new Range(Arguments.FontSizeRange.Min, Arguments.FontSizeRange.Max/*, 0.5*/).CalculateRelativeValue(Arguments.FontSizeRange, word.Weight);
                return new VisualizedWord(word) {
                    Size = new Size(rectangle.Width, rectangle.Height),
                    //FontSize = Convert.ToInt32((Arguments.FontSizeRange.Min / Arguments.FontSizeRange.Max * word.Weight % Arguments.FontSizeRange.Max + Arguments.FontSizeRange.Min) / 0.5) * 0.5,
                    //FontSize = Arguments.FontSizeRange.CalculateRelativeValue(Arguments.FontSizeRange, word.Weight/*, 0.5, 1*/),
                    FontSize = CalculateRelativeValue(Arguments.FontSizeRange, word.Weight, 0.5, 1),
                    Position = new Point(rectangle.Top, rectangle.Left),
                    //Opacity = (Arguments.OpacityRange.Min / Arguments.OpacityRange.Max * word.Weight % 150 + Arguments.OpacityRange.Min) / 150 //CalculateRelativeValue(Arguments.OpacityRange, word.Weight, double Step)
                    Opacity = this.CalculateRelativeValue(Arguments.OpacityRange, word.Weight, 0.1, 1)
                    //Opacity = Arguments.OpacityRange.CalculateRelativeValue(Arguments.OpacityRange, word.Weight/*, 0.1, 1*/)
                }
                ;
            } else { return new VisualizedWord(word); }

		}

        private double CalculateRelativeValue(Range range, double current) {
			return range.CalculateRelativeValue(new Range(0, MaxWeight), current);
		}

        private Point GetSpiralPoint(double position, double radius=2)
        {
            var tau = 2 * Math.PI;
             
            double mult = position / tau * radius;
            double angle = position % tau;

            //Console.Write((int)(mult * Math.Sin(angle)));
            //Console.Write((int)(mult * Math.Cos(angle)));
            //Console.Write(" /-/ ");

            return new Point((int)(mult * Math.Sin(angle)), (int)(mult * Math.Cos(angle)));
        }
/* d3-cloud inspired
 * s.a. https://github.com/jasondavies/d3-cloud/blob/master/index.js
 */
        private Point archimedeanSpiral(System.Windows.Size size) {
            //ohne lambda
            var e = size.Width / size.Height;
            var r = new Random();
            //var t = new Random() { return this.NextDouble() } < .5 ? 1 : -1;
            var t = r.NextDouble() < .5 ? 1 : -1;
            //Console.Write("e="); Console.Write(e); Console.Write("//");
            //t /= .1;
            return new System.Windows.Point(e * (t /= 10) * Math.Cos(t), t * Math.Sin(t));
            //mit lambda?
            //var p = new Point() => { e * (t *= .1) * System.Math.Cos(t), t* System.Math.Sin(t) };
            //oder
            //return (t) => { e * ( t *= .1 ) * Math.cos(t), t * Math.sin(t) };
            //return function(t) {
            //    return [e * (t *= .1) * Math.Cos(t), t * Math.Sin(t)];
            //};
        }
        //f = afrch(s)
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
/* /d3-cloud inspired */


    }
}