using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SicAssembler
{
    public partial class Assembler : Form
    {
        /**
         * Hashtable contains opcode for each mnemonic
         * Directives contains operations that will not generate machine code for
         */
        private Dictionary<string, int> OpTab = new Dictionary<string, int>();
        private List<string> Directives = new List<string>();
        /**
         * Assigning address for each instruction and get it's label if found
         * These two lists are used in Pass2 instead of re-reading Intermediate file again
         */
        List<string> Label = new List<string>();
        List<string> Address = new List<string>();
        List<string> Operation = new List<string>();
        List<string> Operand = new List<string>();
        /**
         * Start Address, End Address, Program Name (Label of Start instruction)
         */
        private int Start { get; set; }
        private int End { get; set; }
        /**
         * InstructionsFlagCounter is false when detecting ` J * ` instruction
         * that stops Length's incrementing
         */
        private int Length { get; set; }
        private string ProgramName { get; set; }
        private string path;
        private string Path
        {
            set
            {
                path = pathTextBox.Text.Substring(0, pathTextBox.Text.LastIndexOf(@"\")+1);
            }
            get
            {
                return path;
            }
        }
        private string LISFILE { get; set; }
        private string INTFILE { get; set; }
        private StringBuilder IntermediateFileData;
        private StringBuilder ListingFileData;
        private string IntData
        {
            get
            {
                return IntermediateFileData.ToString();
            }
        }
        private string LisData
        {
            get
            {
                return ListingFileData.ToString();
            }
        }
        public Assembler()
        {
            InitializeMachineInstructions();
            InitializeComponent();
        }
        private string ReadFile()
        {
            Path = pathTextBox.Text;
            if (!File.Exists(pathTextBox.Text)) return null;
            return File.ReadAllText(pathTextBox.Text);
        }
        //Substring(0, 9) Label
        //Substring(9, 8) Operation
        //Substring(17)   Operand
        private void ReadLabels()
        {
            StringReader sr = new StringReader(ReadFile());
            string line, OperationLine;
            bool IsComment = true;
            while (IsComment)
            {
                if ((line = sr.ReadLine()) != null && !line.StartsWith(".") && line != "")
                {
                    ProgramName = line.Substring(0, 9).Trim().ToUpper();
                    if (line.Substring(9, 8).Trim().ToUpper() != "START") return;
                    Start = toDecimal(line.Substring(17).Trim());
                    Label.Add(ProgramName);
                    Address.Add(Convert.ToString(Start));
                    Operation.Add(line.Substring(9, 8).Trim().ToUpper());
                    Operand.Add(line.Substring(17).Trim().ToUpper());
                    IsComment = false;
                }
            }
            int LOC = Start;
            bool InstructionsFlagCounter = true;

            while((line = sr.ReadLine()) != null)
            {
                IsComment = line.StartsWith(".") && line == "";
                if (IsComment) continue;
                OperationLine = line.Substring(9, 8).Trim().ToUpper();
                if(OperationLine == "J" && line.Substring(17).Trim() == "*") InstructionsFlagCounter = false;
                if (InstructionsFlagCounter) Length++;
                if (OperationLine != "END")
                {
                    Label.Add(line.Substring(0, 9).Trim().ToUpper());
                    Address.Add(Convert.ToString(LOC));
                    Operation.Add(OperationLine);
                    Operand.Add(line.Substring(17).Trim().ToUpper());
                }
                else
                {
                    bool flag = false;
                    for (int i = 0; i < Label.Count; i++)
                    {
                        if (line.Substring(17).Trim().ToUpper() == Label.ElementAt(i))
                        {
                            End = Convert.ToInt32(Address.ElementAt(i));
                            flag = true;
                            break;
                        }
                    }
                    if(!flag) End = toDecimal(line.Substring(17).Trim());
                    return;
                }
                if (OperationLine == "RESW")
                {
                    LOC += toDecimal(line.Substring(17).Trim()) * 3;
                }
                else if (OperationLine == "RESB")
                {
                    LOC += toDecimal(line.Substring(17).Trim());
                }
                else if (OperationLine == "WORD")
                {
                    LOC += 0x3;
                }
                else if (OperationLine == "BYTE")
                {
                    LOC += 0x1;
                }
                else
                {
                    LOC += 0x3;
                }
            }
        }
        private void Pass1()
        {
            ClearAll();
            ReadLabels();
            IntermediateFileData = new StringBuilder();
            ListingFileData = new StringBuilder("\t SYMBOL TABLE");
            ListingFileData.AppendLine();
            ListingFileData.AppendLine("\tName\tAddress");
            for (int i = 0; i < Label.Count; i++)
            {
                if (Label.ElementAt(i) != "")
                {
                    ListingFileData.AppendLine(string.Format("\t{0}\t{1}", Label.ElementAt(i), toHex(Address.ElementAt(i))));
                }
            }
            ListingFileData.AppendLine();
            ListingFileData.AppendLine("     p r o g r a m     l i s t i n g");
            StringReader sr = new StringReader(ReadFile());
            string line = sr.ReadLine();
            while (line.StartsWith("."))
            {
                line = sr.ReadLine();
            }
            int x = 1;
            ListingFileData.AppendLine(string.Format("{0}\t{1}", toHex(Start), line));
            IntermediateFileData.AppendLine(string.Format("{0}\t{1}", toHex(Start), line));
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith(".")) continue;
                if (x < Operation.Count && Operation.ElementAt(x) != "END")
                {
                    ListingFileData.AppendLine(string.Format("{0}\t{1}", toHex(Address.ElementAt(x)), line));
                    IntermediateFileData.AppendLine(string.Format("{0}\t{1}", toHex(Address.ElementAt(x)), line));
                    x++;
                }
                else
                {
                    End = Convert.ToInt32(Address.ElementAt(x - 1)) + 3;
                    ListingFileData.AppendLine(string.Format("{0}\t{1}", toHex(End), line));
                    IntermediateFileData.AppendLine(string.Format("{0}\t{1}", toHex(End), line));
                }

            }
            ListingFileData.AppendLine("  s u c c e s s f u l    a s s e m b l y");
            ListingFileData.Append("e  n  d      o  f      p  r  o  g  r  a  m");
            textBoxPass1.Text = ListingFileData.ToString();
            LISFILE = string.Format("{0}LISFILE.txt", Path);
            if (File.Exists(LISFILE)) File.Delete(LISFILE);
            File.WriteAllText(LISFILE, ListingFileData.ToString());
            INTFILE = string.Format("{0}INTFILE.txt", Path);
            if (File.Exists(INTFILE)) File.Delete(INTFILE);
            File.WriteAllText(INTFILE, IntermediateFileData.ToString());
        }
        private void InitializeBrowseDialog()
        {
            Browse.Filter = "Text File (*.txt)|*.txt";
            Browse.FileName = "src.txt";
            Browse.Title = "Select your source code file";
            Browse.Multiselect = false;
        }
        private void browseButton_Click(object sender, EventArgs e)
        {
            InitializeBrowseDialog();
            DialogResult result = Browse.ShowDialog();
            if (result == DialogResult.Cancel) return;
            else pathTextBox.Text = Browse.FileName;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Pass1();
            Pass2();
        }
        private string AddZeroBits(int NumOfBits, string value)
        {
            StringBuilder Value = new StringBuilder(value);
            while(Value.Length < NumOfBits)
            {
                Value.Insert(0, "0");
            }
            return Value.ToString();
        }
        private string toHex(int fromDecimal)
        {
            return AddZeroBits(5, string.Format("{0:X}", fromDecimal));
        }
        private string toHex(string fromDecimal)
        {
            return AddZeroBits(5, string.Format("{0:X}", Convert.ToInt32(fromDecimal)));
        }
        private string toHexNoZeroMostBits(int fromDecimal)
        {
            return string.Format("{0:X}", fromDecimal);
        }
        private string toHexNoZeroMostBits(string fromDecimal)
        {
            return string.Format("{0:X}", Convert.ToInt32(fromDecimal));
        }
        private int toDecimal(string fromHex)
        {
            return Convert.ToInt32(fromHex, 16);
        }
        private void ClearAll()
        {
            Start = 0;
            End = 0;
            Label.Clear();
            Address.Clear();
            ProgramName = "";
        }
        /////////////////////////////END OF PASS1\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        private void Pass2()
        {
            StringReader sr = new StringReader(IntData);
            List<string> MachineCode = new List<string>();
            for (int i = 0; i < Operation.Count; i++)
            {
                if (Directives.Contains(Operand[i]))
                {
                    MachineCode.Add("-1");
                    continue;
                }
                if (!Operand[i].Contains(",X"))
                {
                    if (Label.Contains(Operand[i]))
                    {
                        int a = Label.IndexOf(Operand[i]);
                        MachineCode.Add(string.Format("{0}{1}", AddZeroBits(2, toHexNoZeroMostBits(OpTab[Operation[i]])), AddZeroBits(4, toHexNoZeroMostBits(Address[a]))));
                    }
                }
                else
                {
                    
                }
            }
            MachineCode.ToString();
            //StringBuilder OBJFILE = new StringBuilder();
            //OBJFILE.AppendLine(string.Format("H|{0}|{1:X}|{2}", ProgramName, Start, Length));
            //for (int i = 0; i < Label.Count; i++)
            //{

            //}
        }
        /////////////////////////////END OF PASS2\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        private void InitializeMachineDirectives()
        {
            //Directives:- start end resw resb word byte base nobase equ org ltorg
            Directives.Add("START");
            Directives.Add("END");
            Directives.Add("RESW");
            Directives.Add("RESB");
            Directives.Add("WORD");
            Directives.Add("BYTE");
            Directives.Add("BASE");
            Directives.Add("NOBASE");
            Directives.Add("EQU");
            Directives.Add("ORG");
            Directives.Add("LTORG");
        }
        private void InitializeMachineInstructions()
        {
            OpTab.Add("ADD", 0x18);
            OpTab.Add("AND", 0x40);
            OpTab.Add("COMP", 0x28);
            OpTab.Add("DIV", 0x24);
            OpTab.Add("J", 0x3C);
            OpTab.Add("JEQ", 0x30);
            OpTab.Add("JGT", 0x34);
            OpTab.Add("JLT", 0x38);
            OpTab.Add("JSUB", 0x48);
            OpTab.Add("LDA", 0x00);
            OpTab.Add("LDCH", 0x50);
            OpTab.Add("LDL", 0x08);
            OpTab.Add("LDX", 0x04);
            OpTab.Add("MUL", 0x20);
            OpTab.Add("OR", 0x44);
            OpTab.Add("RD", 0xD8);
            OpTab.Add("RSUB", 0x4C);
            OpTab.Add("STA", 0x0C);
            OpTab.Add("STCH", 0x54);
            OpTab.Add("STL", 0x14);
            OpTab.Add("STSW", 0xE8);
            OpTab.Add("STX", 0x10);
            OpTab.Add("SUB", 0x1C);
            OpTab.Add("TD", 0xE0);
            OpTab.Add("TIX", 0x2C);
            OpTab.Add("WD", 0xDC);
        }
    }
}
