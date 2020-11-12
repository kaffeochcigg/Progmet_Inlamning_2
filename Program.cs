using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    class Person
    {
        public string name, address, phone, email;

        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; phone = T; email = E;
        }
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
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, address, phone, email);
        }
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
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        Dict[i].Print();
                    }
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
        /* METHOD: RemovePerson (static)
        * PURPOSE: Deletes an existing contact
        * PARAMETERS: Dict stores all contacts
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
        *  PURPOSE: To read contacts from file         
        *  RETURN VALUE: Contacts from file
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
        * PARAMETERS: Dict stores all contacts
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
