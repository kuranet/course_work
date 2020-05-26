using System.IO;
using System;
namespace EnumLibrary
{
    public class Metrics
    {
        public decimal[,] metrics { get; private set; }
            
        public Metrics() 
        {
            string path = @"..\..\..\Metrics.csv";

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {                
                var temp = sr.ReadLine().Split(';');
                int n = temp.Length-1;
                metrics = new decimal[n,n];
                for(int i =0; i< n; i++)
                {
                    temp = sr.ReadLine().Split(';');
                    for(int j =0; j< n; j++)
                    {
                        metrics[i, j] = Convert.ToDecimal(temp[j + 1]);
                    }
                }
            }
        }
    }
}
