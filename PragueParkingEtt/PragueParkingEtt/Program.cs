using PragParking;
using System;
using System.IO;

class MonthsExample

{
    
     static void Main()
    {
        //Ändrar backgrundfärgen
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.Black;

        //Nul i parkeringsmatrisen
        Ticket Nul = new Ticket("Null", "Null 111", DateTime.Now);
        Ticket[,] ParkingMatrix = new Ticket[20, 2];
        int count = 0;
        bool menyloop = false;

        
        //Fyller matrisen med Nul Objekt och skriver ut innan menyn visas
        for (int i = 0; i < ParkingMatrix.GetLength(0); i++)
        {

            for (int j = 0; j < ParkingMatrix.GetLength(1); j++)
            {

                ParkingMatrix[i, j] = Nul;



            }

        }

        FörInitialisera(ParkingMatrix);

        
        while (true)
        {
            Console.WriteLine("Välkommna till Prague Parking. ");

            Console.WriteLine("[s]ök fordon ");
            Console.WriteLine("[h]ämta ut fordon ");
            Console.WriteLine("[l]ämna in fordon ");
            Console.WriteLine("[t]illgänliga Parkerings Platser ");
            Console.WriteLine("[f]lytta fordon ");





            //Om användaren skriver annat än korrekt menyval. 
            string userinput = Console.ReadLine();
            while (userinput != "s" && userinput !="S" && userinput !="h" && userinput!="H" && userinput != "l" && userinput != "L" && userinput !="t" && userinput!= "T" && userinput!="f" && userinput != "F") 
            {
                Console.WriteLine("Fel inmatning, försök igen.");
                userinput = Console.ReadLine();
            }
            // Meny
            switch (userinput)
            {

                case "s":
                case "S":
                    string input = AnvändarinputMetod();

                    Sökfunktion(input, ParkingMatrix);

                    break;
                case "h":
                case "H":
                     input = AnvändarinputMetod();
                    Ticket sparad = Hämtaut(ParkingMatrix, input, Nul);
                    //Kostnadsmetod(sparad);
                    string [] sparaTillLoggen2 = Kostnadsmetod(sparad); 
                    //Returnerar timespan och kostnad i stringarray-format
                    Loggfunktion(sparad, sparaTillLoggen2);

                   
                    break;
                case "l":
                case "L":

                    Ticket blank = LämnaInTvå();
                    LämnaInEtt(blank, ParkingMatrix, Nul);
                   

                    break;
                case "t":
                case "T":
                   
                    Parkeringsvy(ParkingMatrix);

                    break;

                case "f":
                case "F":


                    input = AnvändarinputMetod();
                    //FlyttaFordonsDjävul(input, ParkingMatrix, Nul); //nollställer gamla p-platsen, sparar objektet i "objektSomFlyttas".
                    //returnvärdet sparas i denna variabel
                    Ticket objectSomFlyttas = FlyttaFordonsDjävul(input, ParkingMatrix, Nul); 
                    

                    Parkeringsvy(ParkingMatrix);
                    OmplaceraFordonsDjävul(ParkingMatrix, objectSomFlyttas);

                    break;
            }


        }

    }
    // Indentifierar Objekt & placerar i matris.
    static Ticket[,] LämnaInEtt(Ticket blank, Ticket[,] ParkingMatrix, Ticket Nul)
    {

        for (int i = 0; i < ParkingMatrix.GetLength(0); i++)
        {
            // Bilen är begränsad till att enbart existera i [i,0] och kan därför alrid. existera två gånger
            // Regler för hur Bil placeras.
            if (blank.Fordonstyp == "BIL")
            {

                if (ParkingMatrix[i, 0].Fordonstyp != "BIL" && ParkingMatrix[i, 1].Fordonstyp != "MC" && ParkingMatrix[i, 0].Fordonstyp != "MC")
                {
                    ParkingMatrix[i, 0] = blank;
                    break;
                }


            }
            if (blank.Fordonstyp == "MC")
            {


                if (ParkingMatrix[i, 0].Fordonstyp != "BIL" && ParkingMatrix[i, 1].Fordonstyp != "MC")
                {
                    if (ParkingMatrix[i, 0].Fordonstyp == "MC")
                    {
                        ParkingMatrix[i, 1] = blank;
                        break;
                    }
                    else if (ParkingMatrix[i, 0].Fordonstyp != "MC" && ParkingMatrix[i, 0].Fordonstyp != "BIL")
                    {
                        ParkingMatrix[i, 0] = blank;
                        break;
                    }
                }

            }
            // Regler för hur MC indentifieras och placeras i matris

        }



        return ParkingMatrix;
    }
    // Tar användarinput och förbereder den för matrisen.
    static Ticket LämnaInTvå()
    {
        // Tar emot användarinput genom n variabel, Regnr.
        string n = " ";
        Console.WriteLine("Välkomna till Prague Parking co. Vänligen mata in Reg Nummer SWE 123: ");
        Ticket blank = new Ticket(n, n, DateTime.Now);
        n = Console.ReadLine();
        blank.Regnr = n;

        // Tar emot användarinput Fordonstyp.
        Console.WriteLine("Tack! Skriv nu in vilket fordon du önskar lämna in. B för Bil eller  M för MC. ");
        n = Console.ReadLine();
        string bil = "BIL", mc = "MC";

        while (n != "m" && n != "b" && n != "M" && n != "B") //Om användaren inte skriver b/m så får de göra om tills de lyckas
        {
            Console.WriteLine("Fel inmatning, försök igen.");
            n = Console.ReadLine();
        }
        if (n == "m")
        {
            blank.Fordonstyp = mc;
        }
        if (n == "b")
        {
            blank.Fordonstyp = bil;
        }
        // blank.Fordonstyp = n;

        /*
        // Tar emot användardata kolum,rad. Kundens parkerings position.
        int kol = 0;
        int rad = 0;
        Console.WriteLine("Välj parkeringsplats... ");
        kol = Convert.ToInt32(Console.ReadLine());
        rad = Convert.ToInt32(Console.ReadLine());
        */


        // Skriver ut inlämningstid & lagrar.
        Console.WriteLine("Utmärkt, Din bil är nu inlämnad kl: " + DateTime.Now);
        blank.Ankomsttid = DateTime.Now;



        //Ticket blank = new Ticket(n, n, DateTime.Now);
        //blank.Regnr = n;
        //blank.Fordonstyp = n;
        //blank.Ankomsttid = DateTime.Now;
        return blank;
    }
   
    // För initialiserar 3 Bilar & 3 MC
    static Ticket[,] FörInitialisera(Ticket[,] ParkingMatrix)
    {

        Ticket Bilx1 = new Ticket("BIL", "SWE 111", DateTime.Now);
        Ticket Bilx2 = new Ticket("BIL", "SWE 123", DateTime.Now);
        Ticket Bilx3 = new Ticket("BIL", "SWE 321", DateTime.Now);

        Ticket MCx1 = new Ticket("MC", "SWE 243", DateTime.Now);
        Ticket MCx2 = new Ticket("MC", "SWE 353", DateTime.Now);
        Ticket MCx3 = new Ticket("MC", "SWE 432", DateTime.Now);


        ParkingMatrix[0, 0] = Bilx1;
        ParkingMatrix[1, 0] = Bilx2;
        ParkingMatrix[2, 0] = Bilx3;

        ParkingMatrix[3, 1] = MCx1;
        ParkingMatrix[3, 0] = MCx2;
        ParkingMatrix[4, 0] = MCx3;

        return ParkingMatrix;
    }

    // Funktion som enbart tar emot användarinput
    static string AnvändarinputMetod()
    {
        Console.WriteLine("Vänligen skriv in Registrerings nummer. ");
        string input = Console.ReadLine();
        return input;


    }

    // Hämta ut fordon genom linjärsökning, loggar uthämtaningar.
    static Ticket Hämtaut(Ticket[,] ParkingMatrix, string input, Ticket Nul)
    {
        Ticket sparaobjekt = Nul;

        //LinjärsökningMetod

        for (int i = 0; i < ParkingMatrix.GetLength(0); i++)
        {

            for (int j = 0; j < ParkingMatrix.GetLength(1); j++)
            {
                // Sparar uthämtat objekt i tillfällig variabel samt ersätter indexet med Nul Objekt
                if (ParkingMatrix[i, j].Regnr == input)
                {
                    sparaobjekt = ParkingMatrix[i, j];
                    ParkingMatrix[i, j] = Nul;


                }
            }

        }


        return sparaobjekt;
    }

    static Ticket FlyttaFordonsDjävul(string input, Ticket[,] ParkingMatrix, Ticket Nul)
    {
        for (int i = 0; i < ParkingMatrix.GetLength(0); i++)
        {

            for (int j = 0; j < ParkingMatrix.GetLength(1); j++)
            {

                if (ParkingMatrix[i, j].Regnr == input)
                {
                    //Lagarar Objektet i variabel och placerar nytt nul värde i vald position
                    Ticket objectSomFlyttas = ParkingMatrix[i, j];
                   //Denna gör att om användaren väljer fel pplats vid flytt så försvinner objektet helt från matrisen.
                   //och detta gör att kunden inte kan hämta ut.
                    ParkingMatrix[i, j] = Nul;
                    return objectSomFlyttas;
                    break;
                }




            }

        }
        Console.WriteLine("Vart skulle du vilja flytta ditt fordon? ");
        return Nul; 

    }
    // Denna metod räknar ut priset på vistelsen
    static string[] Kostnadsmetod(Ticket objektFörUtlämning)
    {
        
        DateTime nutid = DateTime.Now;
        TimeSpan nutidMinusAnkomst = nutid.Subtract(objektFörUtlämning.Ankomsttid);
        int antalDagar = nutidMinusAnkomst.Days;
        int antalTimmar = nutidMinusAnkomst.Hours;
        int antalMinuter = nutidMinusAnkomst.Minutes;

        //sparar tid på parkering och avhämtningstid i en string-array. Returneras sidan till loggmetoden
        string[] sparaTillLoggen = new string[3];
        sparaTillLoggen[0] = nutidMinusAnkomst.ToString();
        sparaTillLoggen[1] = nutid.ToString();

        int dagarGgr24 = 0;
        if (antalMinuter >= 1) //om minuterna är mer än 0. Lägg på 1 h.
        {
             dagarGgr24 = antalDagar * 24; //räknar antal timmar på antal dagar
            dagarGgr24 += antalTimmar;
            dagarGgr24 += 1; //lägger till en extra timma
            Console.WriteLine(dagarGgr24); 
        }
        else //om tidsspannets minut är precis 0
        {
             dagarGgr24 = antalDagar * 24;
            dagarGgr24 += antalTimmar;
        }

        int bilTidKostnad = 0;
        if (objektFörUtlämning.Fordonstyp == "BIL")
        {
            bilTidKostnad = dagarGgr24 * 45;

            Console.WriteLine("Kostnaden för din bil blir: " + bilTidKostnad + ":-"+ "Tack för din vistelse " + nutidMinusAnkomst);
            sparaTillLoggen[2] = bilTidKostnad.ToString();
        }

        else 
        {

           int mcTidKostnad = dagarGgr24 * 25;
            Console.WriteLine("Kostnaden för din MC blir: " + mcTidKostnad + ":-" + "Tack för din vistelse " + nutidMinusAnkomst);
            sparaTillLoggen[2] = mcTidKostnad.ToString();
        }

        return sparaTillLoggen;





    }

    static void Parkeringsvy(Ticket[,] ParkingMatrix)
    {
        int count = 0;
        for (int i = 0; i < 20; i++)
        {


            Console.WriteLine("------------" + " [ "+ count +" ]"+'\u2591');
            Console.Write('\u2551');


            for (int j = 0; j < 2; j++)
            {

                Console.Write("|");
                Console.Write(" " + ParkingMatrix[i, j].Fordonstyp);
                count++;


            }
            //Console.Write(" " + ParkingMatrix[i, j].Regnr);
            // Console.Write(" " + ParkingMatrix[i, j].Ankomsttid);

            Console.WriteLine(" ");

        }
        Console.WriteLine("------------");
    }

        //Flyttar fordon
        static void OmplaceraFordonsDjävul(Ticket[,] ParkingMatrix, Ticket objectSomFlyttas)
    {
        
        Console.WriteLine("Ange mu vilken Rad du vill stå på, 0 till 19 ");
    

        string användarinput = Console.ReadLine();

        
        // Felhanterar kolumnval, tar bort allt som ej SKALL vara sökbart.
        int radval;
        if (int.TryParse(användarinput, out radval))
        {
            
        }
        else
        {   // loopar medans vi inte kan konventera
            while (int.TryParse(användarinput, out radval) != true)
            {
                Console.WriteLine("Fel fakking inamtning, försök igen");
                användarinput = Console.ReadLine();

            }

        }


        while (radval > 19 || radval<0)
        {
            Console.WriteLine("Fel inamtning, var god försök igen");
             användarinput = Console.ReadLine();
            int.TryParse(användarinput, out radval);
            
        }
        
        Console.WriteLine("Ange mu vilken kolum du vill stå på, 0 eller 1 ");



        // Felhanterar kolumnval, tar bort allt som ej SKALL vara sökbart.
        användarinput = Console.ReadLine();
        int kolumnval;
        if (int.TryParse(användarinput, out kolumnval))
        {
         // Behöver inget.
        }
        else
        {
            while (int.TryParse(användarinput, out kolumnval) != true)
            {
                Console.WriteLine("Fel inamtning, försök igen");
                användarinput = Console.ReadLine();

            }

        }


        // Felhanterar kolumnval
        while (kolumnval > 1 || kolumnval !<0) 
        {
            Console.WriteLine("Fel inamtning, var god försök igen");
            //behöver konverteras från string till int varje gång
            användarinput = Console.ReadLine();
            int.TryParse(användarinput, out kolumnval);


        }
        //Här finns en spärr så bilar inte kan ta stå på en enda halva av en parkering (i,1)
        if (objectSomFlyttas.Fordonstyp == "BIL" && ParkingMatrix[radval,kolumnval] == ParkingMatrix[radval,1])
        {
            //användaren behöver omregistrera/lämna in sitt fordon
            Console.WriteLine("Finns ej plats, vänligen lämna in ditt fordon."); 

        }

        //Här finns en spärr så bilar inte kan ta över en plats 
        if (ParkingMatrix[radval, kolumnval].Fordonstyp == "BIL" || ParkingMatrix[radval, kolumnval].Fordonstyp == "MC")
        {
            Console.WriteLine("Platsen är redan upptagen, vänligen lämna in ditt fordon.");
            
        }
        
        else
        {
            //placerar in fordonet i den önskade platsen
            ParkingMatrix[radval, kolumnval] = objectSomFlyttas;
        }

                
        
    }
    
    static void Sökfunktion(string input, Ticket[,] ParkingMatrix)
    {
        for (int i =0; i< ParkingMatrix.GetLength(0); i++)
        {
            for (int j=0; j<ParkingMatrix.GetLength(1); j++)
            {
                if (ParkingMatrix[i, j].Regnr == input)
                {
                    Console.WriteLine("Ditt fordon står på plats: {0},{1}", i,j);
                   
                    break;
                }
                
               else if (ParkingMatrix[i, j].Regnr != input)
                {
                   
                    Console.WriteLine("Fordon finns ej. Var god försök igen.");
                    
                }
                
            }
            break;
        }

    }

    static void Loggfunktion (Ticket sparad, string [] sparaTillLoggen) //(Ticket hämtningsobjekt,)
    {
        //Arrayen sparaTillLoggen är en samling av alla objektfält samt kostnader och tidslängd på parkering...
        string[] textlogg = new string[6];
        // index 0 från sparaTillLogg = uthämtningstid minus ankomst
        textlogg[0] = "Registreringsnummer: " + sparad.Regnr.ToString();
        textlogg[1] = "Ankomsttid: " +  sparad.Ankomsttid.ToString();
        textlogg[2] = "Fordonstyp: " + sparad.Fordonstyp;
        
        
        textlogg[3] = "Spenderad tid på P-plats: " + sparaTillLoggen[0];
        textlogg[4] = "Uthämtningstid: " + sparaTillLoggen[1];
        textlogg[5] = "Total kostnad: " + sparaTillLoggen[2] + ":-";

        List<string> textloggList = textlogg.ToList(); 
        //Tydligen kan man inte skriva ut en array smidigt. Fick kovertera till List

        string filePathway = @"C:\Users\kimgu\source\repos\PragueParkingEtt\logg.txt";
        
        //skriver all data från listan till extern textfil. StreamWrite gör att vi inte skriver över gammal textdata i filen
        //utökning av element gör att datan inte skriver över texten
        using (StreamWriter sw = File.AppendText(filePathway)) 
        {
            foreach (string line in textloggList)
            {
                sw.WriteLine(line);
            }
        }



    }
}