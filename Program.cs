using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    /* CLASS: Person
     * PURPOSE: Enables for use in methods to access the contact list.
     */
    class Person
    {
        public string name, address, phone, email;
        /* Constructor: Person
         * Purpose: Creates the object: Person
         * Parameters: sets 'N' = 'name', sets 'A' = 'address', sets 'T' = 'phone', sets 'E' = 'email'
         */
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; phone = T; email = E;
        }
        /* Constructor: Person
         * Purpose: Asking for user input when creating new object from the class Person    
         */
        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }
        /* Method: Print
         * Purpose: To read the contacts when it's being used in PrintList-method
         */
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, address, phone, email);
        }
        /* Method: ModifyInfo
         * Purpose: To modify the current contacts
         * Parameters: 'toBeChanged' is the feild we want to change, 'newValue' the new value input
         * Return value: Sends back input to the object 'Person'
         */
        public void ModifyInfo(string toBeChanged, string newValue)
        {
            switch (toBeChanged)
            {
                case "namn":
                    name = newValue;
                    break;
                case "adress":
                    address = newValue;
                    break;
                case "telefon":
                    phone = newValue;
                    break;
                case "email":
                    email = newValue;
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = ReadFile();
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    Dict.Add(new Person());
                }
                else if (command == "ta bort")
                {
                    RemovePerson(Dict);
                }
                else if (command == "visa")
                {
                    PrinList(Dict);
                }
                else if (command == "ändra")
                {
                    ModifyPerson(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }
        /* METHOD: PrintList (static)
         * PURPOSE: to loop and print the existing contact
         * PARAMETERS: Dict stores all Person objects in a list
         * RETURN VALUE: Reads all contacts
         */
        private static void PrinList(List<Person> Dict)
        {
            for (int i = 0; i < Dict.Count(); i++)
            {
                Person P = Dict[i];
                P.Print();
            }
        }
        /* METHOD: RemovePerson (static)
         * PURPOSE: Deletes an existing contact
         * PARAMETERS: Dict stores all Person objects in a list
         * RETURN VALUE: Returns the index of selected person to delete
         */
        private static void RemovePerson(List<Person> Dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string removeName = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == removeName) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", removeName);
            }
            else
            {
                Dict.RemoveAt(found);
            }
        }
        /* METHOD: ReadFile (static)
         * PURPOSE: To read contacts, split at '#' and store the person object in Dict list    
         * RETURN VALUE: Contacts from file
         */
        static List<Person> ReadFile()
        {
            List<Person> Dict = new List<Person>();
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    string[] word = line.Split('#');
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
            return Dict;
        }
        /* METHOD: ModifyPerson (static)
         * PURPOSE: To alter info on a specific contact
         * PARAMETERS: Dict stores all Person objects in a list
         * RETURN VALUE: Returns input to the Object Person
         */
        static List<Person> ModifyPerson(List<Person> Dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string changeInfo = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == changeInfo) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", changeInfo);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string toBeChanged = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", toBeChanged, changeInfo);
                string newValue = Console.ReadLine();
                Dict[found].ModifyInfo(toBeChanged, newValue);
            }
            return Dict;
        }
    }
}
