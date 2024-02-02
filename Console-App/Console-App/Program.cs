using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Console_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
                Questa parte serve a sistemare la culture e il linguaggio, l'ho usata per stemare la scrittura dei valori di reddito e imposte
             */
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var culture = new CultureInfo("fr-FR");
            culture.NumberFormat.CurrencySymbol = "€";
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = ".";
            culture.NumberFormat.CurrencyPositivePattern = 0;
            culture.NumberFormat.CurrencyNegativePattern = 2;

            /*
                Questa parte serve a scrivere l'incipit del codice con il rettangolo e la scritta
             */

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("/===----====----====----===\\");
            Console.WriteLine("|                          |");
            Console.WriteLine("|    Nuovo Contribuente    |");
            Console.WriteLine("|                          |");
            Console.WriteLine("\\===----====----====----===/");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");

            /*
                Questa parte serve per inserire il nome e renderlo nel formato Nome (cioè prima lettera maiuscola e resto minuscolo), impedisce di lasciare il campo nome vuoto
             */

            string nomeInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inserisci il tuo nome");
                Console.ForegroundColor = ConsoleColor.White;
                nomeInput = Console.ReadLine();
                Console.WriteLine("");
                if (nomeInput == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Errore, nome non valido");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                }
            } while (nomeInput == "");

            string nome = string.Concat(
                char.ToUpper(nomeInput[0]),
                nomeInput.Substring(1).ToLower()
            );

            /*
                Questa parte serve per inserire il cognome e renderlo nel formato Cognome (cioè prima lettera maiuscola e resto minuscolo), impedisce di lasciare il campo cognome vuoto
             */

            string cognomeInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inserisci il tuo cognome");
                Console.ForegroundColor = ConsoleColor.White;
                cognomeInput = Console.ReadLine();
                Console.WriteLine("");
                if (cognomeInput == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Errore, cognome non valido");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                }
            } while (cognomeInput == "");

            string cognome = string.Concat(
                char.ToUpper(cognomeInput[0]),
                cognomeInput.Substring(1).ToLower()
            );

            /*
                Questa parte serve per inserire la data con tipo DateTime, c'è una gestione degli errori che fa inserire nuovamente se il formato non è corretto oppure la data è futura
            */

            bool dataComplete = false;
            DateTime dataNascita = new DateTime();

            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inserisci la tua data di nascita nel formato (GG/MM/AAAA)");
                Console.ForegroundColor = ConsoleColor.White;
                string data = Console.ReadLine();
                Console.WriteLine("");
                if (data.Length == 10 && data[2] == '/' && data[5] == '/')
                {
                    string[] dettaglidata = data.Split('/');
                    try
                    {
                        int giorno = int.Parse(dettaglidata[0]);
                        int mese = int.Parse(dettaglidata[1]);
                        int anno = int.Parse(dettaglidata[2]);
                        dataNascita = new DateTime(anno, mese, giorno);
                        if (DateTime.Compare(dataNascita, DateTime.Today) > 0)
                        {
                            throw new Exception();
                        }
                        dataComplete = true;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Errore nell'inserimento della data di nascita, controlla di non aver inserito lettere, non aver inserito un mese o un giorno non esistente e di non aver inserito una data non ancora avvenuta"
                        );
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Errore nell'inserimento della data di nascita, riprova");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!dataComplete);

            /*
                Questa parte serve per inserire il codice fiscale e renderlo nel formato CODICEFISCALE (cioè tutto con lettere maiuscole), anche questo impedisce di lasciare il campo vuoto
             */

            string codiceFiscaleInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inserisci il tuo codice fiscale");
                Console.ForegroundColor = ConsoleColor.White;
                codiceFiscaleInput = Console.ReadLine();
                Console.WriteLine("");
                if (codiceFiscaleInput == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Errore, codice fiscale non valido");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                }
            } while (codiceFiscaleInput == "");

            string codiceFiscale = codiceFiscaleInput.ToUpper();

            /*
                Questa parte serve per inserire il sesso, gli unici valori possibili sono 'M' = Maschio, 'F' = Femmina e 'N' = Non Binario
             */

            char sesso = ' ';
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inserisci il tuo sesso (M/F/N)");
                Console.ForegroundColor = ConsoleColor.White;
                string sessoInput = Console.ReadLine();
                Console.WriteLine("");
                if (sessoInput == "M" || sessoInput == "F" || sessoInput == "N")
                {
                    sesso = Convert.ToChar(sessoInput);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Errore, sesso non valido");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                }
            } while (sesso == ' ');

            /*
                Questa parte serve per inserire il comonue di residenza e renderlo nel formato Comune (cioè iniziale maiuscola e il resto minuscolo), anche questo impedisce di lasciare il campo vuoto
             */

            string comuneResidenzaInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inserisci il tuo comune di residenza");
                Console.ForegroundColor = ConsoleColor.White;
                comuneResidenzaInput = Console.ReadLine();
                Console.WriteLine("");
                if (comuneResidenzaInput == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Errore, comune non valido");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                }
            } while (comuneResidenzaInput == "");

            string comuneResidenza = string.Concat(
                char.ToUpper(comuneResidenzaInput[0]),
                comuneResidenzaInput.Substring(1).ToLower()
            );

            /*
                Questa parte serve per inserire il reddito annuale, impedisce di lasciare il campo vuoto e di inserire un numero negativo
             */

            int redditoAnnuale = -1;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inserisci il tuo reddito annuale");
                Console.ForegroundColor = ConsoleColor.White;
                string redditoAnnualeInput = Console.ReadLine();
                Console.WriteLine("");
                try
                {
                    redditoAnnuale = int.Parse(redditoAnnualeInput);
                    if (redditoAnnuale < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Errore, il reddito annuale non può essere minore di 0");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("");
                        redditoAnnuale = -1;
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Errore, reddito annuale non valido");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                }
            } while (redditoAnnuale == -1);

            Contribuente contribuente = new Contribuente(
                nome,
                cognome,
                dataNascita,
                codiceFiscale,
                sesso,
                comuneResidenza,
                redditoAnnuale
            );
            double impostaDaVersare = contribuente.ImpostaDovuta();

            /*
                Svuoto la console e inserisco l'output richiesto, arrotondando reddito dichiarato e imposta da versare
             */

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("========================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE");
            Console.WriteLine("");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome}");
            Console.WriteLine("");
            Console.WriteLine(
                $"Nato il {contribuente.DataNascita.ToShortDateString()} ({contribuente.Sesso})"
            );
            Console.WriteLine("");
            Console.WriteLine($"Residente a {contribuente.ComuneResidenza}");
            Console.WriteLine("");
            Console.WriteLine($"Codice fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine("");
            Console.WriteLine(
                $"Reddito dichiarato: {contribuente.RedditoAnnuale.ToString("C", culture)}"
            );
            Console.WriteLine("");
            Console.WriteLine($"IMPOSTA DA VERSARE: {impostaDaVersare.ToString("C", culture)}");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("========================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
    }
}
