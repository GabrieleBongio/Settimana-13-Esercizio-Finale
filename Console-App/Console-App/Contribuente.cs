using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_App
{
    internal class Contribuente
    {
        /*
            Creazione di get e set per tutte le proprietà richieste
         */
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public char Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public double RedditoAnnuale { get; set; }

        /*
            metodo costruttore base per poter instanziare il contribuente all'inizio del programma
            parametri: nessuno
            return: nessuno
         */
        public Contribuente() { }

        /*
            metodo costruttore per poter inserire tutte le proprietà della classe
            parametri: i valori da assegnare a tutte le proprietà con relativo tipo
            return: nessuno
         */
        public Contribuente(
            string nome,
            string cognome,
            DateTime dataNascita,
            string codiceFiscale,
            char sesso,
            string comuneResidenza,
            double redditoAnnuale
        )
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            CodiceFiscale = codiceFiscale;
            Sesso = sesso;
            ComuneResidenza = comuneResidenza;
            RedditoAnnuale = redditoAnnuale;
        }

        /*
            metodo che calcola l'imposta dovuta dal contribuente in base al reddito
            parametri: nessuno
            return: l'imposta dovuta con tipo double
         */
        public double ImpostaDovuta()
        {
            if (RedditoAnnuale < 15000)
            {
                return (RedditoAnnuale / 100) * 23;
            }
            else if (RedditoAnnuale < 28000)
            {
                double Eccedenza = RedditoAnnuale - 15000;
                return 3450 + (Eccedenza / 100) * 27;
            }
            else if (RedditoAnnuale < 55000)
            {
                double Eccedenza = RedditoAnnuale - 28000;
                return 6960 + (Eccedenza / 100) * 38;
            }
            else if (RedditoAnnuale < 75000)
            {
                double Eccedenza = RedditoAnnuale - 55000;
                return 17220 + (Eccedenza / 100) * 41;
            }
            else
            {
                double Eccedenza = RedditoAnnuale - 75000;
                return 25420 + (Eccedenza / 100) * 43;
            }
        }
    }
}
