using System;
using System.IO;

namespace BT2_LTDT
{
    public struct maTranKe
    {
        public int n;
        public int start;
        public int goal;
        public bool[] viengTham;
        public int[] dinhCha;
        public int[,] maTran;
    }
    class Program
    {
        //ghi dữ liệu từ file vào AM
        public static maTranKe maTran(string filename)
        {
            maTranKe AM;
            string[] a = File.ReadAllLines(filename);
            AM.n = int.Parse(a[0]);
            AM.maTran = new int[AM.n, AM.n];
            int b = 0;
            string[] c = a[1].Split(' ');
            AM.start = int.Parse(c[0]);
            AM.goal = int.Parse(c[1]);
            for (int i = 2; i < a.Length; i++)
            {
                string[] d = a[i].Split(' ');
                for (int j = 0; j < d.Length; j++)
                {
                    AM.maTran[b, j] = int.Parse(d[j]);
                }
                b++;
            }
            AM.viengTham = new bool[AM.n];
            AM.dinhCha = new int[AM.n];
            for (int i = 0; i < AM.n; i++)
            {
                AM.dinhCha[i] = -1;
            }
            return AM;
        }
        public static void DFS(maTranKe AM, int u)
        {
            if (AM.viengTham[u] == false)
            {
                AM.viengTham[u] = true;
                Console.Write($"{u} ");
            }
            if (u != AM.goal)
            {
                int v = -1;
                for (int i = 0; i < AM.n; i++)
                {
                    if (AM.viengTham[i] == false && AM.maTran[u, i] == 1)
                    {
                        v = i;
                        break;
                    }
                }
                if (v != -1)
                {
                    AM.dinhCha[v] = u;
                    DFS(AM, v);
                }
                else
                {
                    bool a = false;
                    //v = -1 có 2 trường hợp hết đường hoặc không liên thông
                    for (int i = 0; i < AM.n; i++)
                    {
                        if (AM.viengTham[i] == true)
                        {
                            for (int j = 0; j < AM.n; j++)
                            {
                                if (AM.viengTham[j] == false)
                                {   //nếu vẫn còn đường đi từ đỉnh đã duyệt đến đỉnh chưa duyệt thì lùi đỉnh -> u - 1
                                    if (AM.maTran[i, j] == 1)
                                    {
                                        a = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (a)
                    {
                        DFS(AM, u - 1);
                    }
                }
            }
        }
        public static void duongDi(maTranKe AM)
        {
            if (AM.viengTham[AM.goal] == false)
            {
                Console.WriteLine("Không có đường đi");
            }
            else
            {
                Console.WriteLine("Đường đi in kiểu ngược:");
                int i = AM.goal;
                Console.Write(AM.goal);
                while (i != AM.start)
                {
                    if (AM.dinhCha[i] != -1)
                    {
                        Console.Write($" <- {AM.dinhCha[i]}");
                        i = AM.dinhCha[i];
                    }
                }
            }
        }
        public static void BFS(maTranKe AM, int u)
        {
            //dùng mảng thay cho queue, dauHangDoi dùng để duyệt phần tử đầu hàng đợi
            int[] hangDoi = new int[AM.n];
            int dauHangDoi = 0;
            int v = 0;
            hangDoi[v] = u;
            AM.viengTham[u] = true;
            Console.Write($"{u} ");
            while (true)
            {
                if (AM.viengTham[AM.goal] == true)
                {
                    break;
                }
                else
                {
                    int a = hangDoi[v];
                    v++;
                    for (int i = 0; i < AM.n; i++)
                    {
                        if (AM.viengTham[i] == false && AM.maTran[a, i] == 1)
                        {
                            dauHangDoi++;
                            hangDoi[dauHangDoi] = i;
                            AM.viengTham[i] = true;
                            Console.Write($"{i} ");
                            AM.dinhCha[i] = a;
                            if (AM.viengTham[AM.goal] == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void DFS2(maTranKe AM, int u)
        {
            if (AM.viengTham[u] == false)
            {
                AM.viengTham[u] = true;
            }
            for (int i = 0; i < AM.n; i++)
            {
                if ((AM.viengTham[i] == false && AM.maTran[u, i] == 1) || (AM.viengTham[i] == false && AM.maTran[i, u] == 1))
                {
                    DFS2(AM, i);
                }
            }
        }
        public static int demSoThanhPhanLienThong(maTranKe AM)
        {
            for (int i = 0; i < AM.n; i++)
            {
                AM.viengTham[i] = false;
            }
            int dem = 0;
            for (int i = 0; i < AM.n; i++)
            {
                if (AM.viengTham[i] == false)
                {
                    dem++;
                    DFS2(AM, i);
                }
            }
            return dem;
        }
        public static void DFS3(maTranKe AM, int u)
        {
            if (AM.viengTham[u] == false)
            {
                AM.viengTham[u] = true;
                Console.Write($"{u} ");
            }
            for (int i = 0; i < AM.n; i++)
            {
                if ((AM.viengTham[i] == false && AM.maTran[u, i] == 1) || (AM.viengTham[i] == false && AM.maTran[i, u] == 1))
                {
                    DFS3(AM, i);
                }
            }
        }
        public static void inThanhPhanLienThong(maTranKe AM)
        {
            for (int i = 0; i < AM.n; i++)
            {
                AM.viengTham[i] = false;
            }
            int dem = 0;
            for (int i = 0; i < AM.n; i++)
            {
                if (AM.viengTham[i] == false)
                {
                    dem++;
                    Console.Write($"Thành phần liên thông thứ {dem}: ");
                    DFS3(AM, i);
                    Console.WriteLine();
                }
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            maTranKe AM;
            Console.WriteLine("Nhập vào đường dẫn file/tên file");
            string filename = Console.ReadLine();
            if (!File.Exists(filename))
            {
                Console.WriteLine("File không tồn tại");
            }
            else
            {
                AM = maTran(filename);
                /*
                //Câu a:
                Console.WriteLine("Danh sách đỉnh đã duyệt theo thứ tự:");
                DFS(AM, AM.start);
                Console.WriteLine();
                duongDi(AM);
                */


                /*
                //Câu b:
                Console.WriteLine("Danh sách đỉnh đã duyệt theo thứ tự:");
                BFS(AM, AM.start);
                Console.WriteLine();
                duongDi(AM);
                */


                //Câu c:
                Console.WriteLine($"Số thành phần liên thông: {demSoThanhPhanLienThong(AM)}");
                inThanhPhanLienThong(AM);

            }
        }
    }
}
