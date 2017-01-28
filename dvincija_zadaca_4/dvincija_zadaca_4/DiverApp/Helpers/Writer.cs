using dvincija_zadaca_4.DiverApp.Composite;
using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Helpers
{
    public class Writer
    {
        /// <summary>
        /// Print all divers and their dives
        /// </summary>
        /// <param name="dives"></param>
        public static void PrintDives(IEnumerable<Dive> dives)
        {
            StringBuilder builder = new StringBuilder();
            int diverCounter = 1;

            builder.Append(GenerateHeading("PODACI O URONIMA"));

            foreach (Dive dive in dives)
            {
                diverCounter = 1;
                builder.AppendFormat("\n\n###### URON ######\n");
                builder.AppendFormat("Datum: \t\t{0}\nVrijeme: \t{1}\nTemperatura: \t{2}°C\nBroj ronioca: \t{3}/{4}\n", dive.dateTime.ToString("dd.MM.yyyy"), dive.dateTime.ToString("HH:MM"), dive.temperature, dive.NumOfDivers, dive.numOfDiversNeeded);
                builder.AppendFormat("Noćni uron: \t{0}\nFotografi: \t{1}\n\n", dive.isNightDive ? "Da" : "Ne", dive.numOfPhotographers);

                builder.AppendFormat("{0,-4}{1,-14}{2,-16}{3,-35}{4}\n", "#", "IME RONIOCA", "DATUM ROĐENJA", "CERTIFIKAT", "SPECIJALNOSTI");
                builder.AppendFormat("{0,-4}{1,-14}{2,-16}{3,-35}{4}\n", "--", "-----------", "-------------", "----------", "-------------");

                foreach (Diver diver in dive.Divers)
                    builder.AppendFormat("{0,-4}{1,-14}{2,-16}{3,-35}{4}\n", diverCounter++, diver.name, diver.birthDate, diver.certificate.name, string.Join(", ", diver.superPowers));
            }
            
            Console.WriteLine(builder.ToString());
        }

        public static void PrintEquipment(ConcreteEquipment equipment)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("{0,-50}Zalihe: {1}/{2}", equipment.name, equipment.stock, equipment.originalStock);

            Console.WriteLine(builder.ToString());
        }

        public static void PrintWarehouseStatusHeading()
        {
            Console.WriteLine("\nSTANJE ZALIHA OPREME NAKON OPREMANJA RONIOCA");
            Console.WriteLine("----------------------------------------------");
        }

        public static void PrintEquipmentReturn(DateTime returnDate, Diver diver, ConcreteEquipment equipment)
        {
            StringBuilder builder = new StringBuilder();
            
            builder.AppendFormat("{0,-8}{1,-40}", diver.name, equipment.name);
            builder.AppendFormat("{0, -24}{1,-10}", equipment.loanDate, equipment.maxLoanDays);
            builder.AppendFormat("{0}", returnDate, equipment.stock);

            Console.WriteLine(builder.ToString());
        }

        public static void PrintEquipmentReturnHeading(Dive dive)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("#############################################################################################");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("\nPOVRAT OPREME NA DATUME PRIJE URONA - {0}\n", dive.dateTime);
            Console.WriteLine("{0,-8}{1,-40}{2,-24}{3,-10}{4}", "IME", "OPREMA", "DATUM POSUDBE",  "BR. DANA", "DATUM POVRATA");
            Console.WriteLine("{0,-8}{1,-40}{2,-24}{3,-10}{4}", "---", "------", "--------------", "-------", "-------------");
        }

        public static void PrintEquipmentPerDiverInDive(Dive dive)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("\n\n###### URON ######\n");
            builder.AppendFormat("Datum: \t\t{0}\nTemperatura: \t{1}°C\nBroj ronioca: \t{2}/{3}\n", dive.dateTime, dive.temperature, dive.NumOfDivers, dive.numOfDiversNeeded);
            builder.AppendFormat("Noćni uron: \t{0}\nFotografi: \t{1}\n\n", dive.isNightDive ? "Da" : "Ne", dive.numOfPhotographers);

            // If there is enough divers for dive then print them otherwise print message
            if (dive.NumOfDivers == dive.numOfDiversNeeded)
            {
                foreach (Diver diver in dive.Divers)
                {
                    builder.AppendFormat("\n{0} - {1}\n", diver.name, diver.equipmentStatus);
                    foreach (ConcreteEquipment equipment in diver.EquipmentList)
                        builder.AppendFormat("{0}\n", equipment.name);

                    builder.AppendFormat("\n\n");
                }
            }

            else builder.Append("Za uron nema dovoljno ronioca! Svi ronioci iz urona vraćaju robu ukoliko ju imaju.");

            Console.WriteLine(builder.ToString());
        }

        /// <summary>
        /// Generate pretty heading
        /// </summary>
        /// <param name="heading">Heading</param>
        /// <returns>string</returns>
        private static string GenerateHeading(string heading)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("\n*********************************************************************************\n");
            builder.AppendFormat("*{0}\n", "*".PadLeft(80));
            builder.AppendFormat("*{0}{1}\n", heading.PadLeft(40), "*".PadLeft(40));
            builder.AppendFormat("*{0}\n", "*".PadLeft(80));
            builder.AppendFormat("*********************************************************************************\n\n");

            return builder.ToString();
        }
    }
}
