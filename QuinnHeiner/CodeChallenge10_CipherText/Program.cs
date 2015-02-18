using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
WEEK 10 Challenge:

I recently watched the movie "The Imitation Game" and it got me thinking about cyphers and codes and stuff.  Therefore, this weeks 
code challenge has to do with that topic.
 
A keyword transposition cipher is a method of choosing a monoalphabetic substitution with which to encode a message. 
 The substitution alphabet is determined by choosing a keyword, arranging the remaining letters of the alphabet in columns below the letters of the keyword, and then reading back the columns in the alphabetical order of the letters of the keyword.
 
For instance, if one chose the keyword SECRET, the columns generated would look like the following diagram. 
 Note how the letters in the keyword are skipped when laying out the columns, and duplicate letters are removed from the keyword:
 
SECRT
ABDFG
HIJKL
MNOPQ
UVWXY
Z
Since the alphabetical order of the characters in the keyword is CERST, the columns are then read back in that order to get the substitution alphabet.
 
Original:     ABCDE FGHIJ KLMNO PQRSTU VWXYZ
Substitution: CDJOW EBINV RFKPX SAHMUZ TGLQY
 
Task
 
Given a piece of ciphertext and the keyword used to encipher it with the keyword transposition cipher described above, 
write an algorithm to output the original message.
 
Input Format
You will pass in 2 inputs.  The first contains the keyword, and the second contains the ciphertext. The keyword will be at 
 most 7 characters long, and the ciphertext will be at most 255 characters in length.
 
Output Format
 
Your output should be the decoded version of the cipher text for each test case, one per line.
 
Sample Input
 
SPORT
LDXTW KXDTL NBSFX BFOII LNBHG ODDWN BWK
 
Sample Output
 
ILOVE SOLVI NGPRO GRAMM INGCH ALLEN GES
 
Sample Input 
 
SECRET
JHQSU XFXBQ
 
Sample Output
 
CRYPT OLOGY
*/
namespace CodeChallenge10_CipherText
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input;
            do
            {
                Console.WriteLine("\n\nEnter a keyword for the ciphertext: ");
                input = Console.ReadLine().ToUpper();
                var keyword = Regex.Replace(input, @"[^A-Za-z]+", "");

                Console.WriteLine("\nEnter the cipertext you want to decode (q to quit): ");
                input = Console.ReadLine().ToUpper();
                var ciphertext = Regex.Replace(input, @"[^A-Za-z]+", "");

                var decodedText = DecodeCipherText(keyword, ciphertext);
                Console.WriteLine(decodedText);

            } while (input != "Q");
        }

        public static string DecodeCipherText(string keyword, string ciphertext)
        {
            if (keyword.Length < 1 || ciphertext.Length < 1) {
                return "";
            }
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var substitutionMatrix = BuildSubstitutionMatrix(keyword, alphabet);
            var substitutionAlphabet = GetOrderedSubstitutionAlphabet(substitutionMatrix, keyword);

            var decodedString = new StringBuilder();
            for (var i = 0; i < ciphertext.Length; i++)
            {
                var encodedLetterIndex = substitutionAlphabet.IndexOf(ciphertext[i]);
                var decodedLetter = alphabet[encodedLetterIndex];
                if (i != 0 && i % 5 == 0)
                {
                    decodedString.Append(" ");
                }
                decodedString.Append(decodedLetter);
            }
            return String.Format("{0}", decodedString);
        }

        // e.g. the keyword SECRET becomes:
        // SECRT
        // ABDFG
        // HIJKL
        // MNOPQ
        // UVWXY
        // Z
        private static char[,] BuildSubstitutionMatrix(string keyword, string alphabet)
        {
            var keywordLetters = keyword.Distinct().ToArray();
            var alphabetNoKeywordLetters = String.Concat(alphabet.Split(keywordLetters, StringSplitOptions.RemoveEmptyEntries));
            var rowCount = (int)Math.Ceiling(((decimal)alphabet.Length / keywordLetters.Length));

            var substitutionMatrix = new char[rowCount, keywordLetters.Length];
            var substitutionAlphabet = String.Concat(new String(keywordLetters), alphabetNoKeywordLetters);

            var currentLetterIndex = 0;
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < keywordLetters.Length; col++)
                {
                    if (currentLetterIndex < substitutionAlphabet.Length)
                    {
                        substitutionMatrix[row, col] = substitutionAlphabet[currentLetterIndex];
                        currentLetterIndex++;
                    }
                }
            }

            return substitutionMatrix;
        }

        // e.g. for keyword SECRET based on the matrix returned from BuildSubstitutionMatrix() function
        //Original:     ABCDE FGHIJ KLMNO PQRSTU VWXYZ
        //Substitution: CDJOW EBINV RFKPX SAHMUZ TGLQY
        private static string GetOrderedSubstitutionAlphabet(char[,] matrix, string keyword)
        {
            var keywordLetters = keyword.Distinct().ToArray();
            var orderedSubstitutionAlphabetBuilder = new StringBuilder();
            var sortedKeywordLetters = keywordLetters.OrderBy(l => l).ToList();
            var columnIndices = new int[keywordLetters.Length];
            for (var i = 0; i < keywordLetters.Length; i++)
            {
                columnIndices[i] = Array.IndexOf(keywordLetters, sortedKeywordLetters[i]);
            }

            foreach (var colIndex in columnIndices)
            {
                GetColumnLetters(matrix, colIndex, orderedSubstitutionAlphabetBuilder);
            }
            var orderedSubstitutionAlphabet = orderedSubstitutionAlphabetBuilder.ToString();
            return orderedSubstitutionAlphabet;
        }

        // e.g. given the matrix below, colIndex of 0 would return the following: SAHMUZ
        // SECRT
        // ABDFG
        // HIJKL
        // MNOPQ
        // UVWXY
        // Z
        private static StringBuilder GetColumnLetters(char[,] matrix, int colIndex, StringBuilder letters)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var letter = matrix[row, colIndex];
                if (letter != 0)
                {
                    letters.Append(letter);
                }
            }
            return letters;
        }
    }
}
