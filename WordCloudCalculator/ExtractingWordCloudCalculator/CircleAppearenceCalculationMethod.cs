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
        //public List<Polygon> Polygons { get; private set; }
        public List<Rect> Taken { get; private set; }

        public double Area(Rect r) {
            return (r.BottomRight - r.BottomLeft) * (r.BottomRight - r.TopRight);
        }


        private Point GetSpiralPoint(double position, double radius = 7) {
            var tau = 2 * Math.PI;
            double mult = position / tau * radius;
            double angle = position % tau;
            return new Point((int)(mult * Math.Sin(angle)), (int)(mult * Math.Cos(angle)));
        }


        public VisualizedWord CalculateWordAppearence( IWeightedWord word, int itemIndex/*, VisualizedWord preDecessors*/ ) {
            //Ausdehnung
            var size = Arguments.WordSizeCalculator(word.Text, CalculateRelativeValue(Arguments.FontSizeRange, word.Weight));
            var s = new System.Windows.Size(size.Width, size.Height);

            //Startpunkt ermitteln
            var p = new Point();
            if (itemIndex == 0) { //vll auch (0,0), später!
                //var p = new Point(
                //    (Arguments.PanelSize.Width + s.Width) / 2,
                //    (Arguments.PanelSize.Height + s.Width) / 2
                //);
                p.X = (Arguments.PanelSize.Width + s.Width) / 2;
                p.Y = (Arguments.PanelSize.Height + s.Width) / 2;
            } else {
                //var p = new Point();
                //p=getpoint()
            }

            //mach' ein Rechteck d'raus
            var rectangle = new Rect() {
                Location = p,
                Size = s
            };

            //bis Platz gefunden
            var found = false;

            //Kollisionserkennung: über Vorgänger iterieren
            //Teste Position (x,y) ist frei für Wort mit Größe size
            while ( !found ) {
                if (itemIndex > 0) { //keine Kollision bei 1. Element
                    var intersect = new Rect();
                    foreach (Rect r in Taken) {
                        intersect = Rect.Intersect(rectangle, r);
                        if (intersect == System.Windows.Rect.Empty) { // darf ich benutzen
                            found = true; // mit rumschleifen aufhören
                            break;
                        } else { // Platz belegt
                                 // neuen Punkt finden


                            Point spiralPoint = GetSpiralPoint(r.Left);
                            int offsetX = Console.WindowWidth/100;
                            int offsetY = Console.WindowHeight/100;
                            var testPoint = new Point((int)(spiralPoint.X + offsetX), (int)(spiralPoint.Y - offsetY));





                        
                        }
                    }
                }

            };
            //bzw. of Position (x,y) bis Position (x+size.Width,y+size.Height) frei/leer ist
            //wenn nicht, neue Position anhand Spirale a(r,phi) => a(x,y)
            Taken.Add(rectangle); //platzieren

            var visualizedTag = new VisualizedWord(word) { 
                Position = new Position(p.X, p.Y),
				Opacity = CalculateRelativeValue(Arguments.OpacityRange, word.Weight)
			};

            //rechtecke für nächsten Durchlauf speichern
            Taken.Add( rectangle );

            if (StopAfterWords > 0 &&  itemIndex == (StopAfterWords - 1)) {
				CanAddWords = false;
			}

			return visualizedTag;
		}

        private double CalculateRelativeValue(Range range, double current) {
			return range.CalculateRelativeValue(new Range(0, MaxWeight), current);
		}
	}
}