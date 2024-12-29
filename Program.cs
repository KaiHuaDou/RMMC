namespace RMMC;

public class Program
{
    private static Dictionary<string, double> elements = new( )
    {
        { "H",  1 },
        { "He", 4 },
        { "Li", 7 },
        { "Be", 9 },
        { "B",  11 },
        { "C",  12 },
        { "N",  14 },
        { "O",  16 },
        { "F",  19 },
        { "Ne", 20 },
        { "Na", 23 },
        { "Mg", 24 },
        { "Al", 27 },
        { "Si", 28 },
        { "P",  31 },
        { "S",  32 },
        { "Cl", 35.5 },
        { "Ar", 40 },
        { "K",  39 },
        { "Ca", 40 },
        { "Sc", 45 },
        { "Ti", 48 },
        { "V",  51 },
        { "Cr", 52 },
        { "Mn", 55 },
        { "Fe", 56 },
        { "Co", 59 },
        { "Ni", 59 },
        { "Cu", 64 },
        { "Zn", 65 },
        { "Ga", 70 },
        { "Ge", 73 },
        { "As", 75 },
        { "Se", 79 },
        { "Br", 80 },
        { "Kr", 84 },
        { "Rb", 85.5 },
        { "Sr", 88 },
        { "Y",  89 },
        { "Zr", 91 },
        { "Nb", 93 },
        { "Mo", 96 },
        { "Tc", 98 },
        { "Ru", 101 },
        { "Rh", 103 },
        { "Ag", 108 },
        { "In", 115 },
        { "Sn", 119 },
        { "Sb", 122 },
        { "I",  127 },
        { "Xe", 131 },
        { "Cs", 133 },
        { "Ba", 137 },
        { "La", 139 },
        { "Ce", 140 },
        { "Pr", 141 },
        { "Nd", 144 },
        { "Pm", 145 },
        { "Sm", 150 },
        { "Eu", 152 },
        { "Gd", 157 },
        { "Tb", 159 },
        { "Ho", 165 },
        { "Er", 167 },
        { "Tm", 169 },
        { "Yb", 173 },
        { "Lu", 175 },
        { "Ta", 181 },
        { "W",  184 },
        { "Re", 186 },
        { "Os", 190 },
        { "Ir", 192 },
        { "Pt", 195 },
        { "Au", 197 },
        { "Hg", 200 },
    };

    private static Dictionary<char, char> brackets = new( )
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
        { '（', '）' },
        { '【', '】' },
        { '「', '」' },
    };

    private static double GetWeight(string element)
    {
        return elements.TryGetValue(element, out double w) ? w : 0;
    }

    private static double Calculate(string formula)
    {
        double result = 0;
        double current = -1;

        for (int i = 0; i < formula.Length;)
        {
            int offset = 0;
            if (char.IsUpper(formula[i]))
            {
                if (current != -1)
                    result += current;
                while (i + offset + 1 < formula.Length && char.IsLower(formula[i + offset + 1]))
                    offset++;
                current = GetWeight(formula.Substring(i, offset + 1));
                i += offset + 1;
            }
            else if (brackets.TryGetValue(formula[i], out char pair))
            {
                if (current != -1)
                    result += current;
                for (; i + offset < formula.Length; offset++)
                {
                    if (formula[i + offset] == pair)
                    {
                        current = Calculate(formula.Substring(i + 1, offset - 1));
                        break;
                    }
                }
                i += offset + 1;
            }
            else if (char.IsDigit(formula[i]))
            {
                int coefficient = 0;
                for (; i + offset < formula.Length && char.IsDigit(formula[i + offset]); i++)
                {
                    coefficient = 10 * coefficient + (formula[i + offset] - '0');
                }
                result += current * (coefficient == 0 ? 1 : coefficient);
                current = -1;
            }
            else
            {
                return 0;
            }
        }
        if (current != -1)
            result += current;
        return result;
    }

    private static void Main( )
    {
        Console.WriteLine("相对分子质量计算器");
        Console.WriteLine("[输入 clear 以清屏]");
        Console.WriteLine("[输入 exit 以退出]");
        Console.WriteLine("----------------------------------------");
        while (true)
        {
            Console.Write("化学式: ");
            string Formula = Console.ReadLine( ) ?? "";

            if (string.IsNullOrEmpty(Formula))
            {
                continue;
            }
            else if (Formula == "clear")
            {
                Console.Clear( );
                continue;
            }
            else if (Formula == "exit")
            {
                break;
            }

            int Coefficient = 0;
            int i = 0;

            for (; i < Formula.Length && char.IsDigit(Formula[i]); i++)
            {
                Coefficient = 10 * Coefficient + (Formula[i] - '0');
            }
            Coefficient = Coefficient == 0 ? 1 : Coefficient;

            double Result = Calculate(Formula[i..]);
            Console.WriteLine(Result == 0 ? "输入错误" : $"相对分子质量: {Result * Coefficient}");
        }
    }
}
