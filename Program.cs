using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace AgeCalculator
{
    [Serializable]
    class BinarySerializationClass : IDeserializationCallback
    {
        public int BirthYear { get; set; }
        
        [NonSerialized]
        
        public int ageCalculated;
        public BinarySerializationClass(int age)
        {
            BirthYear = age;
        }
        public void OnDeserialization(object obj)
        {
            ageCalculated = DateTime.Now.Year - BirthYear;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your birth year : ");
            int inputYear = int.Parse(Console.ReadLine());
            BinarySerializationClass BSC = new BinarySerializationClass(inputYear);
            FileStream FS = new FileStream(@"AgeFile.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter BF = new BinaryFormatter();
            BF.Serialize(FS, BSC);
            FS.Seek(0, SeekOrigin.Begin);
            BinarySerializationClass result = (BinarySerializationClass)BF.Deserialize(FS);
            Console.WriteLine("\nAge of Person is " + result.ageCalculated);

        }
    }
}