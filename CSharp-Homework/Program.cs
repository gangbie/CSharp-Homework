namespace CSharp_Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataStructure.List<string> list = new DataStructure.List<string>();

            list.Add("1번 데이터");
            list.Add("2번 데이터");
            list.Add("3번 데이터");
            list.Add("4번 데이터");
            list.Add("5번 데이터");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0}, ", list[i]);
            }

            Console.WriteLine();
            string value;
            value = list[0];
            value = list[1];
            value = list[2];
            value = list[3];
            value = list[4];

            list[0] = "5번 데이터";
            list[1] = "4번 데이터";
            list[2] = "3번 데이터";
            list[3] = "2번 데이터";
            list[4] = "1번 데이터";
            
            for(int i=0; i<list.Count; i++)
            {
                Console.Write("{0}, ",list[i]);
            }

            Console.WriteLine();

            list.Remove("3번 데이터");
            list.Remove("2번 데이터");

            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0}, ", list[i]);
            }

            Console.WriteLine();

            string? findValue = list.Find(x => x.Contains('4'));
            Console.WriteLine(findValue);
            int findIndex = list.FindIndex(x => x.Contains('1'));
            Console.WriteLine(findIndex);
        }
    }
}