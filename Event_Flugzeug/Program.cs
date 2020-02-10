using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event_Flugzeug {
    class Program {
        static void Main ( string [] args ) {
            Flugzeug flugzeug = new Flugzeug ();
            flugzeug.OelDruckMesser += flugzeug.OnEventEintritt;
            flugzeug.FliegHerum ();
        }

    }

    public enum Schalter { EIN, AUS };

    class Flugzeug {
        private Random zfg = new Random ();
        private Schalter motor = Schalter.EIN;
        public int Flughoehe { get; set; } = 10000;
        public string Name { get; set; } = "AirBoing 7+7";
        public int Oeldruck { get; set; }
        public event EventHandler<EventArgs> OelDruckMesser;

        public void MotorZustandAendern () {
            if ( motor == Schalter.EIN ) {
                motor = Schalter.AUS;
            }
            else {
                motor = Schalter.EIN;
            }
        }

        public void FliegHerum () {
            for ( int i = 0 ; i < 100 ; i++ ) {
                this.Oeldruck = zfg.Next ( 0, 101 );
                Thread.Sleep ( 1000 );
                Console.WriteLine ( Oeldruck );

                if ( Oeldruck < 10 ) {
                    //OnEventEintritt(this, new EventArgs());
                    OelDruckMesser ( this, new EventArgs () );
                }
            }
        }

        public virtual void OnEventEintritt ( object source, EventArgs args ) {
            if ( source != null ) {
                Console.WriteLine ( "OOps der Öldruck ist zu niedrig - ich schalte mal den Motor ab" );
                Thread.Sleep ( 2000 );
                this.MotorZustandAendern ();
                Console.WriteLine ( motor.ToString () );
                Console.WriteLine ( "Der Motor wurde aus Sicherheitserwägungen erfolgreich abgeschaltet" );

                Thread.Sleep ( 5000 );
                Console.WriteLine ( "Ihre Flughöhe nimmt ab " );
                for ( int i = Flughoehe ; i >= 0 ; i -= 50 ) {
                    Console.WriteLine ( "Ihre aktuelle Flughöhe: {0} m", Flughoehe );
                    Flughoehe = Flughoehe - 50;
                }

                Console.WriteLine ( "==================================" );
                Console.WriteLine ( "{0} auf Boden angekommen", Name );
                Console.WriteLine ( "Vielen Dank für Ihre Verständnis ..." );

                Console.ReadLine ();
                Environment.Exit ( 0 );
            }
        }
    }
}
