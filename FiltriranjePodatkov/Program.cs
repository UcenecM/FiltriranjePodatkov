using System;

namespace FiltriranjePodatkov
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ustvaru tabele s podatki
            string[] ime = { "Ana", "Bojan", "Ana", "David", "Eva", "Franc", "Grega", "Ivo", "Ivo", "Jakob", "Katja", "Luka", "Maja" };
            int[] starosti = { 25, 30, 35, 40, 28, 33, 45, 50, 22, 29, 31, 27, 26 };
            int[] plače = { 50000, 60000, 70000, 80000, 55000, 62000, 72000, 76000, 48000, 53000, 59000, 57000, 51000 };


            //Program uporabnika vpraša po kriterijih za filtriranje
            Console.WriteLine("Vnesite stolpec po katerem želite filtrirati (ime, starost, plača):");
            string kriterijStolpec = Console.ReadLine();

            Console.WriteLine("Vnesite operator primerjave (==, !=, <, >, <=, >=):");
            string kriterijOperator = Console.ReadLine();

            Console.WriteLine("Vnesite vrednost za primerjavo:");
            string kriterijVrednost = Console.ReadLine();

            string[] kriterijStolpci = { kriterijStolpec };
            string[] kriterijOperatorji = { kriterijOperator };
            string[] kriterijVrednosti = { kriterijVrednost };

            //Ustvarimo tabelo z indeksi filtriranih zapisov in klic metode FilterRecords
            int[] filtriraniIndeksi = FilterRecords(ime, starosti, plače, kriterijStolpci, kriterijOperatorji, kriterijVrednosti);

            // Izpis rezultatov
            Console.WriteLine("Filtrirani zapisi:");

            //Zanka gre skozi vse indekse v tabeli filtriranih indeksov in izpiše ustrezne podatke, kateri imajo enake idekse
            foreach (var indeks in filtriraniIndeksi)
            {
                Console.WriteLine($"Ime: {ime[indeks]}, Starost: {starosti[indeks]}, Plača: {plače[indeks]}");
            }

        }






        //Metoda za filtriranje podatkov
        static int[] FilterRecords(string[] imena, int[] starosti, int[] plače, string[] kriterijStolpci, string[] kriterijOperatorji, string[] kriterijVrednosti)
        {
            int dolžina = imena.Length; //Shranimo dolžino tabele ime (13)
            int[] filtriraniIndeksi = new int[dolžina]; //Ustvarimo tabelo filtriranih indeksov dolžine 13
            int števecFiltriranih = 0; //Števec, ki šteje koliko zapisov imamo, k usterjo kriterijem

            for (int i = 0; i < dolžina; i++)
            {
                bool ustrezaKriteriju = true; //Ustvarimo bool spremenljivko, ki nam pove ali zapis ustreza kriterijem

                for (int j = 0; j < kriterijStolpci.Length; j++)   //Pogledamo, koliko kriterijev imamo in gremo skozi vse
                {
                    string stolpec = kriterijStolpci[j];  //Pogleda kriterij, ki je na indeksu j
                    string operatorPrimerjave = kriterijOperatorji[j];  //Pogleda operator primerjave, ki je na indeksu j
                    string vrednost = kriterijVrednosti[j]; //Poda vrednost, ki je na indeksu j

                    if (stolpec == "ime") 
                    {
                        if (!PrimerjajNize(imena[i], operatorPrimerjave, vrednost)) //Pokliče metodo PrimerjajNize, katero vrne true ali false, ter s ! obrne vrednost
                        { //Če vrne true, se spremeni v false in se ne izvede koda v if stavku
                            ustrezaKriteriju = false;  
                            break;
                        }
                    }
                    else if (stolpec == "starost")
                    {
                        if (!PrimerjajSt(starosti[i], operatorPrimerjave, int.Parse(vrednost)))
                        {
                            ustrezaKriteriju = false;
                            break;
                        }
                    }
                    else if (stolpec == "plača")
                    {
                        if (!PrimerjajSt(plače[i], operatorPrimerjave, int.Parse(vrednost)))
                        {
                            ustrezaKriteriju = false;
                            break;
                        }
                    }
                }

                if (ustrezaKriteriju) //Če zapis ustreza kriterijem, ga dodamo osebo v tabelo filtriranih indeksov
                {
                    filtriraniIndeksi[števecFiltriranih] = i; //dodamo indeks osebe enega za drugim
                    števecFiltriranih++; //povečamo števec filtriranih zapisov, da lahko damo naslednji zapis na naslednji indeks
                }
            }

            // Zmanjšamo tabelo na dejansko število filtriranih zapisov
            int[] rezultat = new int[števecFiltriranih]; //Ustvari tabelo z dolžino dejanskih filtriranih zapisov
            Array.Copy(filtriraniIndeksi, rezultat, števecFiltriranih);//Metoda Array.Copy kopira elemente iz ene tabele v drugo, s določenim ševilom 
            return rezultat; //vrnimo tabelo rezultat v main 
        }

        //Metoda prevero, ter če je pravilno vne true ali false 
        static bool PrimerjajSt(int a, string op, int b)
        {
            switch (op)
            {
                case ">": 
                    return a > b;
                case "<":
                    return a < b;
                case ">=":
                    return a >= b;
                case "<=": 
                    return a <= b;
                case "==":
                    return a == b;
                case "!=": 
                    return a != b;
                default: throw new ArgumentException("Neveljavna operacija");
            }
        }

        static bool PrimerjajNize(string a, string op, string b)
        {
            switch (op)
            {
                case "==":
                    return a == b;
                case "!=":
                    return a != b;
                default: throw new ArgumentException("Neveljavna operacija za nize");
            }
        }
    }
}
