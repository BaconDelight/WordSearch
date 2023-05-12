using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordSearch
{
    // enum Direction { North = 1, North_East, East, South_East, South, South_West, West, North_West };
    enum Direction { South = 1, South_East, East, North_East, North, North_West, West, South_West };

    public partial class Form1 : Form
    {
        string version = "v1.1";
        static Random r;
        List<Answer> objAnswers = new List<Answer>();
        int Across = 0;
        int Down = 0;
        StreamWriter logFile = null;

        public Form1()
        {
            InitializeComponent();
            this.Text = "WordSearch " + version;
            r = new Random();
            //logFile.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                txtFile.Text = openFileDialog1.FileName;
            }
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            // Make Array to hold Square
            Across = (int)numAcross.Value;
            Down = (int)numDown.Value;

            char[,] Square = new char[Across, Down];

            for (int i = 0; i < Across; i++)
            {
                for (int j = 0; j < Down; j++)
                {
                    Square[i, j] = ' ';
                }
            }


            string TextWords = txtWords.Text.Trim();

            // Read in file of words
            if (txtFile.Text.Length != 0)
            {
                StreamReader sr = new StreamReader(txtFile.Text);
                TextWords += "\r\n" + sr.ReadToEnd().Trim();
                TextWords = TextWords.Trim();
                sr.Close();
            }

            string[] Words = TextWords.Replace("\r\n", "\n").Split('\n'); ;


            // Sort in Ascending order by length
            Array.Sort(Words, delegate (string word1, string word2)
            {
                return word1.Length.CompareTo(word2.Length);
            });

            // Reverse so sorted in Descending order by length
            Array.Reverse(Words);

            // Clear List<Answer>
            objAnswers.Clear();

            for (int i = 0; i < Words.Length; i++)
            {
                Words[i] = Words[i].Trim().ToUpper();

                if (Words[i].Replace(" ", "") != Words[i])
                {
                    MessageBox.Show("Words cannot have embedded spaces");
                }
            }

            // Now attempt to fit the words
            int numTries = 0;
            int maxTries = 10;
            bool AllFound = false;

            while (numTries < maxTries && !AllFound)
            {
                bool wordFound = true;

                for (int wordIndex = 0; wordIndex < Words.Length && wordFound; wordIndex++)
                {
                    wordFound = false;

                    string Word = Words[wordIndex];

                    WriteLog("");
                    WriteLog("Word: " + Word);
                    WriteLog("".PadLeft(Word.Length + 6, '='));

                    // Randomise letters of each word
                    int[][] letters = new int[Word.Length][];

                    for (int letterIndex = 0; letterIndex < Word.Length; letterIndex++)
                    {
                        letters[letterIndex] = new int[2] { Word[letterIndex], r.Next(0, Word.Length * 100) };
                    }

                    Sort<int>(letters, 1);


                    for (int letterIndex = 0; letterIndex < Word.Length && !wordFound; letterIndex++)
                    {
                        wordFound = FindMatch(ref Square, Word, letterIndex, (char)letters[letterIndex][0]);
                    }

                    if (!wordFound)
                    {
                        wordFound = FindMatch(ref Square, Word, 0, ' ');
                    }

                    LogSquare(Across, Down, Square, objAnswers);
                }


                if (wordFound)
                {
                    AllFound = true;
                }

                numTries++;
            }

            if (AllFound)
            {
                objAnswers.Sort((a, b) => a.Word.CompareTo(b.Word));
                PrintSquare(Across, Down, Square, objAnswers, true);
                PrintSquare(Across, Down, Square, objAnswers, false);
                LogSquare(Across, Down, Square, objAnswers);
                MessageBox.Show("WordSearch Puzzle Made");
            }
            else
            {
                MessageBox.Show("Cannot fit all the words\nTry less words or bigger Square");
            }
        }

        bool FindMatch(ref char[,] Square, string Word, int letterIndex, char targetChar)
        {
            WriteLog("");
            WriteLog("letterIndex: " + letterIndex.ToString());
            WriteLog("targetChar: " + targetChar.ToString());

            // Make a list of all the squares that match the targetChar
            int[][] candidateSquares = new int[Across * Down][];
            int numSquares = 0;

            for (int i = 0; i < Across; i++)
            {
                for (int j = 0; j < Down; j++)
                {
                    if (Square[i, j] == targetChar)
                    {
                        candidateSquares[numSquares++] = new int[3] { i, j, r.Next(0, Across * Down * 100) };
                    }
                }
            }

            // Simply return early if no matching squares
            if (numSquares == 0)
            {
                return false;
            }

            // Resize candidateSquares Array to trim unused tailing values
            Array.Resize<int[]>(ref candidateSquares, numSquares);

            // Sort by Random Number so Randomising order
            Sort<int>(candidateSquares, 2);


            // Make and randomise the list of possible directions
            int[][] PossibleDirections = new int[Enum.GetNames(typeof(Direction)).Length][];

            for (int i = 0; i < Enum.GetNames(typeof(Direction)).Length; i++)
            {
                PossibleDirections[i] = new int[2] { i + 1, r.Next(0, 8 * 100) };
            }

            // Sort Possible Directions by Random Number so Randomising order
            Sort<int>(PossibleDirections, 1);


            bool wordFound = false;

            // Process each candidate square and see if we can fit the word in any direction
            for (int candidateSquaresIndex = 0; candidateSquaresIndex < candidateSquares.Length && !wordFound; candidateSquaresIndex++)
            {
                int[] TargetSquare = new int[2] { candidateSquares[candidateSquaresIndex][0], candidateSquares[candidateSquaresIndex][1] };

                // Now try to find a direction in which to fit the word
                for (int directionIndex = 0; directionIndex < PossibleDirections.Length && !wordFound; directionIndex++)
                {
                    int directionAttempt = PossibleDirections[directionIndex][0];

                    WriteLog("Direction: " + Enum.GetName(typeof(Direction), directionAttempt).Replace("_", "-"));

                    int x = TargetSquare[0];
                    int y = TargetSquare[1];
                    WriteLog("Start at " + x.ToString() + "," + y.ToString());

                    // Step back to where Word would need to start
                    for (int i = 0; i < letterIndex; i++)
                    {
                        DirectionDecrement(ref x, ref y, directionAttempt);
                        WriteLog("Decrement to " + x.ToString() + "," + y.ToString());
                    }

                    Debug.Print("x=" + x.ToString());
                    Debug.Print("y=" + y.ToString());

                    bool attemptFailed = false;

                    // Is x or y out of bounds to start with?
                    if (x < 0 || y < 0 || x >= Across || y >= Down)
                    {
                        attemptFailed = true;
                    }

                    int xStart = x;
                    int yStart = y;

                    for (int Index = 0; Index < Word.Length && !attemptFailed; Index++)
                    {
                        // Increment according to direction
                        if (Index != 0)
                        {
                            DirectionIncrement(ref x, ref y, directionAttempt);
                            WriteLog("Increment to " + x.ToString() + "," + y.ToString());
                        }

                        // Is x or y out of bounds?
                        if (x < 0 || y < 0 || x >= Across || y >= Down)
                        {
                            attemptFailed = true;
                        }
                        else
                        {
                            //Debug.Print("Square[x, y] '" + Square[x, y].ToString() + "'");
                            //Debug.Print("Word[i] '" + Word.ToCharArray()[letterIndex].ToString() + "'");
                            //Debug.Print("");

                            // Is Square in use? 
                            WriteLog("Square[" + x.ToString() + ", " + y.ToString() + "] = '" + Square[x, y].ToString() + "'");

                            if (Square[x, y] != ' ' && Square[x, y] != Word.ToCharArray()[Index])
                            {
                                attemptFailed = true;
                            }
                        }
                    }

                    if (!attemptFailed)
                    {
                        // We have succeeded!
                        wordFound = true;
                        objAnswers.Add(new Answer { Word = Word, x = xStart, y = Down - yStart - 1, Direction = directionAttempt });
                        PlaceWord(ref Square, Word, xStart, yStart, directionAttempt);
                    }
                }
            }

            return wordFound;
        }


        void PrintSquare(int Across, int Down, char[,] Square, List<Answer> objAnswers, bool bAnswers)
        {
            string Filename = "";

            if (txtTitle.Text != "")
            {
                Filename = txtTitle.Text;
            }
            else
            {
                Filename = "WordSearch Puzzle";
            }

            if (bAnswers)
            {
                Filename += " Answers";
            }

            StreamWriter swText = new StreamWriter(Filename + ".txt");
            StreamWriter swHTML = new StreamWriter(Filename + ".html");

            swText.WriteLine("");
            swHTML.WriteLine("");

            swHTML.WriteLine("<html>");
            swHTML.WriteLine("<head>");
            swHTML.WriteLine("  <style>");
            swHTML.WriteLine("    table, th, td");
            swHTML.WriteLine("    {");
            swHTML.WriteLine("      border: 1px solid black;");
            swHTML.WriteLine("      border-collapse: collapse;");
            swHTML.WriteLine("      padding: 15px;");
            swHTML.WriteLine("      spacing: 5px;");
            swHTML.WriteLine("      font-size: 30px;");
            swHTML.WriteLine("      font-family: Arial, Helvetica, sans-serif;");
            swHTML.WriteLine("    }");
            swHTML.WriteLine("  </style>");
            swHTML.WriteLine("</head>");
            swHTML.WriteLine("<body>");
            swHTML.WriteLine("<br/>");


            if (txtTitle.Text != "")
            {
                swText.WriteLine(txtTitle.Text);
                swText.WriteLine("".PadRight(txtTitle.Text.Length, '='));
                swText.WriteLine("");

                swHTML.WriteLine("<h2>" + txtTitle.Text + "</h2>");
            }

            swHTML.WriteLine("<br/>");
            swHTML.WriteLine("<table>");

            for (int i = 0; i < Down; i++)
            {
                string textLine = "";
                swHTML.WriteLine("  <tr>");

                for (int j = 0; j < Across; j++)
                {
                    string ch = Square[j, i].ToString();

                    if (ch == " ")
                    {
                        if (bAnswers)
                        {
                            ch = "*";
                        }
                        else
                        {
                            ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(r.Next(0, 25), 1);
                        }
                    }
                    textLine += (ch + ' ');
                    swHTML.WriteLine("  <td>" + ch + "</td>");
                }

                swText.WriteLine(textLine);
                swHTML.WriteLine("  </tr>");
            }

            swText.WriteLine("");
            swHTML.WriteLine("<table>");

            // Calculate size of largest Word
            int maxWordLength = 0;

            foreach (var answer in objAnswers)
            {
                maxWordLength = Math.Max(maxWordLength, answer.Word.Length);
            }

            swHTML.WriteLine("<br/>");
            swHTML.WriteLine("<br/>");
            swHTML.WriteLine("<table>");

            foreach (var answer in objAnswers)
            {
                swHTML.WriteLine("  <tr>");

                if (bAnswers)
                {
                    swText.WriteLine(answer.Word.PadRight(maxWordLength) + " " + (answer.x + 1).ToString().PadLeft(4) + "," + (answer.y + 1).ToString().PadRight(4) + " " + Enum.GetName(typeof(Direction), answer.Direction).Replace("_", "-"));

                    swHTML.WriteLine("  <td>" + answer.Word + "</td>");
                    swHTML.WriteLine("  <td>" + (answer.x + 1).ToString() + "," +  (answer.y + 1).ToString() + "</td>");
                    swHTML.WriteLine("  <td>" + Enum.GetName(typeof(Direction), answer.Direction).Replace("_", " - ") + "</td>");
                }
                else
                {
                    swText.WriteLine(answer.Word);
                    swHTML.WriteLine("  <td>" + answer.Word + "</td>");
                }

                swHTML.WriteLine("  </tr>");
            }

            swHTML.WriteLine("</table>");


            swText.WriteLine("");
            swText.WriteLine("       N");
            swText.WriteLine("   NW  |  NE");
            swText.WriteLine("     \\ | / ");
            swText.WriteLine("  W ------- E");
            swText.WriteLine("     / | \\");
            swText.WriteLine("   SW  |  SE");
            swText.WriteLine("       S");

            swText.WriteLine("");
            swHTML.WriteLine("<br/>");
            swHTML.WriteLine("<br/>");
            swHTML.WriteLine("<br/>");

            swText.WriteLine("Produced " + DateTime.Now.ToString("ddd dd-MMM-yy @ HH:mm:ss"));
            swHTML.WriteLine("Produced " + DateTime.Now.ToString("ddd dd - MMM - yy @ HH: mm:ss") + "<br/>");

            if (bAnswers)
            {
                swText.WriteLine("");
                swText.WriteLine("Program: WordSearch" + version);
                swText.WriteLine("Written by Peter Norris");

                swHTML.WriteLine("<br/>");
                swHTML.WriteLine("Program: WordSearch " + version + "<br/>");
                swHTML.WriteLine("Written by Peter Norris<br/>");
            }

            swText.Close();
            swHTML.Close();
        }

        void LogSquare(int Across, int Down, char[,] Square, List<Answer> objAnswers)
        {
            WriteLog("");
            WriteLog("======================================");

            for (int i = 0; i < Down; i++)
            {
                string line = "";

                for (int j = 0; j < Across; j++)
                {
                    string ch = Square[j, i].ToString();

                    if (ch == " ")
                    {
                        ch = "*";
                    }
                    line += (ch + ' ');
                }

                WriteLog(line);
            }

            WriteLog("");

            // Calculate size of largest Word
            int maxWordLength = 0;

            foreach (var answer in objAnswers)
            {
                maxWordLength = Math.Max(maxWordLength, answer.Word.Length);
            }

            foreach (var answer in objAnswers)
            {
                WriteLog(answer.Word.PadRight(maxWordLength) + " " + (answer.x + 1).ToString().PadLeft(4) + "," + (answer.y + 1).ToString().PadRight(4) + " " + Enum.GetName(typeof(Direction), answer.Direction).Replace("_", "-"));
            }
        }

        // moves x and y one step in the direction to the value of Direction
        void DirectionIncrement(ref int x, ref int y, int Direction)
        {
            switch (Direction)
            {
                case 1:
                    y++;
                    break;
                case 2:
                    x++;
                    y++;
                    break;
                case 3:
                    x++;
                    break;
                case 4:
                    x++;
                    y--;
                    break;
                case 5:
                    y--;
                    break;
                case 6:
                    x--;
                    y--;
                    break;
                case 7:
                    x--;
                    break;
                case 8:
                    x--;
                    y++;
                    break;
            }
        }

        // This moves x and y in the opposite direction to the value of Direction
        void DirectionDecrement(ref int x, ref int y, int Direction)
        {
            switch (Direction)
            {
                case 1:
                    y--;
                    break;
                case 2:
                    x--;
                    y--;
                    break;
                case 3:
                    x--;
                    break;
                case 4:
                    x--;
                    y++;
                    break;
                case 5:
                    y++;
                    break;
                case 6:
                    x++;
                    y++;
                    break;
                case 7:
                    x++;
                    break;
                case 8:
                    x++;
                    y--;
                    break;
            }
        }

        private static void Sort<T>(T[][] data, int col)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            Array.Sort<T[]>(data, (x, y) => comparer.Compare(x[col], y[col]));
        }

        void PlaceWord(ref char[,] Square, string Word, int x, int y, int Direction)
        {
            for (int letterIndex = 0; letterIndex < Word.Length; letterIndex++)
            {
                Square[x, y] = Word.ToCharArray()[letterIndex];

                // Increment according to direction
                DirectionIncrement(ref x, ref y, Direction);
            }
        }
        public class Answer : IEquatable<Answer>
        {
            public string Word { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int Direction { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Answer objAsAnswer = obj as Answer;
                if (objAsAnswer == null) return false;
                else return Equals(objAsAnswer);
            }
            public override int GetHashCode()
            {
                return Word.Length;
            }
            public bool Equals(Answer other)
            {
                if (other == null) return false;
                return (this.Word.Equals(other.Word) && this.x.Equals(other.x) && this.y.Equals(other.y) && this.Direction.Equals(other.Direction));
            }
            // Should also override == and != operators.
        }

        void WriteLog(string line)
        {
            return;

            if (logFile == null)
            {
                logFile = new StreamWriter("WordSearch" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".log");
            }

            if (line == "")
            {
                logFile.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
            }
            else
            {
                logFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + line);
            }

            logFile.Flush();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

