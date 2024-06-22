namespace hw_02;

using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static string PrintableArray<T>(IEnumerable<T> array)
    {
        return $"['{string.Join("', '", array)}']";
    }

    static void HW02_01()
    {
        // Реверс строки/масиву. Без додаткового масиву. Складність О(n).

        void ReverseArray(string[] input)
        {
            int halfLength = input.Length / 2;

            for (int i = 0; i < halfLength; i++)
            {
                (input[i], input[input.Length - 1 - i]) = (input[input.Length - 1 - i], input[i]);
            }

            return;
        }

        string ReverseString(string input)
        {
            return string.Join("", from i in Enumerable.Range(0, input.Length) select input[input.Length - 1 - i]);
        }

        Console.WriteLine("===============01===============");

        string[] array = new string[] { "Volvo", "BMW", "Ford", "Porsche", "Hyundai" };
        Console.Write(PrintableArray(array));
        ReverseArray(array);
        Console.WriteLine($" -> {PrintableArray(array)}");

        string str = "Hello";
        string reversed_str = ReverseString(str);
        Console.WriteLine($"{str} -> {reversed_str}");
        Console.WriteLine("================================");
        Console.WriteLine("");

    }

    static void HW02_02()
    {
        // Фільтрування неприпустимих слів у строці. Має бути саме слова, а не частини слів.

        string Replacement(Match match)
        {
            return string.Join("", from i in Enumerable.Range(0, match.Length) select '*');
        }

        string CensorWords(string input, string[] forbidden)
        {
            string pattern = @"\b" + string.Join(@"\b|\b", forbidden) + @"\b";
            Regex regx = new Regex(pattern, RegexOptions.IgnoreCase);
            input = regx.Replace(input, Replacement);

            return input;
        }

        Console.WriteLine("===============02===============");

        string toBeCensored = "It is a good day to die. Or not that Good, for that matter, oh my Goods.. begood.. or die... dieded";
        string[] forbidden = { "good", "die" };

        Console.WriteLine(toBeCensored);
        Console.WriteLine(CensorWords(toBeCensored, forbidden));

        Console.WriteLine("================================");
        Console.WriteLine("");
    }

    static void HW02_03()
    {
        // Генератор випадкових символів. На вхід кількість символів, на виході
        // рядок з випадковими символами.

        string RandomStringGenerator(int length)
        {
            Random random = new Random();
            return string.Join("", (from i in Enumerable.Range(0, length) select (char)random.Next(33, 126)).Order());
        }

        Console.WriteLine("===============03===============");

        Console.WriteLine($"Random(3): {RandomStringGenerator(3)}");
        Console.WriteLine($"Random(7): {RandomStringGenerator(7)}");
        Console.WriteLine($"Random(11): {RandomStringGenerator(11)}");
        Console.WriteLine($"Random(19): {RandomStringGenerator(19)}");

        Console.WriteLine("================================");
        Console.WriteLine("");
    }

    static void HW02_04()
    {
        // "Дірка" (пропущене число) у масиві. Масив довжини N у випадковому
        // порядку заповнений цілими числами з діапазону від 0 до N. Кожне число
        // зустрічається в масиві не більше одного разу. Знайти відсутнє число
        // (дірку). Є дуже простий та оригінальний спосіб вирішення.
        // Складність алгоритму O(N). Використання додаткової пам'яті,
        // пропорційної довжині масиву не допускається.

        int MissingNumber(int[] array)
        {
            int sum = array.Sum();
            int correctSum = array.Length * (array.Length + 1) / 2;

            return correctSum - sum;
        }

        Console.WriteLine("===============04===============");

        int[] arrayA = { 0, 2, 3, 4, 5 };
        int[] arrayB = { 0, 1, 2, 3, 4, 6 };
        int[] arrayC = { 0, 1, 3, 4, 5, 6, 7 };
        int[] arrayD = { 1, 2, 3, 4, 5, 6, 7, 8 };
        int[] arrayE = { 0, 1, 2, 3, 4, 6, 7, 8 ,9 };
        int[] arrayF = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        Console.WriteLine($"{PrintableArray(arrayA)} -> {MissingNumber(arrayA)}");
        Console.WriteLine($"{PrintableArray(arrayB)} -> {MissingNumber(arrayB)}");
        Console.WriteLine($"{PrintableArray(arrayC)} -> {MissingNumber(arrayC)}");
        Console.WriteLine($"{PrintableArray(arrayD)} -> {MissingNumber(arrayD)}");
        Console.WriteLine($"{PrintableArray(arrayE)} -> {MissingNumber(arrayE)}");
        Console.WriteLine($"{PrintableArray(arrayF)} -> {MissingNumber(arrayF )}");

        Console.WriteLine("================================");
        Console.WriteLine("");
    }

    static void HW02_05()
    {
        // Найпростіше стиснення ланцюжка ДНК. Ланцюг ДНК у вигляді строки на
        // вхід (кожен нуклеотид представлений символом "A", "C", "G", "T").
        // Два методи, один для компресії, інший для декомпресії.

        byte[] DnaCompress(string dna)
        {
            if (dna.Length % 4 != 0)
            {
                Console.WriteLine("Malformed DNA string detected. Number of nucleotids must be a multiple of 4 value");
                return new byte[] { };
            }

            Dictionary<char, ushort> dnaMapping = new Dictionary<char, ushort>()
            {
                {'A', 0b00 },
                {'C', 0b01 },
                {'G', 0b10 },
                {'T', 0b11 },
            };

            int compressedLength = dna.Length / 4;
            byte[] compressedDna = new byte[compressedLength];

            byte compressedValue = 0;
            for (int i = 0; i < dna.Length; i++)
            {
                try
                {
                    char c = dna[i];
                    int shift = (3 - i % 4) * 2;
                    compressedValue = Convert.ToByte(compressedValue | dnaMapping[c] << shift);

                    string value_representation = Convert.ToString(dnaMapping[c], 2).PadLeft(8, '0');
                    string compressed_representation = Convert.ToString(compressedValue, 2).PadLeft(8, '0');

                    if (i % 4 == 3 || i == dna.Length - 1)
                    {
                        compressedDna[i / 4] = compressedValue;
                        compressedValue = 0;
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine("Malformed DNA string detected. Allowed symbols: A, C, G, T");
                    return new byte[] { };
                }
                
            }

            return compressedDna;
        }

        string DnaDecompress(byte[] compressedDna)
        {
            int decompressedLength = compressedDna.Length * 4;

            char[] decompressedDna = new char[decompressedLength];

            byte mask0 = 0b11000000;
            byte mask1 = 0b00110000;
            byte mask2 = 0b00001100;
            byte mask3 = 0b00000011;

            Dictionary<ushort, char> dnaReverseMapping = new Dictionary<ushort, char>()
            {
                {0b00, 'A'},
                {0b01, 'C'},
                {0b10, 'G'},
                {0b11, 'T'},
            };

            for (int i = 0; i < compressedDna.Length; i++)
            {
                
                byte chunk = compressedDna[i];

                ushort position0 = (ushort)((chunk & mask0) >> 6);
                ushort position1 = (ushort)((chunk & mask1) >> 4);
                ushort position2 = (ushort)((chunk & mask2) >> 2);
                ushort position3 = (ushort)((chunk & mask3));

                decompressedDna[i * 4] = dnaReverseMapping[position0];
                decompressedDna[i * 4 + 1] = dnaReverseMapping[position1];
                decompressedDna[i * 4 + 2] = dnaReverseMapping[position2];
                decompressedDna[i * 4 + 3] = dnaReverseMapping[position3];
            }

            return new string(decompressedDna);
        }

        Console.WriteLine("===============05===============");

        string dna = "ACGTGTTGAACGTTGA";
        byte[] compressed = DnaCompress(dna);
        string decompressed = DnaDecompress(compressed);

        Console.WriteLine(dna);
        Console.WriteLine(PrintableArray(compressed));
        Console.WriteLine(decompressed);

        Console.WriteLine("================================");
        Console.WriteLine("");
    }

    static void HW02_06()
    {
        // Симетричне шифрування. Є строка на вхід, який має бути зашифрований.
        // Ключ можна задати в коді або згенерувати та зберегти.
        // Два методи, шифрування та дешифрування.

        string EncryptString(string input, string key)
        {
            char[] encrypted = new char[input.Length];

            int controlSum = (from i in key select (int)i).Sum();

            for (int i = 0; i < input.Length; i++)
            {
                int inputLetterValue = (int)input[i];
                int keyLetterValue1 = (int)key[i % key.Length];
                int keyLetterValue2 = (int)key[key.Length - 1 - i % key.Length];

                int encodedValue = (inputLetterValue ^ keyLetterValue1) ^ keyLetterValue2;
                encrypted[i] = (char)(encodedValue ^ controlSum);
            }

            return new string(encrypted);
        }

        string DecryptString(string input, string key)
        {
            char[] decrypted = new char[input.Length];

            int controlSum = (from i in key select (int)i).Sum();

            for (int i = 0; i < input.Length; i++)
            {
                int inputLetterValue = (int)input[i] ^ controlSum;
                int keyLetterValue1 = (int)key[i % key.Length];
                int keyLetterValue2 = (int)key[key.Length - 1 - i % key.Length];

                int decodedValue = (inputLetterValue ^ keyLetterValue2) ^ keyLetterValue1;
                decrypted[i] = (char)decodedValue;
            }

            return new string(decrypted);
        }

        Console.WriteLine("===============06===============");

        string originalString = "Not all those who wander are lost. Not all those who wander are lost. Not all those who wander are lost. Not all those who wander are lost. ";
        string encryptionKey1 = "Why so serious?";
        string encryptionKey2 = "Why so serious!";
        string encryptedString = EncryptString(originalString, encryptionKey1);
        string decryptedString1 = DecryptString(encryptedString, encryptionKey2);
        string decryptedString2 = DecryptString(encryptedString, encryptionKey1);

        Console.WriteLine($"Original string:");
        Console.WriteLine(originalString);
        Console.WriteLine($"Encrypted string:");
        Console.WriteLine(encryptedString);
        Console.WriteLine($"Decrypted string (wrong key):");
        Console.WriteLine(decryptedString1);
        Console.WriteLine($"Decrypted string (right key):");
        Console.WriteLine(decryptedString2);

        Console.WriteLine("================================");
        Console.WriteLine("");

    }


    static void Main(string[] args)
    {
        //HW02_01();
        HW02_02();
        //HW02_03();
        //HW02_04();
        HW02_05();
        //HW02_06();
        Console.ReadLine();
    }
}

